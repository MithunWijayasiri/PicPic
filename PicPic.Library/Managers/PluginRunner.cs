using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PicPic.Library
{
	public class PluginRunner
	{
		private readonly string dllPath;
		private readonly PluginInfo pi;
		private readonly PluginInfo.Function func;
		private readonly bool darkMode;
		private readonly string langCode;
		private readonly Func<string, string, PluginMan.RunFunction> cppEntryPoint;
		private readonly Func<string, string, PluginMan.RunFunction> csharpEntryPoint;

		public PluginRunner(
			PluginMan pluginMan,
			PluginInfo pi,
			PluginInfo.Function func,
			bool darkMode,
			string langCode,
			Func<string, string, PluginMan.RunFunction> cppEntryPoint = null,
			Func<string, string, PluginMan.RunFunction> csharpEntryPoint = null
		)
		{
			this.pi = pi;
			this.func = func;
			this.darkMode = darkMode;
			this.langCode = langCode;
			dllPath = Path.Combine(pluginMan.pluginsFolder, pi.name, pi.name + ".dll");
			this.cppEntryPoint = cppEntryPoint ?? DefaultCppEntryPoint;
			this.csharpEntryPoint = csharpEntryPoint ?? DefaultCsharpEntryPoint;
		}

		/// <summary>Gets the csharp entry-point type name for a DLL path: file name without extension, hyphens replaced with underscores, plus <c>.Main</c>.</summary>
		public static string GetEntryPointTypeName(string dllPath)
		{
			return Path.GetFileNameWithoutExtension(dllPath).Replace("-", "_") + ".Main";
		}

		private static PluginMan.RunFunction DefaultCppEntryPoint(string dllPath, string funcName)
		{
			IntPtr pluginPtr = NativeMan.LoadLibrary(dllPath);
			IntPtr funcPtr = NativeMan.GetProcAddressOrdinal(pluginPtr, funcName);
			return Marshal.GetDelegateForFunctionPointer<PluginMan.RunFunction>(funcPtr);
		}

		private static PluginMan.RunFunction DefaultCsharpEntryPoint(string dllPath, string funcName)
		{
			Assembly assembly = Assembly.LoadFrom(dllPath);
			Type type = assembly.GetType(GetEntryPointTypeName(dllPath));
			MethodInfo method = type.GetMethod(funcName);
			return (input, path, darkMode, langCode, alwaysOnTop, secondaryArg) =>
			{
				object instance = Activator.CreateInstance(type);
				return (string[])method.Invoke(instance, new object[] { input, path, darkMode, langCode, alwaysOnTop, secondaryArg });
			};
		}

		/// <summary>Runs the plugin function. Returns null when the input is required but missing, or the DLL type is unknown.</summary>
		public object Run(object input, string path, bool alwaysOnTop, object secondaryArg)
		{
			if (func.inputRequired && input == null) return null;

			if (pi.dllType == "cpp")
			{
				PluginMan.RunFunction callback = cppEntryPoint(dllPath, func.name);
				return callback(input, path, darkMode, langCode, alwaysOnTop, secondaryArg);
			}
			else if (pi.dllType == "csharp")
			{
				PluginMan.RunFunction callback = csharpEntryPoint(dllPath, func.name);
				return callback(input, path, darkMode, langCode, alwaysOnTop, secondaryArg);
			}
			return null;
		}
	}
}
