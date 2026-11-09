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
			string page = UpdateForm.BuildNotesPage("<p>notes</p>", darkMode);

			Assert.Contains("background:" + expectedBg, page);
			Assert.Contains("<p>notes</p>", page);
			Assert.StartsWith("<html>", page);
			Assert.EndsWith("</html>", page);
		}

		[Fact]
		public void BuildNotesPage_ThemesScrollbarOnlyInDarkMode()
		{
			Assert.Contains("scrollbar-face-color:#383838", UpdateForm.BuildNotesPage("", true));
			Assert.DoesNotContain("scrollbar-face-color", UpdateForm.BuildNotesPage("", false));
		}
	}
}
