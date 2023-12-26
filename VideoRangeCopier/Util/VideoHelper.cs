using FFMpegCore;
using FFMpegCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRangeCopier.Util
{
    public static class VideoHelper
    {
        public static async Task<TimeSpan> GetVideoDuration(string video_path)
        {
            TimeSpan result = new TimeSpan();

            IMediaAnalysis mediaInfo;

            try
            {
                mediaInfo = await FFProbe.AnalyseAsync(video_path);
            }
            catch (Exception e)
            {
                Global.Error = e.Message;
                return result;
            }
            
            if (mediaInfo is null)
            {
                Global.Error = "mediaInfo is null";
                return result;
            }

            return mediaInfo.Duration;
        }

        public static async Task<bool> CopyRange()
        {
            string output_name = Global.VideoOpt.RangeName();
            Helper.Log($"output_name:{output_name}");
            string output_path = Global.VideoOpt.path.Replace(Global.VideoOpt.name, output_name);

            string command = $"-c copy -ss {Global.VideoOpt.from} -to {Global.VideoOpt.to}";

            bool result = false;

            try
            {
                result = await FFMpegArguments
                    .FromFileInput(Global.VideoOpt.path)
                    .OutputToFile(output_path, false, options => options
                    .WithCustomArgument(command))
                    .ProcessAsynchronously();
            }
            catch (Exception e)
            {
                Global.Error = e.Message;
                return false;
            }

            return result;


        }

        public static async Task<bool> MergeVideoAndAudio()
        {
            string command = $"-c copy -map 0:v -map 1:a";

            bool result = false;

            try
            {
                result = await FFMpegArguments
                    .FromFileInput(Global.MergeOpt.video_path)
                    .AddFileInput(Global.MergeOpt.audio_path)
                    .OutputToFile(Global.MergeOpt.output_path, false, options => options
                    .WithCustomArgument(command))
                    .ProcessAsynchronously();
            }
            catch (Exception e)
            {
                Global.Error = e.Message;
                return false;
            }

            return result;


        }


    }
}
