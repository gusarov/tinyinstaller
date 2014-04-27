using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TinyInstaller.Internal
{
	public interface IFileContainer : IEnumerable<InstallableFileInfo>
	{
		IEnumerable<InstallableFileInfo> GetFiles();
		Stream GetFileContent(string fileId);
	}
}