# PicPic

Windows image viewer. WinForms, C#, `net8.0-windows`. Fork of ModuleArt/quick-picture-viewer (QPV, GPL-3, unmaintained since 2023). Fully standalone: no submodules, no other repos. Third-party code only via NuGet or vendored source.

## Keep this file current

Architecture only: project structure, build/run commands, dependencies, conventions. Update in the same change when any of these change. No status, progress, roadmap, or issue lists here.

## Layout

| Path | What |
|:--|:--|
| `quick-picture-viewer/` | App project (`quick-picture-viewer.csproj`, WinExe). |
| `quick-picture-viewer/forms/` | Forms: `MainForm` (viewer), `SettingsForm`, `AboutForm`, `InfoForm`, `MiniViewForm`, `PrintForm`, `PluginManForm`, … |
| `quick-picture-viewer/typewrappers/` | Format decoders/encoders. Dispatch in `system/FileTypeMan.cs` (switch on extension). |
| `quick-picture-viewer/typewrappers/engines/` | In-repo engines: `WebpEngine.cs` (libwebp P/Invoke), `IcoEngine.cs`. |
| `quick-picture-viewer/languages/` | `lang_<code>.resx` UI strings (14 languages), read via `LangMan.Get(key)`. |
| `quick-picture-viewer/resources/` | Icons, images, `dlls/` (libwebp x86/x64, SimplePsd). |
| `QuickLibrary/` | Vendored from ModuleArt/quick-library @ `ffe09a3` (GPL-3). Custom `Qlib*` controls, `ThemeMan`, `LangMan`, `NativeMan`, `UpdateMan`, `PluginMan` (plugins in `%LOCALAPPDATA%\PicPic\plugins`, scanned from disk). Owned code now — edit freely. |
| `docs/plugins.md` | Plugin author/user guide (API v4, manifest, zip layout). |
| `assets/` | Brand icon pack (source of truth): SVGs, `png/`, `favicon.ico`, `picpic.ico` (app icon, 16–256). |
| `libs/SimplePsd.dll` | PSD decoder (HintPath reference). |
| `inno-setup/` | QPV-era Inno Setup installer script (hardcoded local paths; not used by any build). |
| `site/` | Website (GitHub Pages "coming soon" page). Deployed by `.github/workflows/pages.yml` on push to `master`; the workflow copies favicons from `assets/` into the build. |
| `.editorconfig` | LF, final newline, trimmed trailing whitespace; tabs size 4 for `*.cs`. Checked by `dotnet format whitespace`. |
| `.gitattributes` | `* text=auto eol=lf` (`*.sln` CRLF). |
| `.git-blame-ignore-revs` | One-time reformat commit(s); skipped by `git blame` (`git config blame.ignoreRevsFile .git-blame-ignore-revs`). |
| `.github/` | Workflows: `ci.yml` (build + `dotnet format whitespace --verify-no-changes` on PR + push to `master`, `windows-latest`), `pr-title.yml` (Conventional Commits PR title), `codeql.yml` (C# CodeQL: PR, push to `master`, weekly), `pages.yml` (site). `dependabot.yml`: weekly `nuget` + `github-actions`. |

## Build

Requires .NET 8 SDK.

```bash
dotnet restore
dotnet build
dotnet run --project quick-picture-viewer
```

Warnings are errors (`TreatWarningsAsErrors` in both csproj). Existing warnings listed per project in `WarningsNotAsErrors`; new warnings fail the build.

## Conventions

- Version starts at `0.1.0` (beta). Tags `v0.1.0`.
- UI layout in `*.Designer.cs`, logic in code-behind. Edit Designer files by hand; no Visual Studio required.
- Formatting: run `dotnet format whitespace` before pushing; CI fails on diffs. Reformat-only commits → add SHA to `.git-blame-ignore-revs`.
- Don't enable `Nullable`/`ImplicitUsings` project-wide (legacy code floods warnings).
- Formats: decoder per type wrapper registered in `FileTypeMan`. Extension list currently duplicated in `MainForm.cs` (`GetCurrentFiles`, open-dialog filter, save switches) → keep in sync until centralized.
- New user-visible strings → add key to every `languages/lang_*.resx` (English text as fallback).
- No network calls to ModuleArt endpoints. No new heavy runtimes (Chromium, WebView2, Magick.NET).
- Keep GPL-3 and Module Art copyright notices; add PicPic notices alongside.
- Icon change → update `assets/` first, regenerate `assets/picpic.ico`, then sync: `$this.Icon` in every form `.resx`, `logoPictureBox.Image` in `AboutForm.resx`. Exe icon + output `picpic.ico` come from `assets/picpic.ico` via csproj.
- PR titles follow Conventional Commits (squash merge uses PR title); checked by `pr-title.yml`.
- One phase per branch + PR.
