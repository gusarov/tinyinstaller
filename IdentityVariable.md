# Introduction #

[Variables in TinyInstaller](Variables.md)

# Identity #

`%Identity%` - stores an identification string for your application

By default it reads from attributes of SpecAssembly in the next sequence till first is available:

  * `[assembly: InstallerIdentity(...)]`
  * `[assembly: AssemblyProduct(...)]`
  * `[assembly: AssemblyTitle(...)]`
  * Finally if nothing provided - file name is used

You can assign this property to spec manually.

Primary purpose is a key name inside [Windows Uninstall Key](WindowsUninstallKey.md).

Also [Identity](IdentityVariable.md) simplifies installer creation by providing default [Display Name](WindowsUninstallKey.md) and TargetDir application folder name.