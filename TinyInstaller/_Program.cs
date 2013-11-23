using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using TinyInstaller.Internal;

// using MyUtils;

namespace TinyInstaller
{
	static class _Program
	{
		// Sample:

		// TinyInstaller uninstall "My App" "C:\ProgramFiles\My App\Spec.dll"
		// load spec from specified assembly and initiate uninstallation process

		// TinyInstaller uninstallCommit "My App" "C:\ProgramFiles\My App" 2455
		// complete uninstallation process by removing last folder (being bisy by main unsinstallation process) and Windows Uninstaller information

		static int Main(string[] args)
		{
			try
			{
				// MessageBox.Show(string.Join(" ", args)+"\r\n"+Assembly.GetExecutingAssembly().Location, "TinyInstaller");
				// WaitForDebugger();

				for (int i = 0; i < args.Length; i++)
				{
					if (IsKey(args[i], "Uninstall"))
					{
						var identity = args[++i];
						var spec = args[++i];
						Uninstall(identity, spec);
					}
					if (IsKey(args[i], "UninstallCommit"))
					{
						var identity = args[++i];
						var lastDirectoryToRemove = args[++i];
						var waitForProcess = int.Parse(args[++i]);
						UninstallCommit(identity, lastDirectoryToRemove, waitForProcess);
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				Messaging.Message(ex.GetType().Name, ex.Message);
				return 1;
			}
		}

		static void WaitForDebugger()
		{
			while (!Debugger.IsAttached)
			{
				Thread.Sleep(100);
			}
		}

		static void UninstallCommit(string identity, string lastDirectoryToRemove, int waitForProcess)
		{
			WaitForTerminating(waitForProcess);
			InstallationProcessor.UninstallCommit(identity, lastDirectoryToRemove);
		}

		static void WaitForTerminating(int waitForProcess)
		{
			while (true)
			{
				try
				{
					using (Process.GetProcessById(waitForProcess)) {}
					// MessageBox.Show("Process is alive");
				}
				catch (ArgumentException)
				{
					// MessageBox.Show("Process terminated");
					break;
				}
				Thread.Sleep(100);
			}
		}

		static void Uninstall(string identity, string specAssemblyFile)
		{
			var spec = SpecLoader.FromAssembly(specAssemblyFile);
			if (!string.IsNullOrEmpty(identity))
			{
				spec.Identity = identity;
			}
			spec.Uninstall();
		}

		static bool IsKey(string arg, string keyName)
		{
			return string.Equals(arg, keyName, StringComparison.InvariantCultureIgnoreCase);
		}

	}
}
