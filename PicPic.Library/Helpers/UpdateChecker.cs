using Octokit;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace PicPic.Library
{
	internal static class UpdateChecker
	{
		private static IReleasesClient _releaseClient;
		private static GitHubClient _github;
		private static string _repositoryOwner;
		private static string _repositoryName;
		private static Version _currentVersion;

		internal static string CurrentVersion;
		internal static Release LatestRelease;

		internal static void Init(string owner, string repo)
		{
			_currentVersion = Assembly.GetEntryAssembly().GetName().Version;
			CurrentVersion = _currentVersion.ToString(3);

			_github = new GitHubClient(new ProductHeaderValue(repo + @"-UpdateCheck"));
			_releaseClient = _github.Repository.Release;

			_repositoryOwner = owner;
			_repositoryName = repo;
		}

		internal static async Task<string> CheckUpdate()
		{
			var releases = await _releaseClient.GetAll(_repositoryOwner, _repositoryName);

			foreach (Release release in releases)
			{
				Version version = ParseReleaseVersion(release.TagName);
				if (version == null)
				{
					continue;
				}
				if (version > _currentVersion)
				{
					LatestRelease = release;
					return version.ToString();
				}
			}
			return null;
		}

		internal static Version ParseReleaseVersion(string tagName)
		{
			return Version.TryParse(tagName.TrimStart('v', 'V'), out Version version) ? version : null;
		}

		internal static async Task<string> RenderReleaseNotes()
		{
			if (LatestRelease == null)
			{
				throw new InvalidOperationException();
			}
			return await _github.Miscellaneous.RenderRawMarkdown(LatestRelease.Body);
		}
	}
}
