using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyInstaller
{
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class InstallerIdentityAttribute : Attribute
	{
		readonly string _identity;

		public string Identity
		{
			get { return _identity; }
		}

		public InstallerIdentityAttribute(string identity)
		{
			_identity = identity;
		}
	}
}