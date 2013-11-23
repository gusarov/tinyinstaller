using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace TinyInstaller.Build
{
	[LoadInSeparateAppDomain]
	public class InstallerBuildTask : AppDomainIsolatedTask
	{
		public override bool Execute()
		{
			return false;
		}
	}
}
