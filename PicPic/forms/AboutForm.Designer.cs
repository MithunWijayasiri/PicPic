namespace PicPic
{
	partial class AboutForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			productLabel = new System.Windows.Forms.Label();
			versionLabel = new System.Windows.Forms.Label();
			infoTooltip = new System.Windows.Forms.ToolTip(components);
			closeBtn = new PicPic.Library.QlibTitlebarButton();
			updateBtn = new System.Windows.Forms.Button();
			githubBtn = new System.Windows.Forms.Button();
			descTextBox = new System.Windows.Forms.TextBox();
			makeDefaultBtn = new System.Windows.Forms.Button();
			copyrightLabel = new System.Windows.Forms.Label();
			moduleArtLabel = new System.Windows.Forms.Label();
			logoPictureBox = new System.Windows.Forms.PictureBox();
			dividerPanel = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
			SuspendLayout();
			// 
			// productLabel
			// 
			productLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			productLabel.Location = new System.Drawing.Point(110, 38);
			productLabel.Margin = new System.Windows.Forms.Padding(0);
			productLabel.Name = "productLabel";
			productLabel.Size = new System.Drawing.Size(278, 24);
			productLabel.TabIndex = 27;
			productLabel.Text = Program.AppName;
			productLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// versionLabel
			// 
			versionLabel.ForeColor = System.Drawing.Color.FromArgb(106, 106, 146);
			versionLabel.Location = new System.Drawing.Point(110, 62);
			versionLabel.Margin = new System.Windows.Forms.Padding(0);
			versionLabel.Name = "versionLabel";
			versionLabel.Size = new System.Drawing.Size(290, 19);
			versionLabel.TabIndex = 28;
			versionLabel.Text = "version";
			versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// closeBtn
			// 
			closeBtn.DarkImage = Properties.Resources.black_close;
			closeBtn.DarkMode = false;
			closeBtn.FlatAppearance.BorderSize = 0;
			closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(140, 140, 140);
			closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
			closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			closeBtn.ForeColor = System.Drawing.Color.Black;
			closeBtn.Image = Properties.Resources.black_close;
			closeBtn.IsRed = true;
			closeBtn.LightImage = Properties.Resources.white_close;
			closeBtn.Location = new System.Drawing.Point(388, 0);
			closeBtn.Margin = new System.Windows.Forms.Padding(0);
			closeBtn.Name = "closeBtn";
			closeBtn.Size = new System.Drawing.Size(32, 32);
			closeBtn.TabIndex = 4;
			infoTooltip.SetToolTip(closeBtn, "Close (Alt+F4)");
			closeBtn.UseVisualStyleBackColor = true;
			closeBtn.Click += closeBtn_Click;
			// 
			// updateBtn
			// 
			updateBtn.BackColor = System.Drawing.Color.FromArgb(216, 216, 240);
			updateBtn.FlatAppearance.BorderSize = 0;
			updateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(184, 184, 220);
			updateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 200, 232);
			updateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			updateBtn.Enabled = false;
			updateBtn.Image = Properties.Resources.black_sync;
			updateBtn.Location = new System.Drawing.Point(150, 134);
			updateBtn.Margin = new System.Windows.Forms.Padding(0);
			updateBtn.Name = "updateBtn";
			updateBtn.Size = new System.Drawing.Size(120, 36);
			updateBtn.TabIndex = 2;
			updateBtn.Text = " updates";
			updateBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			updateBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			updateBtn.UseVisualStyleBackColor = false;
			// 
			// githubBtn
			// 
			githubBtn.BackColor = System.Drawing.Color.FromArgb(216, 216, 240);
			githubBtn.FlatAppearance.BorderSize = 0;
			githubBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(184, 184, 220);
			githubBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 200, 232);
			githubBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			githubBtn.Image = Properties.Resources.black_github;
			githubBtn.Location = new System.Drawing.Point(20, 134);
			githubBtn.Margin = new System.Windows.Forms.Padding(0);
			githubBtn.Name = "githubBtn";
			githubBtn.Size = new System.Drawing.Size(120, 36);
			githubBtn.TabIndex = 1;
			githubBtn.Text = " GitHub";
			githubBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			githubBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			infoTooltip.SetToolTip(githubBtn, "github.com/MithunWijayasiri/PicPic");
			githubBtn.UseVisualStyleBackColor = false;
			githubBtn.Click += button1_Click;
			// 
			// descTextBox
			// 
			descTextBox.BackColor = PicPic.Library.ThemeMan.LightBackColor;
			descTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			descTextBox.Location = new System.Drawing.Point(113, 86);
			descTextBox.Margin = new System.Windows.Forms.Padding(0);
			descTextBox.Multiline = true;
			descTextBox.Name = "descTextBox";
			descTextBox.ReadOnly = true;
			descTextBox.Size = new System.Drawing.Size(287, 36);
			descTextBox.TabIndex = 5;
			descTextBox.Text = "description";
			// 
			// makeDefaultBtn
			// 
			makeDefaultBtn.BackColor = System.Drawing.Color.FromArgb(216, 216, 240);
			makeDefaultBtn.Enabled = false;
			makeDefaultBtn.FlatAppearance.BorderSize = 0;
			makeDefaultBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(184, 184, 220);
			makeDefaultBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 200, 232);
			makeDefaultBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			makeDefaultBtn.Image = Properties.Resources.black_check;
			makeDefaultBtn.Location = new System.Drawing.Point(280, 134);
			makeDefaultBtn.Margin = new System.Windows.Forms.Padding(0);
			makeDefaultBtn.Name = "makeDefaultBtn";
			makeDefaultBtn.Size = new System.Drawing.Size(120, 36);
			makeDefaultBtn.TabIndex = 3;
			makeDefaultBtn.Text = " set as default";
			makeDefaultBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			makeDefaultBtn.UseVisualStyleBackColor = false;
			makeDefaultBtn.Click += makeDefaultBtn_Click;
			// 
			// copyrightLabel
			// 
			copyrightLabel.ForeColor = System.Drawing.Color.FromArgb(106, 106, 146);
			copyrightLabel.Location = new System.Drawing.Point(20, 194);
			copyrightLabel.Margin = new System.Windows.Forms.Padding(0);
			copyrightLabel.Name = "copyrightLabel";
			copyrightLabel.Size = new System.Drawing.Size(240, 19);
			copyrightLabel.TabIndex = 44;
			copyrightLabel.Text = "Copyright © 2026 Mithun Wijayasiri";
			copyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// moduleArtLabel
			// 
			moduleArtLabel.Font = new System.Drawing.Font("Segoe UI", 6F);
			moduleArtLabel.ForeColor = System.Drawing.Color.FromArgb(106, 106, 146);
			moduleArtLabel.Location = new System.Drawing.Point(260, 196);
			moduleArtLabel.Margin = new System.Windows.Forms.Padding(0);
			moduleArtLabel.Name = "moduleArtLabel";
			moduleArtLabel.Size = new System.Drawing.Size(140, 15);
			moduleArtLabel.TabIndex = 45;
			moduleArtLabel.Text = "© 2023 Module Art";
			moduleArtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dividerPanel
			// 
			dividerPanel.BackColor = System.Drawing.Color.FromArgb(200, 200, 230);
			dividerPanel.Location = new System.Drawing.Point(20, 186);
			dividerPanel.Margin = new System.Windows.Forms.Padding(0);
			dividerPanel.Name = "dividerPanel";
			dividerPanel.Size = new System.Drawing.Size(380, 1);
			dividerPanel.TabIndex = 46;
			// 
			// logoPictureBox
			// 
			logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
			logoPictureBox.Location = new System.Drawing.Point(20, 44);
			logoPictureBox.Margin = new System.Windows.Forms.Padding(0);
			logoPictureBox.Name = "logoPictureBox";
			logoPictureBox.Size = new System.Drawing.Size(72, 72);
			logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			logoPictureBox.TabIndex = 41;
			logoPictureBox.TabStop = false;
			// 
			// AboutForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			ClientSize = new System.Drawing.Size(420, 224);
			Controls.Add(closeBtn);
			Controls.Add(updateBtn);
			Controls.Add(githubBtn);
			Controls.Add(copyrightLabel);
			Controls.Add(moduleArtLabel);
			Controls.Add(dividerPanel);
			Controls.Add(makeDefaultBtn);
			Controls.Add(descTextBox);
			Controls.Add(logoPictureBox);
			Controls.Add(versionLabel);
			Controls.Add(productLabel);
			CustomBack = true;
			CustomBackColor = PicPic.Library.ThemeMan.LightBackColor;
			Draggable = true;
			EscToClose = true;
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "AboutForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "AboutForm";
			Load += AboutForm_Load;
			((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Label productLabel;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.ToolTip infoTooltip;
		private PicPic.Library.QlibTitlebarButton closeBtn;
		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.TextBox descTextBox;
		private System.Windows.Forms.Button makeDefaultBtn;
		private System.Windows.Forms.Label copyrightLabel;
		private System.Windows.Forms.Label moduleArtLabel;
		private System.Windows.Forms.Panel dividerPanel;
		private System.Windows.Forms.Button githubBtn;
		private System.Windows.Forms.Button updateBtn;
	}
}
