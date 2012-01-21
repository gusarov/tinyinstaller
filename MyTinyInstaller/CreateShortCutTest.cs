using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TinyInstaller.CreateShourtcut;
using System.Diagnostics;
using System.Threading;

namespace MyTinyInstaller
{
	[TestClass]
	public class CreateShortCutTest
	{
		[TestMethod]
		public void Test()
		{
			KillNotepads();
			ShellLinkApi.Create(@"%windir%\notepad.exe", "test", "testdescription");
			Assert.IsTrue(File.Exists("test.lnk"));
			Assert.IsFalse(Process.GetProcesses().Any(x => x.ProcessName.Contains("notepad")));
			Process.Start("test.lnk");
			Thread.Sleep(1000);
			Assert.IsTrue(Process.GetProcesses().Any(x => x.ProcessName.Contains("notepad")));
			KillNotepads();
		}

		private static void KillNotepads()
		{
			var pcs = Process.GetProcesses().Where(x => x.ProcessName.Contains("notepad"));
			foreach (var process in pcs)
			{
				process.Kill();
			}
		}
	}



}
