using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TinyInstaller.CreateShourtcut
{
	class Static
	{
		const uint STGM_READ = 0;
		const int MAX_PATH = 260;

		[DllImport("shfolder.dll", CharSet = CharSet.Auto)] 
		internal static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath); 

	}
}