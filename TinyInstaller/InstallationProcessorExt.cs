using System;
using System.Collections.Generic;
using System.Linq;
using TinyInstaller.Internal;

namespace TinyInstaller
{
	public static class InstallationProcessorExt
	{
		public static void Install(this InstallationSpecification spec)
		{
			new InstallationProcessor(spec).Install();
		}

		public static void Uninstall(this InstallationSpecification spec)
		{
			new InstallationProcessor(spec).Uninstall();
		}
	}
}
