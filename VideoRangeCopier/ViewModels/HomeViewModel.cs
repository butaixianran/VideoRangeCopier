using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRangeCopier.Util;

namespace VideoRangeCopier.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {


        /// <summary>
        /// Go to settting page
        /// </summary>
        public void SettingClick()
        {
            if (Global.GotoPage is null) { return; }
            Global.GotoPage("setting");
        }

        public void SelectVideoFile()
        {
            if (Global.GotoPage is null) { return; }
            Global.GotoPage("setting");
        }

    }
}
