using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Media;
using Control = System.Windows.Controls.Control;

namespace TinyInstaller
{
	public class EntryPoint
	{
		internal static EntryPoint Instance;

		public EntryPoint()
		{
			Instance = this;
		}

		public Func<Control> Logo { get; set; }
		public string Title { get; set; }
		// public Func<ImageSource> Icon { get; set; }

		public void Run()
		{
			StaThread(RunSta);
		}

		void RunSta()
		{
			// run as WPF application
			UI.App.EntryPointMain();
		}

		static void StaThread(Action continuation)
		{
			var thread = Thread.CurrentThread;
			if (thread.GetApartmentState() != ApartmentState.STA)
			{
				if (!thread.TrySetApartmentState(ApartmentState.STA))
				{
					thread = new Thread(() => continuation())
					{
						IsBackground = false,
						Name = "TinyInstaller STA Rethreading",
					};
					thread.SetApartmentState(ApartmentState.STA);
					thread.Start();
				}
				else
				{
					// we just become STA
					continuation();
				}
			}
			else
			{
				// we already STA
				continuation();
			}
		}
	}
}
