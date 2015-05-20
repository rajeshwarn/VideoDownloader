//Copy rights are reserved for Akram kamal qassas
//Email me, Akramnet4u@hotmail.com
using System;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace YouTube_Downloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WebRequest.DefaultWebProxy = Helper.InitialProxy();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }



    }
}
