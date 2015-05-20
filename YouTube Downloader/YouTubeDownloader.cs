//Copy rights are reserved for Akram kamal qassas
//Email me, Akramnet4u@hotmail.com
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace YouTube_Downloader
{
    /// <summary>
    /// Use this class to get youtube video urls
    /// </summary>
    public static class YouTubeDownloader
    {
        public static List<YouTubeVideoQuality> GetYouTubeVideoUrls(string videoUrl)
        {
            var list = new List<YouTubeVideoQuality>();

            var id = GetVideoIDFromUrl(videoUrl);
            var infoUrl = string.Format("http://www.youtube.com/get_video_info?&video_id={0}&el=detailpage&ps=default&eurl=&gl=US&hl=en", id);
            var infoText = new WebClient().DownloadString(infoUrl);
            var infoValues = HttpUtility.ParseQueryString(infoText);
            var title = infoValues["title"];
            var videoDuration = infoValues["length_seconds"];
            var videos = infoValues["url_encoded_fmt_stream_map"].Split(',');
            foreach (var item in videos)
            {
                try
                {
                    var data = HttpUtility.ParseQueryString(item);
                    var server = Uri.UnescapeDataString(data["fallback_host"]);
                    var signature = data["sig"] ?? data["signature"];
                    var url = Uri.UnescapeDataString(data["url"]) + "&fallback_host=" + server;
                    if (!string.IsNullOrEmpty(signature))
                        url += "&signature=" + signature;
                    var size = GetSize(url);
                    var videoItem = new YouTubeVideoQuality();
                    videoItem.DownloadUrl = url;
                    videoItem.VideoSize = size;
                    videoItem.VideoTitle = title;
                    var tagInfo = new TagInfo(Uri.UnescapeDataString(data["itag"]));
                    videoItem.Dimension = tagInfo.VideoDimensions;
                    videoItem.Extention = tagInfo.VideoExtentions;
                    videoItem.Length = long.Parse(videoDuration);
                    list.Add(videoItem);
                }
                catch { }
            }

            return list;
        }



        private static long GetSize(string videoUrl)
        {
            HttpWebRequest fileInfoRequest = (HttpWebRequest)HttpWebRequest.Create(videoUrl);
            fileInfoRequest.Proxy = Helper.InitialProxy();
            HttpWebResponse fileInfoResponse = (HttpWebResponse)fileInfoRequest.GetResponse();
            long bytesLength = fileInfoResponse.ContentLength;
            fileInfoRequest.Abort();
            return bytesLength;
        }

        /// <summary>
        /// Get the ID of a youtube video from its URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetVideoIDFromUrl(string url)
        {
            url = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
            char[] delimiters = { '&', '#' };
            var props = url.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            var videoid = "";
            foreach (var prop in props)
            {
                if (prop.StartsWith("v="))
                {
                    videoid = prop.Substring(prop.IndexOf("v=", StringComparison.Ordinal) + 2);
                }
            }

            return videoid;
        }

    }
}
