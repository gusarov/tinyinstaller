using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyInstaller.Internal
{
	public enum InstallationInterface
	{
		Sielent, // display no UI
		ProgressUi, // display AdobeFlash-style passive installator
		ProgressOkUi, // display AdobeFlash-style installator with one button
	}
}