# PicPic

Windows image viewer. WinForms, C#, `net8.0-windows`. Fork of ModuleArt/quick-picture-viewer (QPV, GPL-3, unmaintained since 2023). Fully standalone: no submodules, no other repos. Third-party code only via NuGet or vendored source.

## Keep this file current

Architecture only: project structure, build/run commands, dependencies, conventions. Update in the same change when any of these change. No status, progress, roadmap, or issue lists here.

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

