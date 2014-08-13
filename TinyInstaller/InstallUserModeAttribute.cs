using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyInstaller
{

	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class InstallUserModeAttribute : Attribute
	{
		private readonly bool _isUserMode;

		public bool IsUserMode
		{
			get { return _isUserMode; }
		}

		public InstallUserModeAttribute(bool isUserMode)
		{
			_isUserMode = isUserMode;
		}
	}
}