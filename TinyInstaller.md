# Cases #

  * [Install Notepad using separate installation project](Case1.md)
  * [Install Service systemwide](Case4.md)


# Yet another `InstallShield` or `MSI`? #

## Terminology ##

First of all lets make clear basic terminology.

  * Installer - some software that should install another software to PC and uninstall it on demand
  * MSI install packages - standardized Microsoft Windows Installer packages. It is special case installer for solving common tasks and problems that shares well-designed multipurpose built-in engine.
  * Visual Studio Setup Project - simple and standard way of installer authoring, based on MSI. Provides limited functionality.
  * `InstallShield`, Wise, Wix  - fully featured installer authoring tools, based on MSI. The most advanced tools.

Please, notice that installers divides into two types: MSI-based and Non-MSI-based.

## Why Microsoft had created MSI and recommend it to use? ##

  * Automatic maintenance of references counters for shared components
  * Transactional reliable installation process
  * Versioning, patching and upgrade
  * Self-repairable products
  * Group Policy Deployment

## Does MSI really required? ##
  * Today shared components is uncommon case. HDD has enough of capacity and most software came with all dependencies and installs it under their target directories inside `ProgramFiles`. E.g. `DropBox` comes with `SqlLite`, how many programs do you have with `SqlLite`? Does any one had installed it as a shared component (`C:\Program Files\Common Files`)?
  * Transactional processing is necessary, but should be wise and careful. It is very easy to create MSI that cant be ever uninsulated nor repaired. Just make an exceptions inside uninstall custom action.
  * Versioning in MSI is, again, very deep and overthink. 90% of software never uses 90% of available versioning functionality. Unfortunately standard Visual Studio Setup Project can't provide a good way for upgrade. If try install a rebuilded package, on a system with package already installed - you got an error. If you increate version of your product - a new one will be installed side by side with existing, and, e.g. service installation will fail since "service with this name already exists". You can find a lot of articles and hacks around that problems.
  * I've noticed product self repairing only in 3 cases:
    * Microsoft Office components (Yep, MS had created a great tool for themselves)
    * On-demand installation of features (do you use it?)
    * Poorly authored installers, that starts to repair on every launch, because of problems with permissions or infrastructure. (It is better do not aware about it, rather make a bad installer. Hello from Pinnacle Studio)
  * Group Policy Deployment - that is definitely a point. But you always can run simple executable file by group policy. And I did not dig this subject deeply. Anyway, this feature should be supported. (Affects corporate software and users only)

## Popular programs that does not use MSI? ##
  * Opera Browser
  * Google Chrome
  * Google Picassa
  * `DropBox`
  * Adobe Flash Player
  * Total Commander
  * VMware
  * uTorrent
  * `WinRar`

## [How can I create non-MSI installer](NonMsi.md) ##
## [Msi Drawbacks](MsiDrawBacks.md) ##

[Edit](http://code.google.com/p/tinyinstaller/w/edit/TinyInstaller)