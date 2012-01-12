using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using MyNotepadInstallSpec;

using TinyInstaller;

namespace MyNotepadInstallPackage
{
	class Program
	{
		static void Main(string[] args)
		{
			/* ! Write(EmbedReferences.MetaBuild()); */

			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

//			Console.WriteLine("Debugger..");
//			while (!Debugger.IsAttached)
//			{
//				Thread.Sleep(100);
//			}

			Code();
//			var spec = SpecLoader.FromAssembly<MyNotepadInstallSpecClass>();
//			spec.Install();
//			Console.WriteLine("Installed at " + spec.InstallLocation);
		}

		static void Code()
		{
			var spec = SpecLoader.FromAssembly<MyNotepadInstallSpecClass>();
			spec.TargetDir = "C:\\_cus";
			spec.Install();
			Console.WriteLine("Installed");
		}

		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			try
			{
				//Console.WriteLine("Resolve: " + args.Name);



				var resourceName = "EmbedDeps_" + args.Name.Substring(0, args.Name.IndexOf(','));

				byte[] blob;

				//Console.WriteLine(resourceName);

				var dll = typeof(Program).Assembly.GetManifestResourceStream(resourceName + ".dll");
				var exe = typeof(Program).Assembly.GetManifestResourceStream(resourceName + ".exe");

				using (var res = dll ?? exe)
				{
					if (res == null)
					{
						throw new Exception("Assembly not resolved: " + args.Name);
					}
					blob = new byte[res.Length];
					res.Read(blob, 0, blob.Length);
				}
				var asm = Assembly.Load(blob);
				//Console.Write("HOT LOAD: " + args.Name);
				//Console.WriteLine(" Location: " + asm.Location);
				return asm;
			}
			catch (Exception ex)
			{
				var f = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ForegroundColor = f;
				throw;
			}
		}
	}
}
