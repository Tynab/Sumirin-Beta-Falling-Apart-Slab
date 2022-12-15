using System;
using System.Runtime.InteropServices;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    public class Define
    {
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);
    }
}
