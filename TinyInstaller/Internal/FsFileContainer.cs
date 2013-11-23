using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace TinyInstaller.Internal
{
	class FsFileContainer  : IFileContainer
	{
		public FsFileContainer(DirectoryInfo dir = null)
		{
			_directoryInfo = dir;
		}

		public FsFileContainer(string dir = null)
		{
			if (!string.IsNullOrEmpty(dir))
			{
				_directoryInfo = new DirectoryInfo(dir);
			}
		}

		public IEnumerator<InstallableFileInfo> GetEnumerator()
		{
			return GetFiles().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		DirectoryInfo _directoryInfo;
		DirectoryInfo DirectoryInfo
		{
			get
			{
				return _directoryInfo ?? (_directoryInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)));
			}
		}

		public IEnumerable<InstallableFileInfo> GetFiles()
		{
			return DirectoryInfo.GetFiles().Select(x =>
			                                       new InstallableFileInfo(x.FullName)
				);
		}

		public Stream GetFileContent(string fileId)
		{
			return File.OpenRead(fileId);
		}
	}
}