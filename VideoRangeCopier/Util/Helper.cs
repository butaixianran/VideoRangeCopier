using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace VideoRangeCopier.Util
{
    public static class Helper
    {
        public static void Log(string msg)
        {

            Debug.WriteLine($"{Global.AppName}:{msg}");
        }

        public static void ShowErrorMsg()
        {
            if (string.IsNullOrEmpty(Global.Error)) { return; }

            MsgBox.Alert("Error", Global.Error);
        }
    }
}
