using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	public class InstallableFileInfo
	{
		public InstallableFileInfo(string fileId)
		{
			_fileId = fileId;
		}

		public string FileName
		{
			get { return Path.GetFileName(TargetPath); }
		}

		string _filePath;

		/// <summary>
		/// Path of file installation. E.g. "%TargetDir%\tinyLocaltion\file.ext"
		/// </summary>
		public string TargetPath
		{
			get { return _filePath ?? "%TargetDir%\\" + Path.GetFileName(FileId); }
			set { _filePath = value; }
		}

		private string _fileId;

		/// <summary>
		/// Identity of a file inside package
		/// </summary>
		public string FileId
		{
			get { return _fileId; }
		}

		// public bool DoNotDeploy { get; set; }
	}
}