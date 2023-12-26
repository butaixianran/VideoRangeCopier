using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRangeCopier.Util;

namespace VideoRangeCopier.Model
{
    public class VideoOption
    {
        public string name = "";
        public string path = "";
        public string folder = "";
        public string ffmpeg_path = "";
        public TimeSpan duration = new TimeSpan();
        public TimeSpan from = new TimeSpan();
        public TimeSpan to = new TimeSpan();


        public string RangeName()
        {
            string ext = Path.GetExtension(name);

            string name_no_ext = name.Replace(ext, "");

            string range_part = $"{from}-{to}".Replace(":", "");

            return  $"{name_no_ext}({range_part}){ext}";
        }


        /// <summary>
        /// Clear data for next video
        /// </summary>
        public void Clear()
        {
            name = "";
            path = "";
            ffmpeg_path = "";
            duration = new TimeSpan();
            from = new TimeSpan();
            to = new TimeSpan();
        }

        public async Task<bool> GetInfoByUri(string file_name, Uri file_path)
        {
            path = file_path.ToString().Replace("file:///", "");
            name = file_name;
            folder = path.Replace(name, "");

            duration = await VideoHelper.GetVideoDuration(Global.VideoOpt.path);
            if (!string.IsNullOrEmpty(Global.Error))
            {
                Helper.ShowErrorMsg();
                return false;
            }

            Helper.Log($"video path is:{path}");
            Helper.Log($"video name is:{name}");
            Helper.Log($"video folder is:{folder}");
            Helper.Log($"video duration is:{duration}");

            return true;
        }

    }
}
