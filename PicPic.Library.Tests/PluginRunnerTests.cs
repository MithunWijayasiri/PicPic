using System;
using PicPic.Library;
using static PicPic.Library.Tests.TestData;

namespace PicPic.Library.Tests
{
	public class PluginRunnerTests
	{
		private static PluginRunner CreateRunner(TempPluginsFolder folder, string dllType, Func<string, string, PluginMan.RunFunction> cpp = null, Func<string, string, PluginMan.RunFunction> csharp = null)
		{
			PluginInfo pi = PluginInfo.FromJson(Manifest(dllType: dllType));
			return new PluginRunner(folder.CreatePluginMan(), pi, pi.functions[0], true, "en", cpp, csharp);
		}

		[Fact]
		public void GetEntryPointTypeName_ReplacesHyphens()
		{
			Assert.Equal("my_plugin.Main", PluginRunner.GetEntryPointTypeName(@"C:\plugins\my-plugin\my-plugin.dll"));
			Assert.Equal("myplugin.Main", PluginRunner.GetEntryPointTypeName(@"C:\plugins\myplugin\myplugin.dll"));
		}

		[Fact]
		public void Run_Cpp_DispatchesToCppEntryPoint()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				string seenPath = null;
				string seenFunc = null;
				object[] seenArgs = null;
				PluginRunner runner = CreateRunner(folder, "cpp",
					cpp: (dllPath, funcName) =>
					{
						seenPath = dllPath;
						seenFunc = funcName;
						return (input, path, darkMode, langCode, alwaysOnTop, secondaryArg) =>
						{
							seenArgs = new object[] { input, path, darkMode, langCode, alwaysOnTop, secondaryArg };
							return new string[] { "cpp-result" };
						};
					});

				object result = runner.Run("pixel", "photo.png", true, 42);

				Assert.Equal(new string[] { "cpp-result" }, result);
				Assert.Equal(folder.PathOf("my-plugin", "my-plugin.dll"), seenPath);
				Assert.Equal("invert", seenFunc);
				Assert.Equal(new object[] { "pixel", "photo.png", true, "en", true, 42 }, seenArgs);
			}
		}

		[Fact]
		public void Run_Csharp_DispatchesToCsharpEntryPoint()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				string seenPath = null;
				string seenFunc = null;
				PluginRunner runner = CreateRunner(folder, "csharp",
					csharp: (dllPath, funcName) =>
					{
						seenPath = dllPath;
						seenFunc = funcName;
						return (input, path, darkMode, langCode, alwaysOnTop, secondaryArg) => new string[] { "csharp-result" };
					});

				object result = runner.Run("pixel", "photo.png", false, null);

				Assert.Equal(new string[] { "csharp-result" }, result);
				Assert.Equal(folder.PathOf("my-plugin", "my-plugin.dll"), seenPath);
				Assert.Equal("invert", seenFunc);
			}
		}

		[Fact]
		public void Run_UnknownDllType_ReturnsNull()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				int calls = 0;
				PluginRunner runner = CreateRunner(folder, "unknown",
					cpp: (dllPath, funcName) => { calls++; return (input, path, darkMode, langCode, alwaysOnTop, secondaryArg) => new string[] { "x" }; },
					csharp: (dllPath, funcName) => { calls++; return (input, path, darkMode, langCode, alwaysOnTop, secondaryArg) => new string[] { "x" }; });

				Assert.Null(runner.Run("pixel", "photo.png", false, null));
				Assert.Equal(0, calls);
			}
		}

		[Fact]
		public void Run_InputRequired_NullInput_ReturnsNull()
		{
			using (TempPluginsFolder folder = new TempPluginsFolder())
			{
				int calls = 0;
				PluginRunner runner = CreateRunner(folder, "cpp",
					cpp: (dllPath, funcName) => { calls++; return (input, path, darkMode, langCode, alwaysOnTop, secondaryArg) => new string[] { "x" }; });

				Assert.Null(runner.Run(null, "photo.png", false, null));
				Assert.Equal(0, calls);
			}
		}
	}
}
