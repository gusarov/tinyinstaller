You should decide, how to install application.

# Installation for all users #

This is system wide installation of application. Every users can use it. You can install system services.

  * Only system administrators is able to install system-wide application
  * You have additional privileges for access to `ProgramData`, System, `ProgramFiles` and `ProgramFiles`\Common.
  * Default TargetDir is `%ProgramFiles%\%`[Identity](IdentityVariable.md)`%` (`C:\Program Files\MyApp`)
  * Default registry root is HKLM\_LOCAL\_MACHINE, and [Windows Uninstall information](WindowsUninstallKey.md) is stored here.
  * Every body can see that program is installed, but only system administrators is able to uninstall it. Windows elevates installer automatically, if required.
  * This is most popular way

# Installation for current user #

  * Installation for current user should be performed without additional privileges. You can not install system services or affect other users in any way.
  * Default TargetDir is %[LocaAppData](LocaAppDataVariable.md)%\%Identity% (`C:\Users\gusarov.d\AppData\Local\MyApp\`) Since you have not rights to [ProgramFiles](ProgramFilesVariable.md), and you should not confuse another users on a system.
  * Default registry root is HKLM\_CURRENT\_USER, and Windows Uninstall information is stored here. You can see only system-wide and your applications in [Add/Remove window](AddRemove.md). Other users does not see this application installed unless they install it by themselves.
  * Sample: Google Chrome, Google Talk, Google Picassa 2, Microsoft Live Mesh (before ally with Live Essentials)

# Ask during installation #
  * Supported by MSI
  * Rare practical case, requires good additional knowledge
  * In fact, should be automated as a modern concept. E.g. let's install for everybody, if user is elevated, or can be elevated automatically ([Execution Level](UacExecutionLevel.md) = highestAvailable). But suggest elevation and allow proceed with user-mode installation.
  * Sample: Total Commander (Ask), Opera (Automatic)