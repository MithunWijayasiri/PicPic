using System.Windows.Forms;

namespace quick_picture_viewer
{
	class CustomOpenFolderDialog
	{
		public static string GetFolder(string title)
		{
			using FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = title;
			dialog.UseDescriptionForTitle = true;
			return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
		}
	}
}
