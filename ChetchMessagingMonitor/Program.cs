using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChetchMessagingMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            CMApplicationContext context = new CMApplicationContext();
            //Application.Run(context);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            context.Init();
            Application.Run(new MainForm(context));
            context.CurrentClient.Close();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
