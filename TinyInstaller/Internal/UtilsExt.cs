using System.Linq;
using System.Reflection;

namespace TinyInstaller
{
	static class UtilsExt
	{
		public static T Attribute<T>(this ICustomAttributeProvider attributeProvider)
		{
			return (T)attributeProvider.GetCustomAttributes(typeof(T), false).SingleOrDefault();
		}

	}
}