using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDownloader.Core
{
    interface IDownloadable : INotifyPropertyChanged
    {
        long Progress { get; set; }
        void DownloadTo(string path);
    }
}
