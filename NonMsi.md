# brief description #

## Installation ##

  * Ask user
    * Approve a license agreement
    * Select target directory (C:\Program Files\My App is default)
  * Install
    * Copy files to target directory
    * Run custom actions
    * Write uninstall information into Windows Registry into WindowsUninstallKey
      * `SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\MyApp`
      * required `DisplayName` = "My App"
      * required `UninstallString` = "C:\Program Files\My App\uninstall.exe"
  * Inform user

## Uninstallation ##

  * `UninstallString` from registry is executed by the system
  * Confirm with user
  * Uninstall
    * Run custom actions
    * Remove files from target directory
      * Oops, directory is locked by installer itself or by custom tools
        * Release custom tools (Close process or App Domain)
        * Run installer from another location and commit uninstall
    * Remove uninstall information from Windows Registry
      * `SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\MyApp`
  * Inform user