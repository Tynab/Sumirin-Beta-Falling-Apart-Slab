using System;
using System.IO;
using System.Net.NetworkInformation;
using static Microsoft.VisualBasic.Interaction;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Convert;
using static System.IO.Directory;
using static System.Math;
using static System.Net.NetworkInformation.IPStatus;
using static System.Windows.Forms.Application;
using static System.Windows.Forms.DialogResult;
using static System.Windows.Forms.MessageBoxButtons;
using static System.Windows.Forms.MessageBoxIcon;
using static YANF.Script.YANConstant.MsgBoxLang;
using static YANF.Script.YANMessageBox;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    internal static class Common
    {
        /// <summary>
        /// Update valid license.
        /// </summary>
        private static void UpdVldLic()
        {
            Default.Chk_Key = true;
            Default.Save();
        }

        /// <summary>
        /// Check internet connection.
        /// </summary>
        /// <returns>Connection state.</returns>
        internal static bool IsNetAvail()
        {
            try
            {
                var buffer = new byte[32];
                return new Ping().Send(link_base, TIME_OUT, buffer, new PingOptions()).Status == Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Create directory advanced.
        /// </summary>
        /// <param name="path">Folder path.</param>
        internal static void CrtDirAdv(string path)
        {
            if (!Exists(path))
            {
                CreateDirectory(path);
            }
        }

        /// <summary>
        /// Delete file advanced.
        /// </summary>
        /// <param name="adr">File address.</param>
        internal static void DelFileAdv(string adr)
        {
            if (File.Exists(adr))
            {
                File.Delete(adr);
            }
        }

        /// <summary>
        /// Check license.
        /// </summary>
        internal static void ChkLic()
        {
            if (!Default.Chk_Key)
            {
            ChkPt:
                if (InputBox("シリアルを入力", "ライセンスキー", null, -1, -1) == key_ser)
                {
                    UpdVldLic();
                }
                else
                {
                    if (Show("エラー", "ライセンスが間違っています！", RetryCancel, Error, JAP) == Retry)
                    {
                        goto ChkPt;
                    }
                    else
                    {
                        Exit();
                    }
                }
            }
        }

        /// <summary>
        /// Round 10.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static int Round10<T>(this T num) => (int)Ceiling(ToDouble(num) / 10) * 10;

        /// <summary>
        /// Round 500.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static int Round500<T>(this T num) => (int)Ceiling(ToDouble(num) / 500) * 500;

        /// <summary>
        /// Ceiling 50.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Ceiling number.</returns>
        internal static int Ceiling50<T>(this T num) => (int)Ceiling((ToDouble(num) + 1) / 50) * 50;

        /// <summary>
        /// Number to G span.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Number to G span.</returns>
        internal static decimal ToGSpan(this decimal num) => num < MAX_XFMR_G ? num * Default.Span : num;
    }
}
