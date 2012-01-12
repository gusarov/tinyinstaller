using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyNotepad;
using TinyInstaller;

namespace MyCustomUiInstaller
{
	partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				SpecLoader.FromAssembly<MainNotepadWindow>().Install();
				MessageBox.Show("SUCCESS", "", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
