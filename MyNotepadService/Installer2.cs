﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace MyNotepadService
{
	[RunInstaller(true)]
	public partial class Installer2 : System.Configuration.Install.Installer
	{
		public Installer2()
		{
			InitializeComponent();
		}
	}
}
