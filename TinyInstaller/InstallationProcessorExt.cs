using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	public static class InstallationProcessorExt
	{
		public static void Install(this InstallationSpecification spec)
		{
			new InstallationProcessor().Install(spec);
		}

		public static void Uninstall(this InstallationSpecification spec)
		{
			new InstallationProcessor().Uninstall(spec);
		}
	}
}