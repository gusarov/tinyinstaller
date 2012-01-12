using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using MyNotepadUtils;

namespace MyNotepad
{
	/// <summary>
	/// Interaction logic for MainNotepadWindow.xaml
	/// </summary>
	public partial class MainNotepadWindow
	{
		public MainNotepadWindow()
		{
			InitializeComponent();
			try
			{
				Test();
			}catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		static void Test()
		{
			new MyNotepadUtilsClass();
		}
	}
}
