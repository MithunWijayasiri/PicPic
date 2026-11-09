using PicPic.Library;

namespace PicPic.Library.Tests
{
	public class UpdateFormTests
	{
		[Theory]
		[InlineData(true, "#191919")]
		[InlineData(false, "#ffffff")]
		public void BuildNotesPage_ThemesAndWrapsContent(bool darkMode, string expectedBg)
		{
			string page = UpdateForm.BuildNotesPage("<p>notes</p>", "https://github.com/o/r/releases/tag/v1.0", darkMode);

			Assert.Contains("background:" + expectedBg, page);
			Assert.Contains("<p>notes</p>", page);
			Assert.StartsWith("<html>", page);
			Assert.EndsWith("</html>", page);
		}

		[Fact]
		public void BuildNotesPage_ThemesScrollbarOnlyInDarkMode()
		{
			Assert.Contains("scrollbar-face-color:#383838", UpdateForm.BuildNotesPage("", "https://github.com/o/r/releases/tag/v1.0", true));
			Assert.DoesNotContain("scrollbar-face-color", UpdateForm.BuildNotesPage("", "https://github.com/o/r/releases/tag/v1.0", false));
		}

		[Fact]
		public void BuildNotesPage_SetsBaseUrlForRelativeLinks()
		{
			string page = UpdateForm.BuildNotesPage("", "https://github.com/o/r/releases/tag/v1.0", false);

			Assert.Contains("<base href=\"https://github.com/o/r/releases/tag/v1.0\">", page);
		}
	}
}
