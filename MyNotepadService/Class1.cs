using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MyNotepadService
{
	public class Program
	{
		static void Main()
		{
			var service = new Service1();
			ServiceBase.Run(new[] { service });
		}
	}
}
