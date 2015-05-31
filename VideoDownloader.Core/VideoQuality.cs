using System;
using System.Runtime.InteropServices;

namespace VideoDownloader.Core
{
    public class VideoQuality
    {
        public Extentisons Extention { get; set; }
        public Dimension Dimension { get; set; }
        public string DownloadUrl { get; set; }
        public long FileSize { get; set; }

        public override string ToString()
        {
            //var test = string.Format()
            var videoSize = String.Format(new FileSizeFormatProvider(), "{0:fs}", FileSize);
            return String.Format("{0} ({1}) - {2}", Extention, Dimension, videoSize);
        }
    }
}