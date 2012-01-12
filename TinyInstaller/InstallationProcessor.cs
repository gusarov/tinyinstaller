using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace TinyInstaller
{
	public class InstallationProcessor
	{
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
				Environment.SetEnvironmentVariable("LocalAppData", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			}
			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AppData")))
			{
				Environment.SetEnvironmentVariable("AppData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			}

			return Environment.ExpandEnvironmentVariables(str);
		}

		private static RegistryKey _regRoot = Registry.CurrentUser;
		private const string _uninstall = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";

		public void Install(InstallationSpecification spec)
		{
			spec.Validate();

			Environment.SetEnvironmentVariable("TargetDir", spec.TargetDir);

			DeployFiles(spec);
			AddWindowsUninstallInformation(spec);
		}

		void DeployFiles(InstallationSpecification spec)
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

		void RemoveFiles(InstallationSpecification spec)
		{
			DeleteDir(spec.TargetDir);
		}

		static void DeleteDir(string name)
		{
			Directory.Delete(name, true);
		}

		public void ReadActualValues(InstallationSpecification spec)
		{
			spec.IsInstalled = !string.IsNullOrEmpty(ReadRegistry<string>(spec.Identity, "DisplayName"));
			spec.InstallLocation = ReadRegistry<string>(spec.Identity, "InstallLocation");
		}

		void AddWindowsUninstallInformation(InstallationSpecification spec)
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

		public void Uninstall(InstallationSpecification spec)
		{
			ReadActualValues(spec);

			if (!spec.IsInstalled)
			{
				MessageBox.Show("Product is not installed");
			}
			else
			{
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
			Registry.SetValue(_regRoot.Name + "\\" + _uninstall + identity, parameter, value, GetKind(value));
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
			return (T)Registry.GetValue(_regRoot.Name + "\\" + _uninstall + identity, parameter, null);
		}

		internal static void DeleteWindowsUninstallInformation(string identity)
		{
			using (var reg = _regRoot.OpenSubKey(_uninstall, true))
			{
				reg.DeleteSubKey(identity);
			}
		}
	}
}
