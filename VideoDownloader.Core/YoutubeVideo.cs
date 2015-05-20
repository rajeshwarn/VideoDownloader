using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Web;
using Leon.Framework.Utilities;

namespace VideoDownloader.Core
{
    public class YoutubeVideo
    {
        public string URL { get; private set; }
        public string Id { get; private set; }
        public ICollection<VideoQuality> Qualities { get; private set; }

        public string Title { get; private set; }

        public long Length { get; private set; }

        public string ThumbnailUrl
        {
            get { return string.Format("http://i3.ytimg.com/vi/{0}/default.jpg", Id); }
        }

        public YoutubeVideo(string url)
        {
            if (!Utilities.IsValidUrl(url) || !url.ToLower().Contains("www.youtube.com/watch?"))
            {
                throw new ArgumentException(@"Invalid YouTube URL", "url");
            }
            URL = url;
            Id = GetVideoIDFromUrl(url);
            var raw = GetYouTubeVideoRawInfo();
            var infoValues = HttpUtility.ParseQueryString(raw);
            Title = infoValues["title"];
            Length = long.Parse(infoValues["length_seconds"]);
            var videos = infoValues["url_encoded_fmt_stream_map"].Split(',');
            Qualities = new List<VideoQuality>();
            foreach (var video in videos)
            {
                var data = HttpUtility.ParseQueryString(video);
                var server = Uri.UnescapeDataString(data["fallback_host"]);
                var signature = data["sig"] ?? data["signature"];
                var downloadUrl = Uri.UnescapeDataString(data["url"]) + "&fallback_host=" + server;
                if (!string.IsNullOrEmpty(signature))
                    url += "&signature=" + signature;
                Qualities.Add(new VideoQuality()
                {
                    DownloadUrl = downloadUrl,
                    FileSize = GetVideoSize(downloadUrl),
                    Extention = new Extentisons(Convert.ToInt32(Uri.UnescapeDataString(data["itag"]).Trim())),
                    Dimension = new Dimension(Convert.ToInt32(Uri.UnescapeDataString(data["itag"]).Trim())),

                });
            }
        }

        #region helper method
        private string GetYouTubeVideoRawInfo()
        {
            using (var client = new WebClient())
            {
                var infoUrl = string.Format("http://www.youtube.com/get_video_info?&video_id={0}&el=detailpage&ps=default&eurl=&gl=US&hl=en", Id);
                return client.DownloadString(infoUrl);
            }
        }

        private static long GetVideoSize(string downloadUrl)
        {
            var fileInfoRequest = WebRequest.Create(downloadUrl);
            fileInfoRequest.Proxy = Utilities.GetProxy();
            var fileInfoResponse = fileInfoRequest.GetResponse();
            var bytesLength = fileInfoResponse.ContentLength;
            fileInfoRequest.Abort();
            return bytesLength;
        }

        public static string GetVideoIDFromUrl(string url)
        {
            url = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
            char[] delimiters = { '&', '#' };
            var props = url.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var videoid =
                props.First(prop => prop.StartsWith("v=")).Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1];
            return videoid;
        }
        #endregion

        public class VideoQuality
        {
            public Extentisons Extention { get; set; }
            public Dimension Dimension { get; set; }
            public string DownloadUrl { get; set; }
            public long FileSize { get; set; }

            public override string ToString()
            { 
                var videoSize = String.Format(new FileSizeFormatProvider(), "{0:fs}", FileSize);
                return String.Format("{0} ({1}) - {2}", Extention, Dimension, videoSize);
            }

        }
    }
}
