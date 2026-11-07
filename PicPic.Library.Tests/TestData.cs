using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using PicPic.Library;

namespace PicPic.Library.Tests
{
	internal sealed class TempPluginsFolder : IDisposable
	{
		private readonly string oldPluginsFolder = PluginMan.pluginsFolder;
		private readonly string oldInputType = PluginMan.inputType;

		public string Path { get; }

		public TempPluginsFolder(bool create = true)
		{
			Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
			if (create) Directory.CreateDirectory(Path);
			PluginMan.pluginsFolder = Path;
			PluginMan.inputType = "bitmap";
		}

		public string PathOf(params string[] parts)
		{
			string path = Path;
			foreach (string part in parts) path = System.IO.Path.Combine(path, part);
			return path;
		}

		public void Dispose()
		{
			PluginMan.pluginsFolder = oldPluginsFolder;
			PluginMan.inputType = oldInputType;
			if (Directory.Exists(Path)) Directory.Delete(Path, true);
		}
	}

	internal static class TestData
	{
		private const string DefaultFunctions = @"[{ ""name"": ""invert"", ""title"": { ""en"": ""Invert colors"" }, ""type"": ""effect"", ""inputRequired"": true, ""dialog"": false, ""hideMainForm"": false, ""hotkey"": { ""ctrl"": false, ""shift"": false, ""alt"": false, ""key"": 0 } }]";

		public static string Manifest(
			string name = "my-plugin",
			int apiVer = 4,
			string inputType = "bitmap",
			string dllType = "csharp",
			string functions = DefaultFunctions,
			bool withAuthors = true)
		{
			string authors = withAuthors
				? @"[{ ""name"": ""You"", ""link"": ""https://github.com/you"" }]"
				: "null";

			return @"{
  ""name"": """ + name + @""",
  ""version"": ""1.0.0"",
  ""title"": ""My Plugin"",
  ""description"": { ""en"": ""Example plugin"" },
  ""link"": ""https://github.com/you/my-plugin"",
  ""authors"": " + authors + @",
  ""apiVer"": " + apiVer + @",
  ""inputType"": """ + inputType + @""",
  ""dllType"": """ + dllType + @""",
  ""functions"": " + functions + @"
}";
		}

		public static void CreateZip(string path, params KeyValuePair<string, string>[] entries)
		{
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))
			{
				foreach (KeyValuePair<string, string> entry in entries)
				{
					ZipArchiveEntry archiveEntry = zip.CreateEntry(entry.Key);
					using (StreamWriter writer = new StreamWriter(archiveEntry.Open()))
					{
						writer.Write(entry.Value);
					}
				}
			}
		}

		public static KeyValuePair<string, string> Entry(string name, string content)
		{
			return new KeyValuePair<string, string>(name, content);
		}
	}
}
