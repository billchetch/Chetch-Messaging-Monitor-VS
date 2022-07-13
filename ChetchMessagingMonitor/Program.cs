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
            bool asSysTray = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CMApplicationContext context = new CMApplicationContext(asSysTray);

            //SysTray (comment out below and uncomment this section to run as a syst tray app)
            if (asSysTray)
            {
                Application.Run(context);
            }
            else
            {
                context.Init();
                Application.Run(new MainForm(context));
            }

            //end of both
            context.CloseClients();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
