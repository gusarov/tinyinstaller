using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyNotepad;

using MyNotepadService;

using MyNotepadUtils;

using TinyInstaller;

[assembly: InstallUtilsAssembly(typeof(MyInstallerClass))]
[assembly: InstallUtilsAssembly(typeof(Installer2))]
[assembly: InstallUserMode(false)]

namespace MyNotepadInstallSpec
{
	public class MyNotepadInstallSpecClass
	{
		public MyNotepadInstallSpecClass()
		{
			var q = typeof(Class111);
			var q2 = typeof(Installer2);
			
		}
	}
}
