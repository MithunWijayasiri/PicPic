using PicPic.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PicPic
{
	partial class AboutForm : QlibFixedForm
	{
		public AboutForm()
		{
			InitializeComponent();
			SetDraggableControls(new List<Control>() { logoPictureBox, productLabel, versionLabel, moduleArtLabel, copyrightLabel });

			string fullVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			int lastDotIndex = fullVer.LastIndexOf('.');
			versionLabel.Text = String.Format("v{0}", fullVer.Substring(0, lastDotIndex));

			if (IntPtr.Size == 4) versionLabel.Text += " (x32)";
			else if (IntPtr.Size == 8) versionLabel.Text += " (x64)";

			if (ThemeMan.isWindows10orLater()) makeDefaultBtn.Enabled = true;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			ThemeMan.ApplyDarkTitlebar(Handle, DarkMode);
			base.OnHandleCreated(e);
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			InitLanguage();
			if (DarkMode) SetDarkColors();
		}

		private void SetDarkColors()
		{
			Color back = Color.FromArgb(18, 18, 42);
			Color secondary = Color.FromArgb(156, 156, 200);

			CustomBackColor = back;
			closeBtn.DarkMode = true;
			githubBtn.Image = Properties.Resources.white_github;
			updateBtn.Image = Properties.Resources.white_sync;
			makeDefaultBtn.Image = Properties.Resources.white_check;
			dividerPanel.BackColor = Color.FromArgb(46, 46, 92);
			descTextBox.BackColor = back;
			descTextBox.ForeColor = Color.White;
			versionLabel.ForeColor = secondary;
			moduleArtLabel.ForeColor = secondary;
			copyrightLabel.ForeColor = secondary;

			foreach (Button btn in new[] { githubBtn, updateBtn, makeDefaultBtn })
			{
				btn.BackColor = Color.FromArgb(36, 36, 74);
				btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 48, 96);
				btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(26, 26, 56);
			}
		}

		private void InitLanguage()
		{
			Text = LangMan.Get("about");
			descTextBox.Text = LangMan.Get("app-description");
			makeDefaultBtn.Text = " " + LangMan.Get("set-as-default");
			infoTooltip.SetToolTip(makeDefaultBtn, LangMan.Get("set-as-default-image-viewer"));
			// TODO: enable updateBtn and wire it to UpdateMan once the first PicPic release exists.
			updateBtn.Text = " " + LangMan.Get("updates");
			infoTooltip.SetToolTip(updateBtn, LangMan.Get("check-for-app-updates"));
			infoTooltip.SetToolTip(closeBtn, NativeMan.GetMessageBoxText(NativeMan.DialogBoxCommandID.IDCLOSE) + " | Alt+F4");
		}

		private void makeDefaultBtn_Click(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				UseShellExecute = true,
				FileName = "ms-settings:defaultapps"
			});
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				UseShellExecute = true,
				FileName = "https://github.com/MithunWijayasiri/PicPic"
			});
		}
	}
}
