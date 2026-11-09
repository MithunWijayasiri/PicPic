namespace PicPic.Library
{
	partial class UpdateForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
			this.buttonYes = new System.Windows.Forms.Button();
			this.ReleaseNotes = new System.Windows.Forms.WebBrowser();
			this.closeBtn = new PicPic.Library.QlibTitlebarButton();
			this.infoTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.skipBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// buttonYes
			//
			this.buttonYes.BackColor = PicPic.Library.ThemeMan.LightPaleColor;
			this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.buttonYes.FlatAppearance.BorderSize = 0;
			this.buttonYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonYes.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.buttonYes.Location = new System.Drawing.Point(255, 414);
			this.buttonYes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.buttonYes.Name = "buttonYes";
			this.buttonYes.Size = new System.Drawing.Size(235, 32);
			this.buttonYes.TabIndex = 0;
			this.buttonYes.Text = "download";
			this.buttonYes.UseVisualStyleBackColor = false;
			//
			// ReleaseNotes
			//
			this.ReleaseNotes.AllowWebBrowserDrop = false;
			this.ReleaseNotes.IsWebBrowserContextMenuEnabled = false;
			this.ReleaseNotes.Location = new System.Drawing.Point(10, 104);
			this.ReleaseNotes.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
			this.ReleaseNotes.MinimumSize = new System.Drawing.Size(20, 20);
			this.ReleaseNotes.Name = "ReleaseNotes";
			this.ReleaseNotes.ScriptErrorsSuppressed = true;
			this.ReleaseNotes.Size = new System.Drawing.Size(480, 300);
			this.ReleaseNotes.TabIndex = 3;
			this.ReleaseNotes.WebBrowserShortcutsEnabled = false;
			this.ReleaseNotes.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.ReleaseNotes_Navigating);
			// 
			// closeBtn
			// 
			this.closeBtn.DarkImage = ((System.Drawing.Image)(resources.GetObject("closeBtn.DarkImage")));
			this.closeBtn.DarkMode = false;
			this.closeBtn.FlatAppearance.BorderSize = 0;
			this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
			this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.closeBtn.ForeColor = System.Drawing.Color.Black;
			this.closeBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeBtn.Image")));
			this.closeBtn.IsRed = true;
			this.closeBtn.LightImage = ((System.Drawing.Image)(resources.GetObject("closeBtn.LightImage")));
			this.closeBtn.Location = new System.Drawing.Point(468, 0);
			this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(32, 32);
			this.closeBtn.TabIndex = 4;
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// messageTextBox
			// 
			this.messageTextBox.BackColor = PicPic.Library.ThemeMan.LightBackColor;
			this.messageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.messageTextBox.Location = new System.Drawing.Point(10, 42);
			this.messageTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.messageTextBox.Multiline = true;
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.Size = new System.Drawing.Size(480, 56);
			this.messageTextBox.TabIndex = 5;
			this.messageTextBox.Text = "message";
			//
			// skipBtn
			//
			this.skipBtn.BackColor = PicPic.Library.ThemeMan.LightSecondColor;
			this.skipBtn.DialogResult = System.Windows.Forms.DialogResult.No;
			this.skipBtn.FlatAppearance.BorderSize = 0;
			this.skipBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.skipBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.skipBtn.Location = new System.Drawing.Point(10, 414);
			this.skipBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.skipBtn.Name = "skipBtn";
			this.skipBtn.Size = new System.Drawing.Size(235, 32);
			this.skipBtn.TabIndex = 1;
			this.skipBtn.Text = "skip this version";
			this.skipBtn.UseVisualStyleBackColor = false;
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.ClientSize = new System.Drawing.Size(500, 456);
			this.Controls.Add(this.skipBtn);
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.messageTextBox);
			this.Controls.Add(this.buttonYes);
			this.Controls.Add(this.ReleaseNotes);
			this.Draggable = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(320, 39);
			this.Name = "UpdateForm";
			this.ShowInTaskbar = false;
			this.ShowTitle = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "UpdateForm";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateForm_KeyDown);
			this.Load += new System.EventHandler(this.UpdateForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.WebBrowser ReleaseNotes;
		private System.Windows.Forms.Button buttonYes;
		private QlibTitlebarButton closeBtn;
		private System.Windows.Forms.ToolTip infoTooltip;
		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.Button skipBtn;
	}
}