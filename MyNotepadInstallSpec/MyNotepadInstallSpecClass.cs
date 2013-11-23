using MyNotepad;
using MyNotepadService;
using MyNotepadUtils;
using TinyInstaller;
using TinyInstaller.Actions;

[assembly: InstallUtilsAssembly(typeof(MyInstallerClass))]
[assembly: InstallUtilsAssembly(typeof(Installer2))]
[assembly: InstallUserMode(false)]
[assembly: ShortcutInstallerAction(typeof(MainNotepadWindow))]

public class MyNotepadInstallSpecClass
{
	
}