using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	public enum InstallationInterface
	{
		Sielent, // display no UI
		ProgressUi, // display AdobeFlash-style passive installator
		ProgressOkUi, // display AdobeFlash-style installator with one button
	}
}