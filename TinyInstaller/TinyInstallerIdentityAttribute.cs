using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class TinyInstallerIdentityAttribute : Attribute
	{
		readonly string _identity;

		public string Identity
		{
			get { return _identity; }
		}

		public TinyInstallerIdentityAttribute(string identity)
		{
			_identity = identity;
		}
	}
}