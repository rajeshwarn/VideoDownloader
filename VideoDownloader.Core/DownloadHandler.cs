using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using Leon.Framework.Utilities;
using VideoDownloader.Core.Properties;

namespace VideoDownloader.Core
{
    public class DownloadHandler : INotifyPropertyChanged
    {
        public static int DownloadBlockSize = 1024 * 200;

        public string URL { get; private set; }

        public string Path { get; private set; }

        private long _progress;

        public long Progress
        {
            get { return _progress; }
            private set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DownloadHandler(string url, string path)
        {
            Progress = 0;
            Path = path;
            URL = url;
        }

        public void Start()
        {
            using (var client = new WebClient())
            {
                client.Proxy = Utilities.GetProxyWithCredentials();
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadFileTaskAsync(URL, Path).Wait();
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = e.BytesReceived / e.TotalBytesToReceive;
        }



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
