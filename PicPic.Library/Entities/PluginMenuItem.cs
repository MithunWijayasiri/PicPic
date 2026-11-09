using System;
using System.Windows.Forms;

namespace PicPic.Library
{
	public class PluginMenuItem : ToolStripMenuItem
	{
		private PluginInfo pi;
		private PluginInfo.Function func;
		private bool darkMode;
		private string langCode;
		private PluginMan pluginMan;
		private readonly PluginRunner runner;

		public PluginMenuItem(
			PluginInfo pi,
			PluginInfo.Function func,
			string langCode,
			bool darkMode,
			PluginMan pluginMan
		)
		{
			this.pi = pi;
			this.func = func;
			this.darkMode = darkMode;
			this.langCode = langCode;
			this.pluginMan = pluginMan;
			runner = new PluginRunner(pluginMan, pi, func, darkMode, langCode);

			Text = func.title.Get(langCode);

			if (func.hotkey.ctrl)
			{
				if (func.hotkey.shift)
				{
					if (func.hotkey.alt) ShortcutKeys = Keys.Control | Keys.Shift | Keys.Alt | (Keys)func.hotkey.key;
					else ShortcutKeys = Keys.Control | Keys.Shift | (Keys)func.hotkey.key;
				}
				else
				{
					if (func.hotkey.alt) ShortcutKeys = Keys.Control | Keys.Alt | (Keys)func.hotkey.key;
					else ShortcutKeys = Keys.Control | (Keys)func.hotkey.key;
				}
			}
			else if (func.hotkey.shift)
			{
				if (func.hotkey.alt) ShortcutKeys = Keys.Shift | Keys.Alt | (Keys)func.hotkey.key;
				else ShortcutKeys = Keys.Shift | (Keys)func.hotkey.key;
			}
			else if (func.hotkey.alt)
			{
				ShortcutKeys = Keys.Alt | (Keys)func.hotkey.key;
			}
			else
			{
				ShortcutKeys = (Keys)func.hotkey.key;
			}

			if (func.dialog)
			{
				Text += " ...";
			}

			Image = pluginMan.GetPluginIcon(pi.name, func.name, darkMode);
		}

		public bool IsDialog => func.dialog;

		public string PluginTitle => func.title.Get(langCode);

		public void ProcessPlugin(
			object input,
			string path,
			bool alwaysOnTop,
			object secondaryArg,
			Form mainForm
		)
		{
			if (func.inputRequired && input == null) return;

			if (mainForm != null && func.hideMainForm) mainForm.Hide();

			object res = runner.Run(input, path, alwaysOnTop, secondaryArg);

			if (res != null)
			{
				PluginMan.OutputEventArgs oea = new PluginMan.OutputEventArgs
				{
					input = res
				};
				OnOutput(oea);
			}
			else if (mainForm != null && func.hideMainForm)
			{
				mainForm.Show();
			}
		}

		protected virtual void OnOutput(PluginMan.OutputEventArgs e)
		{
			Output?.Invoke(this, e);
		}
		public event EventHandler<PluginMan.OutputEventArgs> Output;
	}
}
