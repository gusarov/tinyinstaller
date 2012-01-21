using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1;

[assembly: My("!")]

namespace ConsoleApplication1
{
	[My("!")]
	class Program
	{
		[My("!")]
		public static void Main()
		{
			Console.WriteLine("test");
		}
	}

	internal class Program2
	{
		private static void Main()
		{
			Console.WriteLine("haha");
			Program.Main();
		}
	}

	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class MyAttribute : Attribute
	{
		public MyAttribute(string a)
		{
			Console.WriteLine(a);
		}
	}
}
