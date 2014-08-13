using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyInstaller
{
	/// <summary>
	/// Windows Uninstall - WriteRegistry value of this property to the registry for operating system
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	sealed class WuAttribute : Attribute
	{
	}
}