using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDownloader.Core
{
    public abstract class DownloadableVideo : INotifyPropertyChanged
    {
        public long Progress { get; private set; }
        public abstract void DownloadTo(string path);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
