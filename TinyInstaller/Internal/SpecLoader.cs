using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TinyInstaller.Internal
{
	public static class SpecLoader
	{
		public static InstallationSpecification FromAssembly<TTargetAssembly>()
		{
			return FromAssembly(typeof(TTargetAssembly));
		}

		public static InstallationSpecification FromAssembly(string targetAssemblyFile)
		{
			return FromAssembly(Assembly.LoadFile(targetAssemblyFile));
		}

		public static InstallationSpecification FromAssembly(Type targetAssembly)
		{
			return FromAssembly(targetAssembly.Assembly);
		}

		public static InstallationSpecification FromAssembly(Assembly targetAssembly)
		{
			// var specType = typeof (InstallationSpecification);
			// var spec = targetAssembly.GetTypes().FirstOrDefault(x => specType.IsAssignableFrom(x.BaseType));

			var isUserMode = targetAssembly.Attribute<InstallUserModeAttribute>();
			var identityAttribute = targetAssembly.Attribute<InstallerIdentityAttribute>();
			var titleAttribute = targetAssembly.Attribute<AssemblyTitleAttribute>();
			var productAttribute = targetAssembly.Attribute<AssemblyProductAttribute>();

			var location_ = string.IsNullOrEmpty(targetAssembly.Location) ? Assembly.GetEntryAssembly().Location : targetAssembly.Location;

			var fileName = Path.GetFileNameWithoutExtension(location_);

			var identity = string.Empty;

			if (string.IsNullOrEmpty(identity) && identityAttribute != null)
			{
				identity = identityAttribute.Identity;
			}

			if (string.IsNullOrEmpty(identity) && productAttribute != null)
			{
				identity = productAttribute.Product;
			}

			if (string.IsNullOrEmpty(identity) && titleAttribute != null)
			{
				identity = titleAttribute.Title;
			}

			if (string.IsNullOrEmpty(identity))
			{
				identity = fileName;
			}

			var container = GetFileContainer(targetAssembly);
			var specAssembly = GetAssemblyFileId(targetAssembly, container);
			var spec = new InstallationSpecification
			{
				Identity = identity,
				FilesToInstall = container,
				SpecAssembly = specAssembly,
				Publisher = targetAssembly.Attribute<AssemblyCompanyAttribute>().Company,
				DisplayVersion = targetAssembly.Attribute<AssemblyFileVersionAttribute>().Version,
				AssembliesForInstallUtils = GetAssembliesForInstallUtils(container, targetAssembly).ToArray(),
			};
//			if (isUserMode!=null)
//			{
//				spec.IsUserMode = isUserMode.IsUserMode;
//			}
			return spec;
		}

		static IEnumerable<InstallableFileInfo> GetAssembliesForInstallUtils(IFileContainer container, Assembly target)
		{
			var attribs = target.GetCustomAttributes(typeof(InstallUtilsAssemblyAttribute), true);
			var attribs2 = attribs.Cast<InstallUtilsAssemblyAttribute>().ToArray();
			var targets = attribs2.Select(x => GetTargetByInstallUtilsAssemblyAttribute(x, container)).ToList();

			return targets;
		}

		static InstallableFileInfo GetTargetByInstallUtilsAssemblyAttribute(InstallUtilsAssemblyAttribute attr, IFileContainer container)
		{
			return container
				.Where(x=>ValidExtension(x.FileId))
				.Single(x => x.FileName.StartsWith(attr.Assembly.FullName.Substring(0, attr.Assembly.FullName.IndexOf(','))));
		}

		static IFileContainer GetFileContainer(Assembly targetAssembly)
		{
			if (IsEmbeded(targetAssembly))
			{
				return new EmbededResourceFileContainer(Assembly.GetEntryAssembly());
			}
			return new FsFileContainer(Path.GetDirectoryName(targetAssembly.Location));
		}

		static string GetAssemblyFileId(Assembly assembly, IFileContainer container)
		{
			if (IsEmbeded(assembly))
			{
				return container.Single(x => Path.GetFileNameWithoutExtension(x.FileId) == assembly.GetName().Name && ValidExtension(x.FileId)).FileId;
			}
			return Path.GetFileName(assembly.Location);
		}

		static bool ValidExtension(string fileId)
		{
			var ext = Path.GetExtension(fileId);
			return ".dll".Equals(ext, StringComparison.InvariantCultureIgnoreCase) || ".exe".Equals(ext, StringComparison.InvariantCultureIgnoreCase);
		}

		static bool IsEmbeded(Assembly targetAssembly)
		{
			return string.IsNullOrEmpty(targetAssembly.Location);
		}
	}
}
