using MyNotepad;
using MyNotepadService;
using MyNotepadUtils;
using TinyInstaller;
using TinyInstaller.Actions;
using TinyInstaller.Internal;

//[assembly: InstallUtilsAssembly(typeof(MainNotepadWindow))]
[assembly: InstallUtilsAssembly(typeof(Installer2))]
[assembly: InstallUserMode(false)]
[assembly: ShortcutInstallerAction(typeof(MainNotepadWindow))]

public class MyNotepadInstallSpecClass
{
	
}