using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PicPic.Library
{
	public class PluginInstaller
	{
		public enum UninstallResult
		{
			Success,
			NotFound,
			Failed
		}

		private readonly PluginMan pluginMan;
		private readonly Func<string, bool> moveToRecycleBin;

		public PluginInstaller(PluginMan pluginMan, Func<string, bool> moveToRecycleBin = null)
		{
			this.pluginMan = pluginMan;
			this.moveToRecycleBin = moveToRecycleBin ?? FileMan.MoveFileOrFolderToRecycleBin;
		}

		private string GetPluginFolder(string pluginName)
		{
			return Path.Combine(pluginMan.pluginsFolder, pluginName);
		}

		private string GetManifestPath(string pluginName)
		{
			return GetPluginFolder(pluginName) + ".json";
		}

		/// <summary>True if the path has a <c>.zip</c> extension, case-insensitive.</summary>
		public static bool IsZip(string pathToZip)
		{
			return string.Equals(Path.GetExtension(pathToZip), ".zip", StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>True if the plugin targets the instance's <see cref="PluginMan.apiVer"/>, bitmap input and a <c>csharp</c> or <c>cpp</c> DLL.</summary>
		public bool IsCompatible(PluginInfo pi)
		{
			return pi.apiVer == pluginMan.apiVer && pi.inputType == "bitmap" && (pi.dllType == "csharp" || pi.dllType == "cpp");
		}

		/// <summary>Reads the root <c>&lt;name&gt;.json</c> manifest and checks the zip extracts only into <c>&lt;name&gt;/</c> under the instance's <see cref="PluginMan.pluginsFolder"/>.</summary>
		/// <exception cref="InvalidDataException">Manifest missing, null or incomplete; invalid name; DLL missing; or an entry outside the plugin folder.</exception>
		/// <remarks>Malformed JSON throws the parser's own exception.</remarks>
		public PluginInfo ReadManifest(string pathToZip)
		{
			using (ZipArchive zip = ZipFile.OpenRead(pathToZip))
			{
				char[] separators = { '/', '\\' };
				ZipArchiveEntry[] manifests = zip.Entries
					.Where(e => e.FullName.IndexOfAny(separators) < 0 && e.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
					.ToArray();
				if (manifests.Length != 1) throw new InvalidDataException("Expected exactly one .json manifest at the zip root.");

				PluginInfo pi;
				using (StreamReader reader = new StreamReader(manifests[0].Open()))
				{
					pi = PluginInfo.FromJson(reader.ReadToEnd());
				}

				if (pi == null) throw new InvalidDataException("Manifest is null.");
				if (string.IsNullOrEmpty(pi.name) || pi.name != Path.GetFileName(pi.name) || pi.name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
					throw new InvalidDataException("Invalid plugin name.");
				string root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(pluginMan.pluginsFolder));
				string folder = Path.GetFullPath(GetPluginFolder(pi.name));
				if (Path.GetFileName(folder) != pi.name || Path.GetDirectoryName(folder) != root)
					throw new InvalidDataException("Invalid plugin name.");
				if (manifests[0].FullName != pi.name + ".json") throw new InvalidDataException("Manifest file name must be <name>.json.");
				if (pi.functions == null || pi.functions.Length == 0) throw new InvalidDataException("Manifest has no functions.");
				if (pi.authors == null) throw new InvalidDataException("Manifest has no authors.");

				string dllEntry = pi.name + "/" + pi.name + ".dll";
				if (!zip.Entries.Any(e => string.Equals(e.FullName.Replace('\\', '/'), dllEntry, StringComparison.OrdinalIgnoreCase)))
					throw new InvalidDataException("Zip has no " + dllEntry);

				foreach (ZipArchiveEntry entry in zip.Entries)
				{
					string destination = Path.GetFullPath(Path.Combine(root, entry.FullName));
					if (entry != manifests[0] && !destination.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
						throw new InvalidDataException("Zip contains files outside the plugin folder: " + entry.FullName);
				}
				return pi;
			}
		}

		/// <summary>True if <c>&lt;name&gt;.json</c> or <c>&lt;name&gt;/</c> exists under <see cref="PluginMan.pluginsFolder"/>.</summary>
		/// <exception cref="InvalidDataException">Plugin name is invalid.</exception>
		public bool IsInstalled(string pluginName)
		{
			if (string.IsNullOrEmpty(pluginName) || pluginName != Path.GetFileName(pluginName) || pluginName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				throw new InvalidDataException("Invalid plugin name.");
			return File.Exists(GetManifestPath(pluginName)) || Directory.Exists(GetPluginFolder(pluginName));
		}

		/// <summary>Deletes any existing <c>&lt;name&gt;/</c> folder and <c>&lt;name&gt;.json</c>, then extracts the zip into <see cref="PluginMan.pluginsFolder"/>.</summary>
		/// <param name="pathToZip">Path to the zip file, already checked with <see cref="IsZip"/>.</param>
		/// <param name="pi">Plugin info, already checked with <see cref="ReadManifest"/> and <see cref="IsCompatible"/>.</param>
		/// <exception cref="InvalidDataException">Plugin info is null or has an invalid name.</exception>
		/// <remarks>Validation via <see cref="ReadManifest"/> and <see cref="IsCompatible"/> is the caller's job; this method assumes a validated <paramref name="pi"/>.</remarks>
		public void Install(string pathToZip, PluginInfo pi)
		{
			if (pi == null) throw new InvalidDataException("Manifest is null.");
			if (string.IsNullOrEmpty(pi.name) || pi.name != Path.GetFileName(pi.name) || pi.name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				throw new InvalidDataException("Invalid plugin name.");
			string pluginFolder = GetPluginFolder(pi.name);
			string manifestPath = GetManifestPath(pi.name);
			if (Directory.Exists(pluginFolder)) Directory.Delete(pluginFolder, true);
			if (File.Exists(manifestPath)) File.Delete(manifestPath);
			ZipFile.ExtractToDirectory(pathToZip, pluginMan.pluginsFolder);
		}

		/// <summary>Recycles <c>&lt;name&gt;.json</c> and <c>&lt;name&gt;/</c> for the given plugin name.</summary>
		/// <exception cref="InvalidDataException">Plugin name is invalid.</exception>
		/// <returns><see cref="UninstallResult.NotFound"/> when the manifest is absent; <see cref="UninstallResult.Failed"/> when a recycle-bin move fails; otherwise <see cref="UninstallResult.Success"/>.</returns>
		public UninstallResult Uninstall(string pluginName)
		{
			if (string.IsNullOrEmpty(pluginName) || pluginName != Path.GetFileName(pluginName) || pluginName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				throw new InvalidDataException("Invalid plugin name.");
			string pluginFolder = GetPluginFolder(pluginName);
			string manifestPath = GetManifestPath(pluginName);
			if (!File.Exists(manifestPath)) return UninstallResult.NotFound;
			if (!moveToRecycleBin(manifestPath)) return UninstallResult.Failed;
			if (Directory.Exists(pluginFolder) && !moveToRecycleBin(pluginFolder)) return UninstallResult.Failed;
			return UninstallResult.Success;
		}
	}
}
