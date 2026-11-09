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

		[Fact]
		public void Install_ValidZip_ExtractsManifestAndDll()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan());
				string zipPath = ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"));
				PluginInfo pi = installer.ReadManifest(zipPath);

				installer.Install(zipPath, pi);

				Assert.True(File.Exists(folder.PathOf("my-plugin.json")));
				Assert.True(File.Exists(folder.PathOf("my-plugin", "my-plugin.dll")));
			}
		}

		[Fact]
		public void Install_ReplaceOld_RemovesStaleFiles()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Directory.CreateDirectory(folder.PathOf("my-plugin"));
				File.WriteAllText(folder.PathOf("my-plugin", "stale.txt"), "old");
				File.WriteAllText(folder.PathOf("my-plugin.json"), "old");

				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan());
				string zipPath = ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"));
				PluginInfo pi = installer.ReadManifest(zipPath);

				installer.Install(zipPath, pi);

				Assert.False(File.Exists(folder.PathOf("my-plugin", "stale.txt")));
				Assert.True(File.Exists(folder.PathOf("my-plugin", "my-plugin.dll")));
				Assert.Contains("my-plugin", File.ReadAllText(folder.PathOf("my-plugin.json")));
			}
		}

		[Fact]
		public void Install_NullPluginInfo_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan());
				string zipPath = ZipWith(folder,
					Entry("my-plugin.json", Manifest()),
					Entry(DllEntry, "MZ"));

				InvalidDataException ex = Assert.Throws<InvalidDataException>(() => installer.Install(zipPath, null));
				Assert.Equal("Manifest is null.", ex.Message);
			}
		}

		[Fact]
		public void IsInstalled_ChecksManifestAndFolder()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan());

				Assert.False(installer.IsInstalled("my-plugin"));

				Directory.CreateDirectory(folder.PathOf("my-plugin"));
				Assert.True(installer.IsInstalled("my-plugin"));
			}
		}

		[Fact]
		public void IsInstalled_ManifestOnly_True()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				File.WriteAllText(folder.PathOf("my-plugin.json"), Manifest());

				Assert.True(new PluginInstaller(folder.CreatePluginMan()).IsInstalled("my-plugin"));
			}
		}

		[Fact]
		public void Uninstall_ExistingManifest_RecyclesBoth()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Directory.CreateDirectory(folder.PathOf("my-plugin"));
				File.WriteAllText(folder.PathOf("my-plugin.json"), Manifest());
				List<string> moved = new List<string>();

				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan(), path => { moved.Add(path); return true; });
				PluginInstaller.UninstallResult result = installer.Uninstall("my-plugin");

				Assert.Equal(PluginInstaller.UninstallResult.Success, result);
				Assert.Equal(2, moved.Count);
				Assert.Contains(folder.PathOf("my-plugin.json"), moved);
				Assert.Contains(folder.PathOf("my-plugin"), moved);
			}
		}

		[Fact]
		public void Uninstall_MissingManifest_ReturnsNotFound()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				int calls = 0;
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan(), _ => { calls++; return true; });

				Assert.Equal(PluginInstaller.UninstallResult.NotFound, installer.Uninstall("my-plugin"));
				Assert.Equal(0, calls);
			}
		}

		[Fact]
		public void Uninstall_MissingManifestWithFolder_ReturnsNotFoundAndLeavesFolder()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Directory.CreateDirectory(folder.PathOf("my-plugin"));
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan(), _ => true);

				Assert.Equal(PluginInstaller.UninstallResult.NotFound, installer.Uninstall("my-plugin"));
				Assert.True(Directory.Exists(folder.PathOf("my-plugin")));
			}
		}

		[Fact]
		public void Uninstall_RecycleBinFails_ReturnsFailed()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Directory.CreateDirectory(folder.PathOf("my-plugin"));
				File.WriteAllText(folder.PathOf("my-plugin.json"), Manifest());
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan(), _ => false);

				Assert.Equal(PluginInstaller.UninstallResult.Failed, installer.Uninstall("my-plugin"));
			}
		}

		[Fact]
		public void Uninstall_FolderMoveFails_ReturnsFailed()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				Directory.CreateDirectory(folder.PathOf("my-plugin"));
				File.WriteAllText(folder.PathOf("my-plugin.json"), Manifest());
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan(), path => path.EndsWith(".json"));

				Assert.Equal(PluginInstaller.UninstallResult.Failed, installer.Uninstall("my-plugin"));
			}
		}

		[Fact]
		public void Uninstall_InvalidName_Throws()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				PluginInstaller installer = new PluginInstaller(folder.CreatePluginMan(), _ => true);

				InvalidDataException ex = Assert.Throws<InvalidDataException>(() => installer.Uninstall("a/b"));
				Assert.Equal(InvalidName, ex.Message);
			}
		}
	}
}
