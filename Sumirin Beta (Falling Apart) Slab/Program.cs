using Sumirin_Beta__Falling_Apart__Slab.Screen;
using System;
using System.Threading;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;
using static System.GC;
using static System.Windows.Forms.Application;

namespace Sumirin_Beta__Falling_Apart__Slab
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var mutex = new Mutex(true, app_name, out var rslt);
            if (!rslt)
            {
                return;
            }
            EnableVisualStyles();
            SetCompatibleTextRenderingDefault(false);
            Run(new FrmMain());
            KeepAlive(mutex);
        }
    }
}
