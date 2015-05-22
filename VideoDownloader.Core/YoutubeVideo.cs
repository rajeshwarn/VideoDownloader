using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Text;
using System.Web;
using Leon.Framework.Utilities;

namespace VideoDownloader.Core
{
    public class YoutubeVideo : IDownloadable
    {
        public string URL { get; private set; }

        public string Id { get; private set; }

        public ICollection<VideoQuality> AvaliableQualities { get; private set; }

        public VideoQuality SelectedQuality { get; set; }

        public string Title { get; private set; }

        public TimeSpan Length { get; private set; }

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
            Length = new TimeSpan(long.Parse(infoValues["length_seconds"]) * TimeSpan.TicksPerSecond);
            var videos = infoValues["url_encoded_fmt_stream_map"].Split(',');
            AvaliableQualities = new List<VideoQuality>();
            foreach (var video in videos)
            {
                var data = HttpUtility.ParseQueryString(video);
                var server = Uri.UnescapeDataString(data["fallback_host"]);
                var signature = data["sig"] ?? data["signature"];
                var downloadUrl = Uri.UnescapeDataString(data["url"]) + "&fallback_host=" + server;
                if (!string.IsNullOrEmpty(signature))
                    url += "&signature=" + signature;
                AvaliableQualities.Add(new VideoQuality()
                {
                    DownloadUrl = downloadUrl,
                    FileSize = GetVideoSize(downloadUrl),
                    Extention = new Extentisons(Convert.ToInt32(Uri.UnescapeDataString(data["itag"]).Trim())),
                    Dimension = new Dimension(Convert.ToInt32(Uri.UnescapeDataString(data["itag"]).Trim())),

                });
            }
        }

        public void DownloadTo(string path)
        {
            if (SelectedQuality == null)
            {
                throw new VideoDownloaderBaseException("Video Selected Quality Not Set.");
            }
            SelectedQuality.DownloadTo(path);
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

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendFormat("Title : {0} \r\nLength : {1} \r\nThumbnailUrl : {2}\r\nQulities:\r\n", Title, Length, ThumbnailUrl);
            foreach (var videoQuality in AvaliableQualities)
            {
                result.AppendLine("\t" + videoQuality);
            }
            return result.ToString();
        }


    }
}
