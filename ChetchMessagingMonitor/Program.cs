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
            //SysTray
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CMApplicationContext context = new CMApplicationContext();
            Application.Run(context);*/

            //Normal winform
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CMApplicationContext context = new CMApplicationContext();
            context.Init();
            Application.Run(new MainForm(context));

            //end of both
            context.CloseClients();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
