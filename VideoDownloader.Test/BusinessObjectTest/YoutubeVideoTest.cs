using System;
using System.Diagnostics;
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
                new YoutubeVideo(@"https://www.youtube.com/watch?v=3OA_CV_77Vc");
            }
            catch (Exception)
            {
                Assert.Fail("Exception");
            }
            Assert.IsTrue(true);
        }
    }
}
