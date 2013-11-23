using System.Windows;

namespace TinyInstaller.UI
{
	public class App : Application
	{
		public void InitializeComponent()
		{
			StartupUri = new System.Uri("/TinyInstaller;component/UI/EntryPointMainWindow.xaml", System.UriKind.Relative);
		}

		public static void EntryPointMain()
		{
			var app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}

