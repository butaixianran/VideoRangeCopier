using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VideoRangeCopier.Util;

namespace VideoRangeCopier.Model
{
    public class MergeOption
    {
        public string video_name = "";
        public string video_path = "";

        public string audio_path = "";

        public string output_path = "";

        public void GetVideoInfoByUri(string file_name, Uri file_path)
        {
            video_path = file_path.ToString().Replace("file:///", "");
            video_name = file_name;

            string ext = Path.GetExtension(video_name);

            string name_no_ext = video_name.Replace(ext, "");

            string new_ext = ext;
            if (new_ext != ".mp4" && new_ext != ".mkv")
            {
                new_ext = ".mp4";
            }

            string new_name = name_no_ext + "_merged" + new_ext;

            output_path = video_path.Replace(video_name, new_name);


            Helper.Log($"video name is:{video_name}");
            Helper.Log($"video path is:{video_path}");
            Helper.Log($"output path is:{output_path}");

        }

        public void GetAudioInfoByUri(Uri file_path)
        {
            audio_path = file_path.ToString().Replace("file:///", "");
        }

    }


}
