# Plugins

Plugins add effects and tools to PicPic. API version 4, compatible with plugins for Quick Picture Viewer such as [qpv-plugins](https://github.com/ModuleArt/qpv-plugins).

## Install

Plugin Manager → browse for a `.zip`, or drop it on the window. PicPic asks for confirmation first: plugins run code with your permissions, install only plugins you trust.

- Plugins live in `%LOCALAPPDATA%\PicPic\plugins`.
- Installing a plugin with the same `name` replaces it. If the replace fails because the DLL is in use, restart PicPic and retry.
- Invalid or incompatible zips (bad manifest, `apiVer` not 4, files outside the plugin folder) are rejected with an error.

## Zip layout

```
my-plugin.zip
├─ my-plugin.json          manifest, exactly one .json at the root, named <name>.json
└─ my-plugin/              folder named <name>
   ├─ my-plugin.dll        <name>.dll
   ├─ invert.dark.png      icon per function: <function>.dark.png / <function>.light.png
   └─ invert.light.png     (<function>.png is the fallback)
```

## Manifest

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
| `dllType` | `csharp` or `cpp`. |
| `title`, `description` | Per language: `en`, `cn`, `fr`, `hu`, `ru`, `es`. Other languages use `en`. |
| `functions[].type` | `effect` (Effects menu) or `tool` (Tools menu). |
| `functions[].inputRequired` | Menu item disabled while no image is open. |
| `functions[].dialog` | Appends `...` to the menu text. |
| `functions[].hideMainForm` | Hides the main window while the function runs. |
| `functions[].hotkey` | Modifiers plus `key` (`System.Windows.Forms.Keys` value). |

## C# plugin

Namespace = `<name>` with `-` replaced by `_`. Class `Main` with a public parameterless constructor. One public method per manifest function:

```csharp
namespace my_plugin
{
    public class Main
    {
        public Bitmap invert(Bitmap bmp, string path, bool darkMode, string language, bool alwaysOnTop, Rectangle selection)
        {
            // return the new image, or null to leave the current image unchanged
        }
    }
}
```

- `path`: current file path, or null.
- `selection`: current selection, `Rectangle.Empty` when none.
- Target `net8.0-windows`. Dependencies go in `<name>/` next to the DLL.

## C++ plugin

`dllType: "cpp"`: `<name>/<name>.dll` exports a function named like the manifest function, called with the arguments of `PluginMan.RunFunction` (`QuickLibrary/Managers/PluginMan.cs`).
