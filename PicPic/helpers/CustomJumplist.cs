using System;
using System.Windows.Shell;

namespace PicPic
{
	public class CustomJumplist
	{
		private JumpList list;

		public CustomJumplist(string newWindowTitle, string newWindowDesc)
		{
			JumpItem[] jumpItems = { new JumpTask() {
				Title = newWindowTitle,
				Description = newWindowDesc,
				ApplicationPath = Environment.ProcessPath,
				Arguments = "-1",
				IconResourcePath = Environment.ProcessPath
			} };

			list = new JumpList(jumpItems, true, true);
			list.Apply();
		}
	}
}
