using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace TinyInstaller.Internal
{
	class EmbededResourceFileContainer : IFileContainer
	{
		private readonly Assembly _asm;
		string _embeddeps = "EmbedDeps_";

		public EmbededResourceFileContainer(Assembly asm)
		{
			_asm = asm;
		}

		public IEnumerator<InstallableFileInfo> GetEnumerator()
		{
			return GetFiles().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<InstallableFileInfo> GetFiles()
		{
			return _asm.GetManifestResourceNames().Where(x => x.StartsWith(_embeddeps)).Select(x => new InstallableFileInfo
			                                                                                        	(
			                                                                                        	x.Substring(_embeddeps.Length)
			                                                                                        	)).ToArray();
		}

		public Stream GetFileContent(string fileId)
		{
			return _asm.GetManifestResourceStream(_embeddeps + fileId);
		}
	}
}