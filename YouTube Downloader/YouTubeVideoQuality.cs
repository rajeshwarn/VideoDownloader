using System;
using System.Drawing;

namespace YouTube_Downloader
{
    /// <summary>
    /// Contains information about the video url extension and dimension
    /// </summary>
    public class YouTubeVideoQuality
    {
        /// <summary>
        /// Gets or Sets the file name
        /// </summary>
        public string VideoTitle { get; set; }
        /// <summary>
        /// Gets or Sets the file extention
        /// </summary>
        public string Extention { get; set; }
        /// <summary>
        /// Gets or Sets the file url
        /// </summary>
        public string DownloadUrl { get; set; }
        /// <summary>
        /// Gets or Sets the youtube video url
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// Gets or Sets the youtube video size
        /// </summary>
        public long VideoSize { get; set; }
        /// <summary>
        /// Gets or Sets the youtube video dimension
        /// </summary>
        public Size Dimension { get; set; }
        /// <summary>
        /// Gets the youtube video length
        /// </summary>
        public long Length { get; set; }
        public override string ToString()
        {
            var videoExtention = Extention;
            var videoDimension = FormatSize(Dimension);
            var videoSize = String.Format(new FileSizeFormatProvider(), "{0:fs}", this.VideoSize);

            return String.Format("{0} ({1}) - {2}", videoExtention.ToUpper(), videoDimension, videoSize);
        }

        private static string FormatSize(Size value)
        {
            string s = value.Height >= 720 ? " HD" : "";
            return ((Size)value).Width + " x " + value.Height + s;
        }
    }
}