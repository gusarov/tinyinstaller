# Introduction #

This is registry key in operation system. Installers should write a couple values here about program installed and how to uninstall it.

# Path #

  * `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\`
  * `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\`
  * `HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\`

# Values #

I've seen a good article about this... it is necessary to publish

Specifying `DisplayName` and `UninstallString` is required.

# MSI #

Note that MSI packages write additional values and make this entry hidden for [Add/Remove Window](AddRemove.md). MSI create lots of additional entries and maintain another product and component registry, in another hive. [Add/Remove Window](AddRemove.md) analyze those hives too.