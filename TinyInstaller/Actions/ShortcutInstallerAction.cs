using System;
using System.IO;
using TinyInstaller.CreateShourtcut;

namespace TinyInstaller.Actions
{
	public class ShortcutInstallerAction : Attribute, IInstallerAction
	{
		private const string DefaultShortcut = "%userprofile%\\desktop";
		private string _original;
		private string _shortcut;

		public ShortcutInstallerAction(Type assembly, string shortcut = DefaultShortcut)
			: this(Path.GetFileName(assembly.Assembly.Location), shortcut)
		{
			
		}

		public ShortcutInstallerAction(string original, string shortcut = DefaultShortcut)
		{
			_original = original;
			_shortcut = shortcut;
		}

		public void Install()
		{
			VerifyPath();
			ShellLinkApi.Create(_original, _shortcut);
		}

		private void VerifyPath()
		{
			_original = InstallationProcessor.Expand(_original);
			if (!Path.IsPathRooted(_original))
			{
				_original = Path.Combine(InstallationProcessor.Expand("%targetdir%"), _original);
			}

			_shortcut = InstallationProcessor.Expand(_shortcut);
			var file = Path.GetFileName(_original);
			if (string.IsNullOrEmpty(file))
			{
				throw new Exception("Can not parse file name from: " + _original);
			}
			if (!_shortcut.EndsWith(file, StringComparison.InvariantCultureIgnoreCase))
			{
				_shortcut = Path.Combine(_shortcut, file);
			}
		}

		public void Uninstall()
		{
			VerifyPath();
			File.Delete(_shortcut);
		}
	}
}