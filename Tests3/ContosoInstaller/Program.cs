using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUI;

namespace ContosoInstaller
{
	static class Program
	{
		static void Main()
		{
			var ref1 = typeof(MainWindow);
			TinyInstaller.EntryPoint.GuiRun();
		}
	}
}
