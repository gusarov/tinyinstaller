# Problem #

Unfortunately you can often see full files and folder paths inside Internet. It is OK for beginner users, but not for IT professionals. Never publish full path, like `C:\Windows\notepad.exe` or `C:\Document and settings\UserName\`, especially when you publish it to mass of users.

The problem is quite bigger actually, since you can find it not only in professional forums, but in Source Code also!

Some users even have no C:\ drive at all. Some developers install their Visual Studio into custom location. Someone change a "Windows" folder name. Another just has x64 or not x64 system

# Legend #

People often surprised when I write something like `Path.GetTempFileName` for dropping some debugging information...

  * U: "Why not just write it to C:\ ?"
  * K: "Actually I have no access for writing in C:\ folder"
  * U: "?!"
  * K: "And it is very convenient to use folders by their purposes"
  * U: "How are you going to open it? It is so damn painful..."
  * K: "It is just 5 symbols typing... `<Win>+R, %tmp%, <Enter>`"
  * U: "o\_O"

It is very good to know that apps can't throw a shit everywhere... if you want apps not throwing a shit, do not throw a shit by yourselves. I'm always under non privileged user and with UAC enabled. More than that, I'm using embedded Software Restriction Policy turned on. This prevents me (and crapware) from executing arbitrary app from non trusted location.

# Solution #

Do use environment variables when you publish a paths.
Check "cmd /k set" for list of available variables and try to remember most common.

# Philosophy #

I suppose, future operating system should use absolute path mounted in different scopes.

  * E.g. `\user\documents\test.txt` instead of `%UserProfile%\Documents\test.txt`
  * E.g. `\app\config.xml` instead of `%AppData%\MyApp\config.xml`
  * E.g. `\disk\MyFlashDrive` instead of `F:\`

  * \public is mounted as a path to all users profile in system context
  * \user is mounted as a path to current user profile, in user session context
  * \app is mounted as a path to current application isolated storage in application context
  * \disk is a virtual path for default external drives mounting, rename is allowed (like drive label, combined with drive letter concept)

# More Samples #

  * `Path to msbuild: %windir%\Microsoft.NET\Framework\v3.5\msbuild.exe`
  * `Path to devenv: %VS100COMNTOOLS%..\IDE\devenv` (Yep, try to execute it in your system)
  * `Path to Remote Debugger: %VS100COMNTOOLS%..\IDE\Remote Debugger\x86\msvsmon.exe`