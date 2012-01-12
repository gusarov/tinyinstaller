using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

// namespace TinyInstaller.MetaCreator
//{
	public static class EmbedReferences
	{
		public static string MetaBuild()
		{
			var sb = new StringBuilder();

			sb.AppendLine("\\\\ hello");

			foreach (var proj in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*proj"))
			{
				sb.AppendLine("\\\\ file: " + proj);

				var xml = new XmlDocument();
				xml.Load(proj);
				var nm = new XmlNamespaceManager(xml.NameTable);
				nm.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

				foreach (var reference in xml.SelectNodes(@"x:Project\x:ItemGroup\x:Reference"))
				{
					sb.AppendLine("\\\\" + reference);
				}
				foreach (var reference in xml.SelectNodes(@"x:Project\x:ItemGroup\x:Reference"))
				{
					sb.AppendLine("\\\\" + reference);
				}
			}
			return sb.ToString();
		}
	}
//}
