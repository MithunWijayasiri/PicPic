using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace PicPic.Library
{
	public partial class UpdateForm : QlibFixedForm
	{
		private readonly string _notesFailedText;
		private readonly string _viewOnGithubText;

		public UpdateForm(bool darkMode, string title, string message, string dwnldBtnText, string skipBtnText, string notesFailedText, string viewOnGithubText)
		{
			InitializeComponent();

			_notesFailedText = notesFailedText;
			_viewOnGithubText = viewOnGithubText;

			Text = title;
			messageTextBox.Text = string.Format(message, UpdateChecker.LatestRelease.TagName, "v" + UpdateChecker.CurrentVersion);
			buttonYes.Text = dwnldBtnText;
			skipBtn.Text = skipBtnText;

			infoTooltip.SetToolTip(closeBtn, NativeMan.GetMessageBoxText(NativeMan.DialogBoxCommandID.IDCLOSE) + " | Alt+F4");

			DarkMode = darkMode;
			closeBtn.DarkMode = darkMode;
			if (darkMode)
			{
				buttonYes.BackColor = ThemeMan.DarkPaleColor;
				buttonYes.ForeColor = Color.White;
				skipBtn.BackColor = ThemeMan.DarkSecondColor;
				messageTextBox.BackColor = ThemeMan.DarkBackColor;
				messageTextBox.ForeColor = Color.White;
			}
		}

		private async void UpdateForm_Load(object sender, EventArgs e)
		{
			string html;
			try
			{
				html = await UpdateChecker.RenderReleaseNotes();
			}
			catch (Exception)
			{
				html = "<p>" + WebUtility.HtmlEncode(_notesFailedText) + " <a href=\"" + WebUtility.HtmlEncode(UpdateChecker.LatestRelease.HtmlUrl) + "\">" + WebUtility.HtmlEncode(_viewOnGithubText) + "</a></p>";
			}

			if (IsDisposed) return;
			ReleaseNotes.DocumentText = BuildNotesPage(html, DarkMode);
		}

		internal static string BuildNotesPage(string bodyHtml, bool darkMode)
		{
			string bg = darkMode ? "#191919" : "#ffffff";
			string text = darkMode ? "#e8e8e8" : "#242430";
			string heading = darkMode ? "#ffffff" : "#101018";
			string border = darkMode ? "#3a3a3a" : "#d8d8f0";
			string thBg = darkMode ? "#2c2c2c" : "#ececf8";
			string link = darkMode ? "#5ca8ff" : "#0b5cad";
			string codeBg = darkMode ? "#2c2c2c" : "#ececf8";
			// IE-engine scrollbar ignores normal CSS; only these legacy properties theme it.
			string scrollbar = darkMode ? "scrollbar-base-color:#202020;scrollbar-face-color:#383838;scrollbar-track-color:#191919;scrollbar-arrow-color:#a0a0a0;scrollbar-shadow-color:#383838;scrollbar-highlight-color:#383838;scrollbar-3dlight-color:#383838;scrollbar-darkshadow-color:#191919;" : "";
			return "<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><style>" +
				"body{background:" + bg + ";color:" + text + ";font-family:'Segoe UI',sans-serif;font-size:12px;line-height:1.5;margin:8px;" + scrollbar + "}" +
				"h1,h2,h3{color:" + heading + ";font-size:14px;border-bottom:1px solid " + border + ";padding-bottom:4px;}" +
				"a{color:" + link + ";}" +
				"table{border-collapse:collapse;width:100%;}" +
				"th,td{border:1px solid " + border + ";padding:5px 8px;text-align:left;vertical-align:top;}" +
				"th{background:" + thBg + ";color:" + heading + ";}" +
				"code{background:" + codeBg + ";font-family:Consolas,monospace;font-size:11px;padding:1px 4px;}" +
				"pre{background:" + codeBg + ";padding:6px;overflow:auto;}" +
				"</style></head><body>" + bodyHtml + "</body></html>";
		}

		private void ReleaseNotes_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == Uri.UriSchemeHttp || e.Url.Scheme == Uri.UriSchemeHttps)
			{
				e.Cancel = true;
				Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = e.Url.ToString() });
			}
		}

		private void UpdateForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
