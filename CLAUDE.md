# PicPic

Windows image viewer. WinForms, C#, `net8.0-windows`. Fork of ModuleArt/quick-picture-viewer (QPV, GPL-3, unmaintained since 2023). Fully standalone: no submodules, no other repos. Third-party code only via NuGet or vendored source.

## Keep this file current

Update CLAUDE.md in the same change whenever project structure, build/run commands, dependencies, conventions, or status change. Status section must always reflect the current state of `master` + open work.

## Layout

| Path | What |
|:--|:--|
| `quick-picture-viewer/` | App project (`quick-picture-viewer.csproj`, WinExe). Renamed to `PicPic/` in rebrand. |
| `quick-picture-viewer/forms/` | Forms: `MainForm` (viewer), `SettingsForm`, `AboutForm`, `InfoForm`, `MiniViewForm`, `PrintForm`, `PluginManForm`, … |
| `quick-picture-viewer/typewrappers/` | Format decoders/encoders. Dispatch in `system/FileTypeMan.cs` (switch on extension). |
| `quick-picture-viewer/typewrappers/engines/` | In-repo engines: `WebpEngine.cs` (libwebp P/Invoke), `IcoEngine.cs`. |
| `quick-picture-viewer/languages/` | `lang_<code>.resx` UI strings (14 languages), read via `LangMan.Get(key)`. |
| `quick-picture-viewer/resources/` | Icons, images, `dlls/` (libwebp x86/x64, SimplePsd). |
| `QuickLibrary/` | Vendored from ModuleArt/quick-library @ `ffe09a3` (GPL-3). Custom `Qlib*` controls, `ThemeMan`, `LangMan`, `NativeMan`, `UpdateMan`, `PluginMan`, `SerializeMan`. Owned code now — edit freely. |
| `libs/SimplePsd.dll` | PSD decoder (HintPath reference). |
| `inno-setup/` | Old QPV installer script; to be rewritten. |
| `docs/index.html` | GitHub Pages "coming soon" page. |

## Build

Requires .NET 8 SDK.

```bash
dotnet restore
dotnet build
dotnet run --project quick-picture-viewer
```

## Conventions

- Version starts at `0.1.0` (beta). Tags `v0.1.0`.
- UI layout in `*.Designer.cs`, logic in code-behind. Edit Designer files by hand; no Visual Studio required.
- Don't enable `Nullable`/`ImplicitUsings` project-wide (legacy code floods warnings).
- Formats: decoder per type wrapper registered in `FileTypeMan`. Extension list currently duplicated in `MainForm.cs` (`GetCurrentFiles`, open-dialog filter, save switches) → keep in sync until centralized.
- New user-visible strings → add key to every `languages/lang_*.resx` (English text as fallback).
- No network calls to ModuleArt endpoints. No new heavy runtimes (Chromium, WebView2, Magick.NET).
- Keep GPL-3 and Module Art copyright notices; add PicPic notices alongside.
- One phase per branch + PR.

## Status

Done (branch `chore/initial-cleanup`, not yet merged):
- Removed upstream funding files, Chocolatey package, MSIX packages, committed installer.
- Update checker disabled (startup check + About button removed). `CheckForUpdates`/`SkippedVersion` settings and Settings checkbox still present, unused.
- New README and `docs/index.html`.
- quick-library vendored into `QuickLibrary/`; submodules and `.gitmodules` removed.

Not verified: no successful `dotnet build` on net8 yet.

Next:
1. Green net8 build: csproj cleanup (ClickOnce/Bootstrapper/manifest-signing leftovers, `Microsoft.AspNetCore.SystemWebAdapters`, Octokit 0.50 vs 9.0 mismatch, `UseWPF` check), `App.config` .NET Framework leftovers, libwebp DLLs copied to output, resx binary resources check.
2. Remove plugin system (`PluginMan`, `PluginManForm`, `PluginsCache` setting, BinaryFormatter in `SerializeMan`).
3. Rebrand to PicPic: namespace `quick_picture_viewer` → `PicPic`, exe `PicPic.exe`, folder/sln/csproj names, AssemblyInfo, registry keys `QuickPictureViewer` → `PicPic`, strings in code + 14 resx, About form, logo assets. Remove update checker fully.
4. CI (GitHub Actions publish win-x64) + releases.
5. UI: DWM dark title bar (attr 20), Zen HUD (auto-hiding floating dock + info chip, toolbar/statusbar hidden).
6. Codecs: centralize extensions, update libwebp, AVIF/HEIC/JXL via WIC.
7. Async preload + LRU cache, sorted directory listing.
8. New installer (own AppId, `PicPic.` ProgIDs, HKCU), in-app registry writes → HKCU.
9. Localization: auto-detect `zh`/`uk` etc.

## Known issues

- Plugin cache uses BinaryFormatter → throws on net8 WinForms (disabled by default).
- libwebp DLLs not copied by `dotnet build` → WebP fails to open.
- In-app "Open with" registry writes target HKCR (needs admin) and hardcode `quick-picture-viewer.exe`.
- First-run language detect never matches Chinese (`zh` vs `cn`) or Ukrainian (`uk` vs `ua`).
