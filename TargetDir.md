# What is `TargetDir` #

`TargetDir` - it is common alias for installation destination path.

This term is common both for MSI and for TinyInstaller.
In [Windows Uninstall Key](WindowsUninstallKey.md) it is known as `InstallLocation`

Usually it is something like:

  * `C:\Program Files\MyApp` ([never hard code a path](HardCodePath.md))
  * `C:\Program Files\MyCompany\MyApp` ([never hard code a path](HardCodePath.md))`

for user mode installation it is
  * `C:\Users\MyUser\AppData\Local\MyApp` ([never hard code a path](HardCodePath.md))
  * `C:\Users\MyUser\AppData\Local\MyCompany\MyApp` ([never hard code a path](HardCodePath.md))
  * `C:\Documents and settings\MyUser\Local Settings\Application Data\MyApp` ([never hard code a path](HardCodePath.md))
  * `C:\Documents and settings\MyUser\Local Settings\Application Data\MyCompany\MyApp` ([never hard code a path](HardCodePath.md))