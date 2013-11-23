using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TinyInstaller.Actions.CreateShourtcut
{
	public class ShellLinkApi
	{
		public static void Create(string original, string shortcut, string comment = null)
		{
			if (string.IsNullOrWhiteSpace(original))
			{
				throw new ArgumentException("", "original");
			}
			if (string.IsNullOrWhiteSpace(shortcut))
			{
				throw new ArgumentException("", "shortcut");
			}
			if (!shortcut.EndsWith(".lnk", StringComparison.InvariantCultureIgnoreCase))
			{
				shortcut += ".lnk";
			}

			var shellLink = new ShellLink(); // cocreate instance
			var ishellLink = (IShellLinkW)shellLink; // query interface
			var ipersistFile = (IPersistFile)shellLink; // query interface

			try
			{
				// Set the path to the shortcut target and add the description. 
				ishellLink.SetPath(original);
				ishellLink.SetDescription(comment);

				// Save the link by calling IPersistFile::Save. 
				ipersistFile.Save(shortcut, true);
			}
			finally
			{
				Marshal.ReleaseComObject(ishellLink);
				Marshal.ReleaseComObject(ipersistFile);
				Marshal.ReleaseComObject(shellLink);
			}
		}
	}
}
