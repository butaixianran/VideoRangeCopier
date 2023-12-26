using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRangeCopier.Util
{
    public static class MsgBox
    {
        public static async Task<ButtonResult> AlertNoCap(string msg)
        {
            return await Alert("", msg);
        }

        public static async Task<ButtonResult> Alert(string caption, string msg)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(caption, msg, ButtonEnum.Ok);

            return await box.ShowAsync();
        }

        public static async Task<ButtonResult> ConfirmNoCap(string msg)
        {
            return await Confirm("", msg);
        }


        public static async Task<ButtonResult> Confirm(string caption, string msg)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(caption, msg, ButtonEnum.OkCancel);

            return await box.ShowAsync();
        }

    }
}
