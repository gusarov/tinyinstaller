  * [Manually Adding or Removing Services and Devices](http://nt4ref.zcm.com.au/mansd.htm)
  * [Microsoft Support: CurrentControlSet\Services Subkey Entries](http://support.microsoft.com/?id=103000)

# NOTE #

Call Windwos API for refresh services list

# Fields #

  * Type - Specifies the type of Device or Service being installed. Valid values are:
    * 00000001 - This is a Kernel Mode Driver.
    * 00000002 - This is a File System Driver.
    * 00000004 - This is an argument group.
    * 00000010 - This is a Win32 Service that runs in it's own address space.
    * 00000020 - This is a Win32 Service that runs in shared address space.

  * Start - Specifies when the Device or Service is to be started. Valid values are:
    * 00000000 - Boot (Device Drivers Only - preload before Kernel Startup)
    * 00000001 - System (Device Drivers Only - load at Kernel Startup)
    * 00000002 - Automatic (Start Automatically during System Startup)
    * 00000003 - Manual (Start on demand, when required)
    * 00000004 - Disabled (DO NOT Start)

  * ErrorControl - Specifies what action to take if Device or Service fails to initialise. Valid values are:
    * 00000000 - Ignore the error completely and continue system boot normally.
    * 00000001 - Log the error and continue system boot normally. (You will get a Service Control Manager popup warning after system boot)
    * 00000002 - Switch to 'Last Known Good' control set and continue system boot.
    * 00000003 - Fail the startup, reboot and use 'Last Known Good' control set. If already on the 'Last Known Good' control set, generate a 'STOP' error (Blue Screen of Death) and halt the system.