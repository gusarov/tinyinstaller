using System.Linq;
using System.Collections.Generic;
using System;

namespace TinyInstaller.Actions.CreateShourtcut
{
	[Flags] 
	enum SLGP_FLAGS 
	{ 
		/// <summary>Retrieves the standard short (8.3 format) file name</summary> 
		SLGP_SHORTPATH = 0x1, 
		/// <summary>Retrieves the Universal Naming Convention (UNC) path name of the file</summary> 
		SLGP_UNCPRIORITY = 0x2, 
		/// <summary>Retrieves the raw path name. A raw path is something that might not exist and may include environment variables that need to be expanded</summary> 
		SLGP_RAWPATH = 0x4 
	}
}