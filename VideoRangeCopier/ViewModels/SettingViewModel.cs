using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRangeCopier.Util;

namespace VideoRangeCopier.ViewModels
{
    public partial class SettingViewModel : ViewModelBase
    {

        public void BackToHomeClick()
        {
            if (Global.GotoPage is null) { return; }
            Global.GotoPage("home");
        }
    }
}
