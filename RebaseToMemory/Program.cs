using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RebaseToMemory
{
	static class Program
	{
		static void Main()
		{
			Console.WriteLine("Dom " + AppDomain.CurrentDomain.Id);
			var dom = AppDomain.CreateDomain("Rebase");
			var location = typeof (Program).Assembly.Location;
			dom.Execute<RemoteClass>(x => x.Run(location, AppDomain.CurrentDomain));
//			var ev = new AutoResetEvent(false);
//			ThreadPool.QueueUserWorkItem(x =>
//			{
//				ev.Set();
//				AppDomain.Unload(AppDomain.CurrentDomain);
//			});
//			ev.WaitOne();
//			Thread.Sleep(1000);
		}

		public static void Execute<T>(this AppDomain domain, Action<T> action) where T : MarshalByRefObject
		{
			var runnerType = typeof(T);
			var runnerAsm = runnerType.Assembly.FullName;
			var runnerTypeName = runnerType.FullName;
			// var handle = Activator.CreateInstance(runnerAsm, runnerTypeName);
			// var remoteClass = (T)handle.Unwrap();
			var remoteClass = (T)domain.CreateInstanceAndUnwrap(runnerAsm, runnerTypeName);
			action(remoteClass);
		}
	}

	[Serializable]
	class RemoteClass : MarshalByRefObject
	{
		public void Run(string originalFile, AppDomain currentDomain)
		{
			var thread = new Thread(CallBack)
			{
				Name = "RebasedThread",
			};
			thread.Start(Tuple.Create(originalFile, currentDomain));
		}

		void CallBack(object state)
		{
			var stateTuple = (Tuple<string, AppDomain>)state;
			var originalFile = stateTuple.Item1;
			var originalDomain = stateTuple.Item2;
			Console.WriteLine("Dom " + AppDomain.CurrentDomain.Id);
			Console.WriteLine("OriginalDom " + originalDomain.Id);
			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(1000);
				Console.WriteLine(i);
			}
			AppDomain.Unload(originalDomain);
			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(1000);
				Console.WriteLine(i);
			}
			return;
			Debug.Assert(File.Exists(originalFile));

			Exception exx = null;
			for (int i = 0; i < 5; i++)
			{
				exx = TryDelete(originalFile, exx);
			}
			if (exx != null)
			{
				throw exx;
			}
			Debug.Assert(exx == null);
		}

		[DebuggerStepThrough]
		static Exception TryDelete(string originalFile, Exception exx)
		{
			try
			{
				File.Delete(originalFile);
			}
			catch (Exception ex)
			{
				exx = ex;
				Thread.Sleep(1000);
			}
			return exx;
		}
	}
}
