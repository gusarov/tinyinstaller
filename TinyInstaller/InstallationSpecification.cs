using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller
{
	public class InstallationSpecification
	{
		public bool IsInstalled { get; set; }

		public IFileContainer FilesToInstall { get; set; }

		/// <summary>
		/// Internal fixed tinyAssemblyFileName or guid. This is tinyAssemblyFileName of a key in a registry
		/// </summary>
		public string Identity { get; set; }

		#region WU REQUIRED

		private string _uninstallString;

		/// <summary>
		/// Command line for uninstalling product. Windows will execute it.
		/// </summary>
		[Wu]
		public string UninstallString
		{
			get
			{
				return _uninstallString ?? DefaultUninstallString;
			}
			set { _uninstallString = value; }
		}

		protected string DefaultUninstallString
		{
			get
			{
				// return string.Format(@"cmd /c copy /y ""{0}"" ""%tmp%"" & ""%tmp%\TinyInstaller.exe"" -UninstallThisFolder", Path.Combine(TargetDir, @"TinyInstaller.exe"));
				return string.Format(@"""{0}"" uninstall {1} ""{2}""", Path.Combine(TargetDir, @"TinyInstaller.exe"), Identity, Path.Combine(TargetDir, SpecAssembly));
			}
		}

		#endregion

		#region WU RECOMMENDED

		private string _displayName;

		/// <summary>
		/// Display tinyAssemblyFileName of application as it appears in Add/Remove. This is required value, by default equals to Identity.
		/// </summary>
		[Wu]
		public string DisplayName
		{
			get { return _displayName ?? Identity; }
			set { _displayName = value; }
		}

		[Wu]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string InstallLocation
		{
			get { return TargetDir; }
			set { TargetDir = value; }
		}

		[Wu]
		public string DisplayIcon { get; set; }

		[Wu]
		public string HelpLink { get; set; }

		#endregion

		#region WU OPTIONAL

		// public string URLUpdateInfo = "http://my/updates/";
		// public string HelpTelephone;
		private ulong _noModify = 1;

		[Wu]
		public ulong NoModify
		{
			get { return _noModify; }
			set { _noModify = value; }
		}

		private ulong _noRepair = 1;

		[Wu]
		public ulong NoRepair
		{
			get { return _noRepair; }
			set { _noRepair = value; }
		}

		// public ulong SystemComponent = 0;
		// public ulong EstimatedSize = 10240;
		// public string Comments = "No Comments";

		#endregion

		public void Validate()
		{
			if (string.IsNullOrEmpty(Identity))
			{
				throw new InvalidOperationException("AppInfo.Identity is not specified");
			}
			if (string.IsNullOrEmpty(DisplayName))
			{
				throw new InvalidOperationException("AppInfo.DisplayName is not specified");
			}
			if (string.IsNullOrEmpty(UninstallString))
			{
				throw new InvalidOperationException("AppInfo.UninstallString is not specified");
			}
			if (FilesToInstall == null || !FilesToInstall.Any())
			{
				throw new InvalidOperationException("AppInfo.FilesToInstall is empty");
			}
		}

		bool _isUserMode = true;
		public bool IsUserMode
		{
			get { return _isUserMode; }
			set { _isUserMode = value; }
		}

		public string SpecAssembly { get; set; }

		string _targetDir;

		public string TargetDir
		{
			get { return _targetDir ?? DefaultTargetDir; }
			set { _targetDir = value; }
		}

		protected string DefaultTargetDirName
		{
			get { return Identity; }
		}

		protected string DefaultTargetDir
		{
			get { return IsUserMode ? DefaultUserTargetDir : DefaultAdminTargetDir; }
		}

		protected string DefaultAdminTargetDir
		{
			get { return InstallationProcessor.Expand(@"%ProgramFiles%\" + DefaultTargetDirName); }
		}

		protected string DefaultUserTargetDir
		{
			get { return InstallationProcessor.Expand(@"%LocalAppData%\" + DefaultTargetDirName); }
		}
	}
}