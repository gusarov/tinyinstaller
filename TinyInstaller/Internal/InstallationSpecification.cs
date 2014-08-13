using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TinyInstaller.Internal
{
	public class InstallationSpecification
	{
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public InstallationInterface Interface { get; set; }

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsInstalled { get; set; }

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFileContainer FilesToInstall { get; set; }

		/// <summary>
		/// Internal fixed name or guid. This is name of a key in a registry
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		private string _qUninstallString;

		[Wu]
		public string QuietUninstallString
		{
			get
			{
				return _qUninstallString ?? (UninstallString + " /s");
			}
			set { _qUninstallString = value; }
		}

		protected string DefaultUninstallString
		{
			get
			{
				// return string.Format(@"cmd /c copy /y ""{0}"" ""%tmp%"" & ""%tmp%\TinyInstaller.exe"" -UninstallThisFolder", Path.Combine(TargetDir, @"TinyInstaller.exe"));
				return string.Format(@"""{0}"" uninstall ""{1}"" ""{2}""", Path.Combine(TargetDir, @"TinyInstaller.exe"), Identity, Path.Combine(TargetDir, SpecAssembly));
			}
		}

		#endregion

		#region WU RECOMMENDED

		private string _displayName;

		private string _displayIcon;

		[Wu]
		public string DisplayIcon
		{
			get
			{
				return _displayIcon ?? (Path.Combine(TargetDir, SpecAssembly) + ",0");
			}
			set { _displayIcon = value; }
		}

		/// <summary>
		/// Display name of application as it appears in Add/Remove. This is required value, by default equals to Identity.
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
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string HelpLink { get; set; }

		[Wu]
		public string Publisher { get; set; }

		[Wu]
		public string DisplayVersion { get; set; }

		#endregion

		#region WU OPTIONAL

		// public string URLUpdateInfo = "http://my/updates/";
		// public string HelpTelephone;
		private ulong _noModify = 1;

		[Wu]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ulong NoModify
		{
			get { return _noModify; }
			set { _noModify = value; }
		}

		private ulong _noRepair = 1;

		[Wu]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		bool? _isUserMode;
		public bool IsUserMode
		{
			get { return _isUserMode ?? true; }
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
			get { return IsUserMode ? DefaultUserTargetDir : DefaultSystemTargetDir; }
		}

		protected string DefaultSystemTargetDir
		{
			get { return InstallationProcessor.Expand(@"%ProgramFiles%\" + DefaultTargetDirName); }
		}

		protected string DefaultUserTargetDir
		{
			get { return InstallationProcessor.Expand(@"%LocalAppData%\" + DefaultTargetDirName); }
		}

		public IEnumerable<InstallableFileInfo> AssembliesForInstallUtils { get; set; }
	}
}