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
			Color back = ThemeMan.DarkBackColor;
			Color secondary = ThemeMan.DarkSecondaryTextColor;

			CustomBackColor = back;
			closeBtn.DarkMode = true;
			githubBtn.Image = Properties.Resources.white_github;
			updateBtn.Image = Properties.Resources.white_sync;
			makeDefaultBtn.Image = Properties.Resources.white_check;
			dividerPanel.BackColor = ThemeMan.DarkSecondColor;
			descTextBox.BackColor = back;
			descTextBox.ForeColor = Color.White;
			versionLabel.ForeColor = secondary;
			moduleArtLabel.ForeColor = secondary;
			copyrightLabel.ForeColor = secondary;

			foreach (Button btn in new[] { githubBtn, updateBtn, makeDefaultBtn })
			{
				btn.BackColor = ThemeMan.DarkSecondColor;
				btn.FlatAppearance.MouseOverBackColor = ThemeMan.DarkHoverColor;
				btn.FlatAppearance.MouseDownBackColor = ThemeMan.DarkBackColor;
			}
		}

		private void InitLanguage()
		{
			Text = LangMan.Get("about");
			descTextBox.Text = LangMan.Get("app-description");
			makeDefaultBtn.Text = " " + LangMan.Get("set-as-default");
			infoTooltip.SetToolTip(makeDefaultBtn, LangMan.Get("set-as-default-image-viewer"));
			updateBtn.Text = " " + LangMan.Get("updates");
			infoTooltip.SetToolTip(updateBtn, LangMan.Get("check-for-app-updates"));
			infoTooltip.SetToolTip(closeBtn, NativeMan.GetMessageBoxText(NativeMan.DialogBoxCommandID.IDCLOSE) + " | Alt+F4");
		}

		private void updateBtn_Click(object sender, EventArgs e)
		{
			UpdateMan.CheckForUpdates(true, true, Handle);
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
