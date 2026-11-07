# Plugins

Plugins add effects and tools to PicPic. API version 4, compatible with plugins for Quick Picture Viewer.

## Get plugins

### Quick Picture Viewer plugins

[qpv-plugins](https://github.com/ModuleArt/qpv-plugins): blur, grayscale, invert, rainbow, screenshot. Download `qpv-plugins.zip` from the [latest release](https://github.com/ModuleArt/qpv-plugins/releases/latest), then [install](#install) it.

### PicPic plugins

Coming soon.

## Install

Plugin Manager → browse for a `.zip`, or drop it on the window. PicPic asks for confirmation first: plugins run code with your permissions, install only plugins you trust.

- Plugins live in `%LOCALAPPDATA%\PicPic\plugins`.
- Installing a plugin with the same `name` replaces it. If the replace fails because the DLL is in use, restart PicPic and retry.
- Invalid or incompatible zips (bad manifest, `apiVer` not 4, no `<name>/<name>.dll`, files outside the plugin folder) are rejected with an error.

## Write your own plugin with an AI agent

C# only. Needs the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and a coding agent (Claude Code, Copilot, Cursor, …).

1. Give the agent this prompt, with your own name and functions:

   ```
   Write a PicPic plugin in C#. Follow this spec exactly (zip layout, manifest, C# plugin):
   https://raw.githubusercontent.com/MithunWijayasiri/PicPic/master/docs/plugins.md

   Plugin name: my-plugin
   Functions:
   - invert: inverts the image colors. Effects menu.

   Class library targeting net8.0-windows with <UseWindowsForms>true</UseWindowsForms>.
   Build in Release, then create my-plugin.zip: my-plugin.json at the root,
   the DLL (and any icons) in my-plugin/.
   ```

2. Install the zip from the Plugin Manager and try each function from the Effects or Tools menu.
3. Install fails or a function misbehaves → paste the error message back to the agent.
4. Reinstall the fixed zip. Same `name` → replaces the old one; restart PicPic first if the DLL is in use.

Icons are optional; without them the menu item shows text only.

## Reference

### Zip layout

```
my-plugin.zip
├─ my-plugin.json          manifest, exactly one .json at the root, named <name>.json
└─ my-plugin/              folder named <name>
   ├─ my-plugin.dll        <name>.dll
   ├─ invert.dark.png      icon per function: <function>.dark.png / <function>.light.png
   └─ invert.light.png     (<function>.png is the fallback)
```

### Manifest

```json
{
  "name": "my-plugin",
  "version": "1.0.0",
  "title": "My Plugin",
  "description": { "en": "Example plugin" },
  "link": "https://github.com/you/my-plugin",
  "authors": [{ "name": "You", "link": "https://github.com/you" }],
  "apiVer": 4,
  "inputType": "bitmap",
  "dllType": "csharp",
  "functions": [
    {
      "name": "invert",
      "title": { "en": "Invert colors" },
      "type": "effect",
      "inputRequired": true,
      "dialog": false,
      "hideMainForm": false,
      "hotkey": { "ctrl": false, "shift": false, "alt": false, "key": 0 }
    }
  ]
}
```

| Field | Value |
|:--|:--|
| `apiVer` | Must be `4`. |
| `inputType` | `bitmap`. |
| `dllType` | `csharp` (`cpp` is accepted but not supported, see below). |
| `title` | Plugin title, plain string. |
| `description`, `functions[].title` | Per language: `en`, `cn`, `fr`, `hu`, `ru`, `es`. Missing language uses `en`. |
| `functions[].type` | `effect` (Effects menu) or `tool` (Tools menu). |
| `functions[].inputRequired` | Menu item disabled while no image is open. |
| `functions[].dialog` | Appends `...` to the menu text. |
| `functions[].hideMainForm` | Hides the main window while the function runs. |
| `functions[].hotkey` | Modifiers plus `key` (`System.Windows.Forms.Keys` value). |

### C# plugin

Namespace = `<name>` with `-` replaced by `_`; must be a valid C# identifier, so `<name>` must not start with a digit. Class `Main` with a public parameterless constructor. One public method per manifest function:

```csharp
namespace my_plugin
{
    public class Main
    {
        public Bitmap invert(Bitmap bmp, string path, bool darkMode, string language, bool alwaysOnTop, Rectangle selection)
        {
            // return the new image, or null to leave the current image unchanged.
            // bmp is the live image: in-place edits survive a null return and are not marked as changed. Clone bmp before editing if the plugin may cancel.
        }
    }
}
```

- `path`: current file path, or null.
- `selection`: current selection, `Rectangle.Empty` when none.
- Target `net8.0-windows`. Dependencies go in `<name>/` next to the DLL.

### C++ plugin

Not supported. `dllType: "cpp"` plugins fail at run time on .NET 8: `PluginMan.RunFunction` returns `string[]`, which the native marshaller rejects (`MarshalDirectiveException`). Use `csharp`.
