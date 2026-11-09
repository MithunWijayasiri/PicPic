using System;
using System.Collections.Generic;
using System.IO;
using PicPic.Library;
using static PicPic.Library.Tests.TestData;

namespace PicPic.Library.Tests
{
	public class PluginInstallerTests
	{
		private const string DllEntry = "my-plugin/my-plugin.dll";
		private const string NoRootManifest = "Expected exactly one .json manifest at the zip root.";
		private const string InvalidName = "Invalid plugin name.";
		private const string Outside = "Zip contains files outside the plugin folder: ";

		private static void AssertInvalid(TempPluginsFolder folder, string zipPath, string message)
		{
			InvalidDataException ex = Assert.Throws<InvalidDataException>(() => new PluginInstaller(folder.CreatePluginMan()).ReadManifest(zipPath));
			Assert.Equal(message, ex.Message);
		}

		private static string ZipWith(TempPluginsFolder folder, params KeyValuePair<string, string>[] entries)
		{
			string path = folder.PathOf("plugin.zip");
			CreateZip(path, entries);
			return path;
		}

		[Fact]
		public void ReadManifest_ValidZip_ReturnsPluginInfo()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				PluginInfo pi = new PluginInstaller(folder.CreatePluginMan()).ReadManifest(ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ")));

				Assert.Equal("my-plugin", pi.name);
				Assert.Equal(4, pi.apiVer);
				Assert.Single(pi.functions);
			}
		}

		[Fact]
		public void ReadManifest_NoRootManifest_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder, Entry(DllEntry, "MZ")), NoRootManifest);
			}
		}

		[Fact]
		public void ReadManifest_TwoRootManifests_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry("second.json", Manifest()),
					Entry(DllEntry, "MZ")), NoRootManifest);
			}
		}

		[Fact]
		public void ReadManifest_ManifestOnlyInSubfolder_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin/my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ")), NoRootManifest);
			}
		}

		[Fact]
		public void ReadManifest_ManifestNameDiffersFromPluginName_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("other.json", Manifest()),
					Entry(DllEntry, "MZ")), "Manifest file name must be <name>.json.");
			}
		}

		[Theory]
		[InlineData("..", "...json")]
		[InlineData(".", "..json")]
		[InlineData("foo.", "foo..json")]
		[InlineData("a/b", "b.json")]
		[InlineData("a:b", "a:b.json")]
		[InlineData("a*b", "a*b.json")]
		[InlineData("", ".json")]
		public void ReadManifest_InvalidPluginName_Throws(string name, string manifestEntry)
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry(manifestEntry, Manifest(name: name)),
					Entry(name + "/" + name + ".dll", "MZ")), InvalidName);
			}
		}

		[Fact]
		public void ReadManifest_TraversalEntry_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"),
					Entry("my-plugin/../x.json", "{}")), Outside + "my-plugin/../x.json");
			}
		}

		[Fact]
		public void ReadManifest_AbsolutePathEntry_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"),
					Entry("C:/x.dll", "MZ")), Outside + "C:/x.dll");
			}
		}

		[Fact]
		public void ReadManifest_EntryOutsidePluginFolder_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"),
					Entry("readme.txt", "hello")), Outside + "readme.txt");
			}
		}

		[Fact]
		public void ReadManifest_EntryInOtherFolder_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"),
					Entry("other/x.dll", "MZ")), Outside + "other/x.dll");
			}
		}

		[Fact]
		public void ReadManifest_MissingDll_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder, Entry("my-plugin.json", Manifest())), "Zip has no " + DllEntry);
			}
		}

		[Fact]
		public void ReadManifest_DllEntryDifferentCase_Accepted()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				PluginInfo pi = new PluginInstaller(folder.CreatePluginMan()).ReadManifest(ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry("my-plugin/MY-PLUGIN.dll", "MZ")));

				Assert.Equal("my-plugin", pi.name);
			}
		}

		[Theory]
		[InlineData("null")]
		[InlineData("[]")]
		public void ReadManifest_NoFunctions_Throws(string functions)
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest(functions: functions)),
					Entry(DllEntry, "MZ")), "Manifest has no functions.");
			}
		}

		[Fact]
		public void ReadManifest_NoAuthors_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", Manifest(withAuthors: false)),
					Entry(DllEntry, "MZ")), "Manifest has no authors.");
			}
		}

		[Theory]
		[InlineData("{")]
		[InlineData("nul")]
		[InlineData("")]
		[InlineData("[]")]
		public void ReadManifest_InvalidJson_Throws(string json)
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				string path = ZipWith(folder,
					Entry("my-plugin.json", json),
					Entry(DllEntry, "MZ"));

				Assert.ThrowsAny<Exception>(() => new PluginInstaller(folder.CreatePluginMan()).ReadManifest(path));
			}
		}

		[Fact]
		public void ReadManifest_NullManifest_ThrowsInvalidDataException()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				AssertInvalid(folder, ZipWith(folder,
					Entry("my-plugin.json", "null"),
					Entry(DllEntry, "MZ")), "Manifest is null.");
			}
		}

		[Theory]
		[InlineData("plugin.zip", true)]
		[InlineData("plugin.ZIP", true)]
		[InlineData("plugin.rar", false)]
		[InlineData("plugin", false)]
		public void IsZip_ChecksExtension(string path, bool expected)
		{
			Assert.Equal(expected, PluginInstaller.IsZip(path));
		}

		[Fact]
		public void IsCompatible_ValidManifest_True()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Assert.True(new PluginInstaller(folder.CreatePluginMan()).IsCompatible(PluginInfo.FromJson(Manifest())));
			}
		}

		[Theory]
		[InlineData(3, "bitmap", "csharp", false)]
		[InlineData(4, "text", "csharp", false)]
		[InlineData(4, "bitmap", "unknown", false)]
		[InlineData(4, "bitmap", "cpp", true)]
		public void IsCompatible_ChecksApiVerInputTypeAndDllType(int apiVer, string inputType, string dllType, bool expected)
		{
			PluginInfo pi = PluginInfo.FromJson(Manifest(apiVer: apiVer, inputType: inputType, dllType: dllType));

			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Assert.Equal(expected, new PluginInstaller(folder.CreatePluginMan()).IsCompatible(pi));
			}
		}
	}
}
