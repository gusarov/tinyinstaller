using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	public interface IFileContainer : IEnumerable<InstallableFileInfo>
	{
		IEnumerable<InstallableFileInfo> GetFiles();
		Stream GetFileContent(string fileId);
	}
}