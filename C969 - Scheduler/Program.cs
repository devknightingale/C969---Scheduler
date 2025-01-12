using C969___Scheduler.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DBConnection.startConnection(); //opens database connection before form opens
            Application.Run(new Form1());
            DBConnection.endConnection(); //closes connection when form closes. 
        }
    }
}
