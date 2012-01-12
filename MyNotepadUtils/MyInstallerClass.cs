using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace MyNotepadUtils
{
	[RunInstaller(true)]
	public class MyInstallerClass : Installer
	{
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			base.Install(stateSaver);
			// MessageBox.Show("!Install!");
		}

		public override void Uninstall(System.Collections.IDictionary savedState)
		{
			base.Uninstall(savedState);
			// MessageBox.Show("!Uninstall!");

		}
	}
}