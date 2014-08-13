using TinyInstaller;

namespace SampleInstaller
{
	internal static class Program
	{
		public static void Main()
		{
			EntryPoint.GuiRunWith("My Notepad Installer", () => new Logo());
		}
	}
}
