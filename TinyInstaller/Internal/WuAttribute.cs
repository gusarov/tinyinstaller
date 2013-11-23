using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller.Internal
{
	/// <summary>
	/// Windows Uninstall - WriteRegistry value of this property to the registry for operating system
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	sealed class WuAttribute : Attribute
	{
	}
}