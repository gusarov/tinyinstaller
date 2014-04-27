using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TinyInstaller.Internal
{
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class InstallUtilsAssemblyAttribute : Attribute
	{
		//readonly string _assemblyFileId;
		readonly Type _assemblyType;

		public InstallUtilsAssemblyAttribute(Type assembly)
		{
			_assemblyType = assembly;
		}

//		public InstallUtilsAssemblyAttribute(string assemblyFileId)
//		{
//			_assemblyFileId = assemblyFileId;
//		}

//		public string AssemblyFileId
//		{
//			get { return _assemblyFileId; }
//		}

		public Type AssemblyType
		{
			get { return _assemblyType; }
		}

		public Assembly Assembly
		{
			get { return AssemblyType.Assembly; }
		}
	}
}