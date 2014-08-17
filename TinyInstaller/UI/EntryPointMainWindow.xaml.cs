using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
			DataContext = _model;
		}

		private readonly EntryPointViewModel _model = new EntryPointViewModel();

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

		private bool? _success;

		private void InstallButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var sb = (Storyboard)Resources["_goInstall"];
			sb.Begin();
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					retry:
					try
					{
						var spec = EntryPoint.SpecFromEntryAssembly();
						spec.Install();
						_success = true;
					}
					catch (Exception ex)
					{
						_success = false;
						var res = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
						if (res == System.Windows.Forms.DialogResult.Retry)
						{
							goto retry;
						}
					}
				}
				finally
				{
					if (_success == true)
					{
						Dispatcher.BeginInvoke((Action)delegate
						{
							var sb2 = (Storyboard)Resources["_goInstalled"];
							sb2.Begin();
						});
						Thread.Sleep(2000);
					}
					Dispatcher.BeginInvoke((Action)delegate
					{
						Close();
					});
				}
			});
		}

		private void OptionsButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}
	}

	class EntryPointViewModel
	{
		public string Title
		{
			get { return EntryPointArgs.Instance.Title; }
		}

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

		public ImageSource Icon
		{
			get
			{
				var icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
				return icon.ToImageSource();
			}
		}

	}
}
