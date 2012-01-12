using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	/// <summary>
	/// Windows Uninstall - WriteRegistry value of this property to the registry for operating system
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	internal sealed class WuAttribute : Attribute
	{
	}
}