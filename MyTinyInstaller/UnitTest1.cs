using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyInstaller;
using MyTinyInstaller.Properties;
using TinyInstaller.Internal;

namespace MyTinyInstaller
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			new InstallationSpecification
				{
					Identity = "___Test",
					// authoring should generate it
				}.Install();
		}

		[TestMethod]
		public void TestMethod2()
		{
			new InstallationSpecification
			{
				Identity = "___Test", // authoring should generate it
			}.Uninstall();
		}

	}
}
