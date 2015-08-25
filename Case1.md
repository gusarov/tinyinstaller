# Introduction #

Steps that you need to create simple installation project for your notepad windows application.

# Details #

  * Create empty project for your installer, like console application or any other template
  * Nuget tinyinstaller to it
  * Make sure you have entry point like
```
class Program
{
    static void Main()
    {
        TinyInstaller.EntryPoint.GuiRun();
    }
}
```