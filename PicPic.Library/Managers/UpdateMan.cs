using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PicPic.Library
{
	public static class UpdateMan
	{
		private static string _githubOwner;
		private static string _githubRepo;
		private static bool _darkMode;
		private static string _skippedVersion;

		private static string _title;
		private static string _message;
		private static string _dwnldBtnText;
		private static string _skipBtnText;
		private static string _whatsNewBtnText;

		public static void Init(string githubOwner, string githubRepo, bool darkMode, string skippedVersion)
		{
			_githubOwner = githubOwner;
			_githubRepo = githubRepo;
			_darkMode = darkMode;
			_skippedVersion = skippedVersion;
		}

		public static void InitLang(string title, string message, string dwnldBtnText, string skipBtnText, string whatsNewBtnText)
		{
			_title = title;
			_message = message;
			_dwnldBtnText = dwnldBtnText;
			_skipBtnText = skipBtnText;
			_whatsNewBtnText = whatsNewBtnText;
		}

		public static async void CheckForUpdates(bool showUpToDateDialog, bool topMost, IntPtr owner)
		{
			try
			{
				UpdateChecker.Init(_githubOwner, _githubRepo);
				string update = await UpdateChecker.CheckUpdate();

				if (update == null)
				{
					if (showUpToDateDialog)
					{
						OnIsUpToDate(EventArgs.Empty);
					}
				}
				else
				{
					if (!showUpToDateDialog && _skippedVersion == update)
					{
						return;
					}

					UpdateForm updateDialog = new UpdateForm(_darkMode, _title, _message, _dwnldBtnText, _skipBtnText, _whatsNewBtnText);
					updateDialog.TopMost = topMost;

					DialogResult result = updateDialog.ShowDialog(Form.FromHandle(owner));
					updateDialog.Dispose();

					if (result == DialogResult.Yes)
					{
						try
						{
							Process.Start(new ProcessStartInfo
							{
								UseShellExecute = true,
								FileName = UpdateChecker.LatestRelease.HtmlUrl
							});
						}
						catch (Exception ex)
						{
							// The user clicked Download, so report even on the silent startup path.
							OnUpdateFailed(new UpdateFailedEventArgs { Exception = ex });
						}
					}
					else if (result == DialogResult.No)
					{
						UpdateSkippedEventArgs usea = new UpdateSkippedEventArgs
						{
							SkippedVersion = update
						};
						OnUpdateSkipped(usea);
					}
				}
			}
			catch (Exception ex)
			{
				if (showUpToDateDialog)
				{
					UpdateFailedEventArgs ufea = new UpdateFailedEventArgs
					{
						Exception = ex
					};
					OnUpdateFailed(ufea);
				}
			}
		}

		public static void OnUpdateFailed(UpdateFailedEventArgs ufea)
		{
			UpdateFailed?.Invoke(null, ufea);
		}
		public static event EventHandler<UpdateFailedEventArgs> UpdateFailed;

		public static void OnIsUpToDate(EventArgs ea)
		{
			IsUpToDate?.Invoke(null, ea);
		}
		public static event EventHandler<EventArgs> IsUpToDate;

		public static void OnUpdateSkipped(UpdateSkippedEventArgs usea)
		{
			UpdateSkipped?.Invoke(null, usea);
		}
		public static event EventHandler<UpdateSkippedEventArgs> UpdateSkipped;
	}

	public class UpdateFailedEventArgs : EventArgs
	{
		public Exception Exception { get; set; }
	}

	public class UpdateSkippedEventArgs : EventArgs
	{
		public string SkippedVersion { get; set; }
	}
}
