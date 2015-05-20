using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoDownloader.Core;

namespace VideoDownloader.Test.BusinessObjectTest
{
    [TestClass]
    public class YoutubeVideoTest
    {
        [TestMethod]
        public void TestGetVideoIDFromUrl()
        {
            var id = YoutubeVideo.GetVideoIDFromUrl(@"https://www.youtube.com/watch?v=v489sYYjtHI");
            Assert.AreEqual("v489sYYjtHI", id);
        }
    }
}
