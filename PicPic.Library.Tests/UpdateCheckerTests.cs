using System;
using PicPic.Library;

namespace PicPic.Library.Tests
{
	public class UpdateCheckerTests
	{
		[Theory]
		[InlineData("v1.2.3", "1.2.3")]
		[InlineData("V1.2.3", "1.2.3")]
		[InlineData("1.2.3", "1.2.3")]
		[InlineData("v0.1.1", "0.1.1")]
		public void ParseReleaseVersion_ParsesVersionTags(string tag, string expected)
		{
			Assert.Equal(Version.Parse(expected), UpdateChecker.ParseReleaseVersion(tag));
		}

		[Theory]
		[InlineData("v1.2.3-rc1")]
		[InlineData("nightly")]
		[InlineData("")]
		[InlineData("v")]
		public void ParseReleaseVersion_SkipsTagsThatAreNotVersions(string tag)
		{
			Assert.Null(UpdateChecker.ParseReleaseVersion(tag));
		}
	}
}
