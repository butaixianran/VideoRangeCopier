using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using VideoRangeCopier.Util;
using System;
using System.IO;

using System.Linq;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace VideoRangeCopier.Views
{
    public partial class MergeView : UserControl
    {
        public MergeView()
        {
            InitializeComponent();
            DropVideoBorder.AddHandler(DragDrop.DropEvent, DropVideo);
            DropAudioBorder.AddHandler(DragDrop.DropEvent, DropAudio);
        }

        private async void SelectVideoFile_Clicked(object sender, RoutedEventArgs args)
        {
            Helper.Log("Run SelectVideoFile_Clicked");

            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel is null) { return; }

            // Start async operation to open the dialog.
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Video File",
                AllowMultiple = false
            });

            if (files.Count < 1)
            {
                return;
            }

            var file = files[0];

            Global.MergeOpt.GetVideoInfoByUri(file.Name, file.Path);

            VideoPathTextBlock.Text = Global.MergeOpt.video_path;

        }

        private async void SelectAudioFile_Clicked(object sender, RoutedEventArgs args)
        {
            Helper.Log("Run SelectAudioFile_Clicked");

            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel is null) { return; }

            // Start async operation to open the dialog.
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Video File",
                AllowMultiple = false
            });

            if (files.Count < 1)
            {
                return;
            }

            var file = files[0];

            Global.MergeOpt.GetAudioInfoByUri(file.Path);

            AudioPathTextBlock.Text = Global.MergeOpt.audio_path;

        }

        private void DropVideo(object? sender, DragEventArgs e)
        {
            if (!e.Data.Contains(DataFormats.Files)) { return; }

            var files = e.Data.GetFiles() ?? Array.Empty<IStorageItem>();

            if (files.Count() < 1)
            {
                return;
            }

            var item = files.First();

            if (item is null) { return; }

            if (!(item is IStorageFile))
            {
                return;
            }

            Helper.Log($"Dragged Item name:{item.Name}");
            Helper.Log($"Dragged Item path:{item.Path}");

            Global.MergeOpt.GetVideoInfoByUri(item.Name, item.Path);
            VideoPathTextBlock.Text = Global.MergeOpt.video_path;

        }

        private void DropAudio(object? sender, DragEventArgs e)
        {
            if (!e.Data.Contains(DataFormats.Files)) { return; }

            var files = e.Data.GetFiles() ?? Array.Empty<IStorageItem>();

            if (files.Count() < 1)
            {
                return;
            }

            var item = files.First();

            if (item is null) { return; }

            if (!(item is IStorageFile))
            {
                return;
            }

            Helper.Log($"Dragged Item name:{item.Name}");
            Helper.Log($"Dragged Item path:{item.Path}");

            Global.MergeOpt.GetAudioInfoByUri(item.Path);
            AudioPathTextBlock.Text = Global.MergeOpt.audio_path;

        }

        private async void Start_Click(object sender, RoutedEventArgs args)
        {

            LogTextBlock.Text = "Processing...";

            bool result = await VideoHelper.MergeVideoAndAudio();
            if (!string.IsNullOrEmpty(Global.Error))
            {
                Helper.ShowErrorMsg();
                LogTextBlock.Text = "Merge failed";
                return;
            }
            else if (!result)
            {
                LogTextBlock.Text = "Merge failed";
                return;
            }

            LogTextBlock.Text = "Done";
            return;

        }
    }
}
