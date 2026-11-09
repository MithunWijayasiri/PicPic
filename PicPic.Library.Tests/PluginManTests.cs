using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PicPic.Library;
using static PicPic.Library.Tests.TestData;

namespace PicPic.Library.Tests
{
	public class PluginManTests
	{
		private static void WriteIcon(string path, int size = 1)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			using (Bitmap bitmap = new Bitmap(size, size))
			{
				bitmap.Save(path, ImageFormat.Png);
			}
		}

		[Fact]
		public void GetPlugins_ReturnsOnlyValidManifests()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				File.WriteAllText(folder.PathOf("valid.json"), Manifest(name: "valid"));
				File.WriteAllText(folder.PathOf("empty.json"), "");
				File.WriteAllText(folder.PathOf("invalid.json"), "{");
				File.WriteAllText(folder.PathOf("nul.json"), "nul");
				File.WriteAllText(folder.PathOf("array.json"), "[]");
				File.WriteAllText(folder.PathOf("null.json"), "null");
				File.WriteAllText(folder.PathOf("wrong-api.json"), Manifest(name: "wrong-api", apiVer: 3));
				File.WriteAllText(folder.PathOf("wrong-input.json"), Manifest(name: "wrong-input", inputType: "text"));
				File.WriteAllText(folder.PathOf("mismatch.json"), Manifest(name: "valid"));

				PluginMan pluginMan = folder.CreatePluginMan();
				PluginInfo[] plugins = pluginMan.GetPlugins();

				Assert.Single(plugins);
				Assert.Equal("valid", plugins[0].name);
			}
		}

		[Fact]
		public void GetPlugins_MissingFolder_ReturnsEmpty()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder(create: false))
			{
				Assert.Empty(folder.CreatePluginMan().GetPlugins());
			}
		}

		[Theory]
		[InlineData("fr", "fr")]
		[InlineData("de", "en")]
		public void MultilangString_Get_FallsBackToEnglish(string langCode, string expected)
		{
			PluginInfo.MultilangString text = new PluginInfo.MultilangString { en = "en", fr = "fr" };

			Assert.Equal(expected, text.Get(langCode));
		}

		[Fact]
		public void MultilangString_Get_EmptyLanguageFallsBackToEnglish()
		{
			PluginInfo.MultilangString text = new PluginInfo.MultilangString { en = "en", fr = "" };

			Assert.Equal("en", text.Get("fr"));
		}

		[Fact]
		public void GetPluginIcon_CopiesIconToMemory()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				string iconPath = folder.PathOf("my-plugin", "invert.png");
				WriteIcon(iconPath);

				using (Image icon = folder.CreatePluginMan().GetPluginIcon("my-plugin", "invert", false))
				{
					Assert.NotNull(icon);
					File.Delete(iconPath);
					Assert.Equal(1, icon.Width);
				}
			}
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void GetPluginIcon_PrefersThemeVariant(bool darkMode)
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				WriteIcon(folder.PathOf("my-plugin", "invert.png"));
				WriteIcon(folder.PathOf("my-plugin", "invert.dark.png"), 2);
				WriteIcon(folder.PathOf("my-plugin", "invert.light.png"), 3);

				using (Image icon = folder.CreatePluginMan().GetPluginIcon("my-plugin", "invert", darkMode))
				{
					Assert.Equal(darkMode ? 2 : 3, icon.Width);
				}
			}
		}

		[Fact]
		public void GetPluginIcon_FallsBackToPlainIcon()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				WriteIcon(folder.PathOf("my-plugin", "invert.png"));

				PluginMan pluginMan = folder.CreatePluginMan();
				using (Image darkIcon = pluginMan.GetPluginIcon("my-plugin", "invert", true))
				using (Image lightIcon = pluginMan.GetPluginIcon("my-plugin", "invert", false))
				{
					Assert.NotNull(darkIcon);
					Assert.NotNull(lightIcon);
				}
			}
		}

		[Fact]
		public void GetPluginIcon_NoIcon_ReturnsNull()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Assert.Null(folder.CreatePluginMan().GetPluginIcon("my-plugin", "invert", false));
			}
		}
	}
}
