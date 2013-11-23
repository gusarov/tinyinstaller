using TinyInstaller;

namespace SampleInstaller
{
	internal static class Program
	{
		public static void Main()
		{
			new EntryPoint
			{
				Logo = () => new Logo(),
				Title = "My Notepad Installer",
			}.Run();
		}
	}
}
