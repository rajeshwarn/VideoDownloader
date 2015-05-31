using System;
using System.Diagnostics;
using System.Linq;
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

        [TestMethod]
        public void TestContruction()
        {
            try
            {
                var video = new YoutubeVideo(@"https://www.youtube.com/watch?v=3OA_CV_77Vc");
                video.SelectedQuality = video.AvaliableQualities.First();
                //video.DownloadTo(@"C:\Users\leon\Desktop\temp\" + video.Title + "." + video.SelectedQuality.Extention);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception: " + ex.Message);
            }
            Assert.IsTrue(true);
        }
    }
}
