using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PicPic.Library
{
	public class PluginMan
	{
		public static readonly string DefaultPluginsFolder = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"PicPic",
			"plugins"
		);

		public readonly string pluginsFolder;
		public readonly string inputType;
		public readonly int apiVer;

		public PluginMan(string pluginsFolder, string inputType, int apiVer = 4)
		{
			this.pluginsFolder = pluginsFolder;
			this.inputType = inputType;
			this.apiVer = apiVer;
		}

		public static PluginMan CreateDefault()
		{
			return new PluginMan(DefaultPluginsFolder, "bitmap");
		}

		public delegate string[] RunFunction(
			object input = null,
			string path = null,
			bool darkMode = false,
			string language = "en",
			bool alwaysOnTop = false,
			object secondaryArg = null
		);

		public delegate void PluginOutput(object sender, OutputEventArgs oea);

		public class OutputEventArgs : EventArgs
		{
			public object input { get; set; }
		}

		public PluginInfo[] GetPlugins()
		{
			List<PluginInfo> plugins = new List<PluginInfo>();
			DirectoryInfo di = new DirectoryInfo(pluginsFolder);
			if (di.Exists)
			{
				FileInfo[] files = di.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					if (Path.GetExtension(files[i].Name) == ".json")
					{
						try
						{
							PluginInfo pi = PluginInfo.FromJson(File.ReadAllText(Path.Combine(di.FullName, files[i].Name)));
							// Name must match the manifest file name; delete and icon lookups rely on it.
							if (pi != null && pi.name == Path.GetFileNameWithoutExtension(files[i].Name) && pi.apiVer == apiVer && pi.inputType == inputType) plugins.Add(pi);
						}
						catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is Utf8Json.JsonParsingException || ex is IndexOutOfRangeException)
						{
							// Unreadable or invalid manifest: skip it so other plugins still load.
						}
					}
					files[i] = null;
				}
			}
			return plugins.ToArray();
		}

		// Copies the icon into memory so the file is not locked and the plugin can be replaced.
		private static Image LoadIcon(string path)
		{
			using (FileStream fs = File.OpenRead(path))
			using (Image img = Image.FromStream(fs))
			{
				return new Bitmap(img);
			}
		}

		public Image GetPluginIcon(string pluginName, string funcName, bool darkMode)
		{
			if (darkMode)
			{
				string path = Path.Combine(pluginsFolder, pluginName, funcName + ".dark.png");
				if (File.Exists(path)) return LoadIcon(path);

				path = Path.Combine(pluginsFolder, pluginName, funcName + ".png");
				if (File.Exists(path)) return LoadIcon(path);

				return null;
			}
			else
			{
				string path = Path.Combine(pluginsFolder, pluginName, funcName + ".light.png");
				if (File.Exists(path)) return LoadIcon(path);

				path = Path.Combine(pluginsFolder, pluginName, funcName + ".png");
				if (File.Exists(path)) return LoadIcon(path);

				return null;
			}
		}
	}
}
