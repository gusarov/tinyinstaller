using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyInstaller;
using MyTinyInstaller.Properties;

namespace MyTinyInstaller
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			new InstallationProcessor().Install(new InstallationSpecification()
			                                    	{
			                                    		Identity = "___Test", // authoring should generate it
			                                    	});
		}

		[TestMethod]
		public void TestMethod2()
		{
			new InstallationProcessor().Uninstall(new InstallationSpecification()
			{
				Identity = "___Test", // authoring should generate it
			});
		}

	}
}
