using System;
using System.Collections.Generic;
using System.Linq;
using ContosoService;
using ContosoUI;

[assembly: TinyInstaller.InstallUserMode(false)]
[assembly: TinyInstaller.InstallUtilsAssembly(typeof(Service1))]

namespace ContosoInstaller
{
	static class Program
	{
		static void Main()
		{
			// var ref1 = typeof(MainWindow);
			// var ref2 = typeof(Service1);

			TinyInstaller.EntryPoint.GuiRun();
		}
	}
}
