# Product

<!-- impeccable:product-schema 1 -->

## Platform

web

## Users

Primary: everyday Windows desktop users who open images frequently and want a fast, minimal viewer without opening a heavyweight editor. Their situation is routine viewing, browsing a folder, and quick adjustments while working on Windows.

Other audience: photographers, designers, and plugin users who need broad format coverage (including camera RAW and Photoshop files) and extensibility. They matter, but they do not override the everyday-user priority.

## Product Purpose

PicPic is a fast, minimal image viewer for Windows. It makes opening and browsing images instant and distraction-free, with basic editing, window modes, slideshow, print/PDF, and wallpaper setting built in. Success means a user can open an image, navigate, tweak or present it, and get back to work without friction or extra UI.

## Positioning

Lead claim: fast, minimal, distraction-free viewing with broad native format coverage. PicPic opens 19 formats natively, provides basic editing without a heavyweight editor, and loads Quick Picture Viewer-compatible plugins. Free and open source under GPL-3.0. In beta.

Do not claim performance benchmarks, testimonials, customer counts, or other performance numbers: none are on hand.

## Operating Context

- Distribution: beta builds on GitHub Releases as two zips: a self-contained zip (runs with nothing else installed) and a small framework-dependent zip (requires the .NET 8 Desktop Runtime).
- The site is a static GitHub Pages site (`site/index.html` with `site/styles.css` and `site/app.js`). Site content is static with no interactive demos; the real PicPic runs on Windows.
- Plugin context: users install existing community plugins or write plugins using the plugin guide (`docs/plugins.md`). Plugins are scanned from `%LOCALAPPDATA%\PicPic\plugins`.

## Capabilities and Constraints

Capabilities:
- Opens .png, .jpg, .jpeg, .jpe, .jfif, .exif, .gif, .bmp, .dib, .rle, .tiff, .tif, .ico, .webp, .svg, .dds, .tga, .psd, .cr2 (19 formats).
- Basic editing: rotate, flip, crop, convert file type.
- Window modes: picture-in-picture, frameless, always-on-top.
- Slideshow and checkerboard background for transparent files.
- Print, export to PDF, set as desktop wallpaper, image details view, dark mode.
- Loads plugins compatible with Quick Picture Viewer (API v4).

Constraints:
- Windows only; requires the .NET 8 SDK to build (`net8.0-windows`, WinForms).
- Beta status. Releases are manual GitHub Actions workflow releases. Builds are unsigned (SmartScreen warning on first run).
- GPL-3.0 license. Fork of Quick Picture Viewer by Module Art (unmaintained upstream); Module Art attribution must be preserved.
- No new heavy runtimes (Chromium, WebView2, Magick.NET). Third-party code only via NuGet or vendored source.
- New user-visible strings in the app must be added to every `languages/lang_*.resx` file.
- Undecided: installer packaging, `win-arm64` support, and support for AVIF / HEIC / JXL. Do not present these as shipped.

## Brand Commitments

- Name: PicPic. Display name in the app: `PicPic - Image Viewer`; executable: `PicPic.exe`.
- Existing site copy lines: "View image, instantly." and "Less interface, more image." These are current copy, not independently verified claims.
- Assets: `assets/picpic-icon.svg`, `assets/picpic-mark.svg`, `assets/picpic.ico` (app icon), `site/assets/demo-neon-dusk.jpg`, `site/assets/demo-prism-study.jpg`, `site/assets/demo-midnight-avenue.jpg`, `site/assets/og-card.png`.
- Existing name, voice, and assets only. No other binding visual commitments are recorded here.

## Evidence on Hand

- `README.md` (features, build steps, bug report process, credits).
- `site/index.html`, `site/styles.css`, `site/app.js` (product copy, static feature and plugin content, format groups).
- `docs/plugins.md` (plugin author/user guide).
- Release evidence: `v0.1.0` and `v0.1.1` prereleases on GitHub Releases (self-contained single-file zip and framework-dependent zip).
- Absences to respect: no testimonials, no customer or download counts, no benchmark figures, no press coverage, no Code of Conduct or support channel beyond GitHub Issues. Future work must not fabricate these.

## Product Principles

1. Instant by default: opening and browsing images must feel immediate; nothing should block the viewer.
2. Minimal interface, more image: every control must earn its place; distraction-free viewing wins over feature density.
3. Breadth without bloat: open as many formats natively as possible while keeping the app small and dependency-light.
4. Editing is basic on purpose: quick tweaks and conversions only; PicPic is not a heavyweight editor.
5. Extend, don't fork the ecosystem: stay compatible with Quick Picture Viewer plugins and credit upstream contributors.
