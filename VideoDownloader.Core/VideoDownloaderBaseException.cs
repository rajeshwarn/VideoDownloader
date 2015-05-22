using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDownloader.Core
{
    public class VideoDownloaderBaseException : Exception
    {
        public VideoDownloaderBaseException(string message)
            : base(message)
        {
        }

        public VideoDownloaderBaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
