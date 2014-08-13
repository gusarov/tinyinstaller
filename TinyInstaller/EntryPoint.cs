using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Media;
using TinyInstaller.Internal;
using Control = System.Windows.Controls.Control;

namespace TinyInstaller
{
	class EntryPointArgs
	{
		public static readonly EntryPointArgs Instance = new EntryPointArgs();

		EntryPointArgs()
		{
			
		}

		public Func<Control> Logo { get; set; }
		public string Title { get; set; }
	}

	public static class EntryPoint
	{
// ReSharper disable MethodOverloadWithOptionalParameter
		public static void GuiRunWith(string title = null, Func<Control> logo = null)
// ReSharper restore MethodOverloadWithOptionalParameter
		{
			EntryPointArgs.Instance.Title = title;
			EntryPointArgs.Instance.Logo = logo;
			GuiRun();
		}

		public static void GuiRun()
		{
			StaThread(RunSta);
		}

		public static InstallationSpecification SpecFromCaller()
		{
			var asm = Assembly.GetCallingAssembly();
			return SpecLoader.FromAssembly(asm);
		}

		public static InstallationSpecification SpecFromEntryAssembly()
		{
			var asm = Assembly.GetEntryAssembly();
			return SpecLoader.FromAssembly(asm);
		}

		public static InstallationSpecification SpecFrom<TAssembly>()
		{
			return SpecLoader.FromAssembly<TAssembly>();
		}

		static void RunSta()
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
					thread.Join();
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

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public static new bool Equals(object obj1, object obj2)
		{
			throw new Exception("This is not object.Equals!");
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new static bool ReferenceEquals(object obj1, object obj2)
		{
			throw new Exception("This is not object.ReferenceEquals!");
		}
	}
}
