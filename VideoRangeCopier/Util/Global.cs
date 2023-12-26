using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRangeCopier.Model;

namespace VideoRangeCopier.Util
{
    public static class Global
    {
        public static string AppName = "VideoRangeCopier";
        public static string AppVersion = "";

        public static string Error = "";

        public static Action<string>? GotoPage;

        public static VideoOption VideoOpt = new VideoOption();
    }
}
