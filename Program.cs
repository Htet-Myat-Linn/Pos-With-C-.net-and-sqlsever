using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.MasterData;
using POS.Purchase;

using POS.DBA;
using POS.Sale;
using POS.ProfitLoss;

namespace POS
{
    static class Program
    {
       public static int UserID;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
