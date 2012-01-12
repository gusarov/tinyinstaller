using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace TinyInstaller
{
	public class InstallationProcessor
	{
		private readonly InstallationSpecification spec;

		public InstallationProcessor(InstallationSpecification spec)
		{
			this.spec = spec;
		}

		public static string Expand(string str)
		{
			// TODO Verify
			// stability for WOW64 installations
			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ProgramFiles(x86)")))
			{
				Environment.SetEnvironmentVariable("ProgramFiles(x86)", Environment.GetEnvironmentVariable("ProgramFiles"));
			}

			// windows XP / 2003
			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LocalAppData")))
			{
				Environment.SetEnvironmentVariable("LocalAppData",
				                                   Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			}
			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AppData")))
			{
				Environment.SetEnvironmentVariable("AppData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			}

			return Environment.ExpandEnvironmentVariables(str);
		}

		private RegistryKey RegRoot
		{
			get { return GetRegRoot(spec.IsUserMode); }
		}

		private static RegistryKey GetRegRoot(bool isUserMode)
		{
			return isUserMode ? Registry.CurrentUser : Registry.LocalMachine;
		}

		private const string _uninstall = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";

		public void Install()
		{
			spec.Validate();

			SetVariables();

			DeployFiles();
			RunActions(false, true);
			RunActions(true);
			AddWindowsUninstallInformation();
		}

		void SetVariables()
		{
			Environment.SetEnvironmentVariable("TargetDir", spec.TargetDir);
		}

		void RunActions(bool installOrUninstall, bool ignoreErrors = false)
		{
			foreach (var installUtilAssembly in spec.AssembliesForInstallUtils)
			{
				var installUtil = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "InstallUtil");
				try
				{
					Run(installUtil, (installOrUninstall ? "" : "/u") + " " + TargetFileNameById(installUtilAssembly.FileId));
				}
				catch (Exception ex)
				{
					if (!ignoreErrors)
					{
						throw;
					}
				}
			}
		}

		void Run(string command, string arg)
		{
			var psi = new ProcessStartInfo(command, arg)
			{
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
			};
			var p = Process.Start(psi);
			p.WaitForExit();
			if (p.ExitCode != 0)
			{
				throw new Exception(string.Format("ExitCode: {0}: {1} {2}", p.ExitCode, p.StandardOutput.ReadToEnd(), p.StandardError.ReadToEnd()));
			}
		}

		void DeployFiles()
		{
			foreach (var file in spec.FilesToInstall)
			{
				using(var stream = spec.FilesToInstall.GetFileContent(file.FileId))
				{
					var destinationFileName = Expand(file.TargetPath);
					Directory.CreateDirectory(Path.GetDirectoryName(destinationFileName));
					using (var destination = File.OpenWrite(destinationFileName))
					{
						stream.CopyTo(destination);
					}
				}
			}
		}

		string TargetFileNameById(string fileId)
		{
			return Expand(spec.FilesToInstall.Single(x => x.FileId == fileId).TargetPath);
		}

		void RemoveFiles()
		{
			DeleteDir(spec.TargetDir);
		}

		static void DeleteDir(string name)
		{
			Directory.Delete(name, true);
		}

		public void ReadActualValues()
		{
			spec.IsInstalled = !string.IsNullOrEmpty(ReadRegistry<string>(spec.Identity, "DisplayName"));
			spec.InstallLocation = ReadRegistry<string>(spec.Identity, "InstallLocation");
		}

		void AddWindowsUninstallInformation()
		{
			// save actual install location
			spec.InstallLocation = spec.TargetDir;

			foreach (var pro in spec.GetType().GetProperties())
			{
				if (pro.GetCustomAttributes(typeof(WuAttribute), false).Any())
				{
					var value = pro.GetValue(spec, null);
					if (value != null && value != string.Empty && !0.Equals(value))
					{
						WriteRegistry(spec.Identity, pro.Name, value);
					}
				}
			}
		}

		public void Uninstall()
		{
			ReadActualValues();

			if (!spec.IsInstalled)
			{
				MessageBox.Show("Product is not installed");
			}
			else
			{
				SetVariables();
				RunActions(false);
				if (AppDomain.CurrentDomain.GetAssemblies().Any(x => x.Location.StartsWith(spec.InstallLocation, StringComparison.InvariantCultureIgnoreCase)))
				{
					Rebase("UninstallCommit", spec.Identity, spec.InstallLocation, Process.GetCurrentProcess().Id);
				}
				else
				{
					UninstallCommit(spec.Identity, spec.InstallLocation);
				}
			}
		}

		static void Rebase(params object[] arguments)
		{
			var tinyAssembly = typeof(Program).Assembly;
			var tinyLocaltion = tinyAssembly.Location;
			//var targetDir = Path.GetDirectoryName(tinyAssembly.Location);
			var tinyAssemblyFileName = Path.GetFileName(tinyLocaltion);
			var shadowTinyAssemblyFileName = Path.Combine(Path.GetTempPath(), tinyAssemblyFileName);
			File.Copy(Assembly.GetExecutingAssembly().Location, shadowTinyAssemblyFileName, true);
			Process.Start(shadowTinyAssemblyFileName, string.Join(" ", arguments));
		}

		internal static void UninstallCommit(string identity, string targetDir)
		{
			DeleteDir(targetDir);
			DeleteWindowsUninstallInformation(identity);
		}

		private void WriteRegistry(string identity, string parameter, object value)
		{
			Registry.SetValue(RegRoot.Name + "\\" + _uninstall + identity, parameter, value, GetKind(value));
		}

		RegistryValueKind GetKind(object value)
		{
			if (value is string)
			{
				return RegistryValueKind.String;
			}
			if (value is int || value is uint || value is long || value is ulong)
			{
				return RegistryValueKind.DWord;
			}
			throw new Exception("Unknown Value Kind for " + (value == null ? "null" : value.GetType().Name));
		}

		private T ReadRegistry<T>(string identity, string parameter)
		{
			return (T)Registry.GetValue(RegRoot.Name + "\\" + _uninstall + identity, parameter, null);
		}

		internal static void DeleteWindowsUninstallInformation(string identity)
		{
			bool ok = false;
			using (var reg = GetRegRoot(true).OpenSubKey(_uninstall, true))
			{
				if (reg != null)
				{
					if (reg.GetSubKeyNames().Contains(identity))
					{
						reg.DeleteSubKey(identity);
						ok = true;
					}
				}
			}
			if(!ok)
			{
				using (var reg = GetRegRoot(false).OpenSubKey(_uninstall, true))
				{
					if (reg != null)
					{
						if (reg.GetSubKeyNames().Contains(identity))
						{
							reg.DeleteSubKey(identity);
							ok = true;
						}
					}
				}
			}
		}


	}
}
