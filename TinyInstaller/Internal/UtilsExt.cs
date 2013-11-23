using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace TinyInstaller.Internal
{
	static class UtilsExt
	{
		public static T Attribute<T>(this ICustomAttributeProvider attributeProvider)
		{
			return (T)attributeProvider.GetCustomAttributes(typeof(T), false).SingleOrDefault();
		}

	}
}