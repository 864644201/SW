using System;
using System.Threading;
using System.Windows.Forms;

namespace Interop.Office.Core
{
    internal static class Program
    {
        private static Mutex mutex;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool createdNew;
            mutex = new Mutex(true, "TrunnionCalculation_SingleInstance", out createdNew);
            if (createdNew)
            {
                Application.Run(new zhouj());
            }
        }
    }
}
