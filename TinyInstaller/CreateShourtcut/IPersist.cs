using System;
using System.Runtime.InteropServices;

namespace TinyInstaller.CreateShourtcut
{
	[ComImport]
	[Guid("0000010c-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	interface IPersist
	{
		[PreserveSig]
		void GetClassID(out Guid pClassID);
	}
}