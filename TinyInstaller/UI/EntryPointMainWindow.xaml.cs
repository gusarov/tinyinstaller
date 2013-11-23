using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;

namespace TinyInstaller.UI
{
	public partial class EntryPointMainWindow
	{
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImport("user32")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImport("user32")]
		public static extern bool ReleaseCapture();


		public EntryPointMainWindow()
		{
			_interop = new WindowInteropHelper(this);
			InitializeComponent();
			var ep = EntryPoint.Instance;
			
			Title = ep.Title;
			// this is made by system already:
			//Icon = (ep.Icon ?? DefaulIcon)();
			DataContext = _model;
		}

		private readonly EntryPointViewModel _model = new EntryPointViewModel();

		private static ImageSource DefaulIcon()
		{
			var icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
			return icon.ToImageSource();
		}

		private readonly WindowInteropHelper _interop;

		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			if (!e.Handled)
			{
				e.Handled = true;
				ReleaseCapture();
				SendMessage(_interop.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Close();
		}

		private void MinButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			WindowState = System.Windows.WindowState.Minimized;
		}

		private void InstallButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}

		private void OptionsButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}
	}

	class EntryPointViewModel
	{
		bool HaveUserAgreement
		{
			get
			{
				return false;
			}
		}

		public string InstallButtonCaption
		{
			get
			{
				return HaveUserAgreement
					? "Accept & Install"
					: "Install";
			}
		}

		public string Copyright
		{
			get
			{
				var cr =
					(AssemblyCopyrightAttribute[])
					Assembly.GetEntryAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), true);
				if (cr.Any())
				{
					return cr.First().Copyright;
				}
				return null;
			}
		}
	}
}
