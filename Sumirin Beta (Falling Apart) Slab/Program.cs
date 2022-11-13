using Sumirin_Beta__Falling_Apart__Slab.Screen;
using System;
using System.Windows.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
