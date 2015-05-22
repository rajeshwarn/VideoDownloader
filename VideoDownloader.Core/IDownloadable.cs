using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDownloader.Core
{
    interface IDownloadable
    {        
        void DownloadTo(string path);
    }
}
