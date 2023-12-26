using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Linq;
using System.IO;
using VideoRangeCopier.Util;

namespace VideoRangeCopier.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            AddHandler(DragDrop.DropEvent, File_Drop);
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

            Global.VideoOpt.GetInfoByUri(file.Name, file.Path);

            VideoPathTextBlock.Text = Global.VideoOpt.path;
            VideoDurationTextBlock.Text = $"Duration: {Global.VideoOpt.duration}";
            VideoInfoPanel.IsVisible = true;

        }

        private void FromStart_Checked(object sender, RoutedEventArgs args)
        {
            FromTextBox.Text = "00:00:00";
        }

        private void ToEnd_Checked(object sender, RoutedEventArgs args)
        {
            ToTextBox.Text = new DateTime(Global.VideoOpt.duration.Ticks).ToString("HH:mm:ss");
        }

        private async void Start_Click(object sender, RoutedEventArgs args)
        {
            if (FromTextBox.Text is null) { return;}
            if (ToTextBox.Text is null) { return; }

            try
            {
                Global.VideoOpt.from = TimeSpan.Parse(FromTextBox.Text);
                Global.VideoOpt.to = TimeSpan.Parse(ToTextBox.Text);
            }
            catch (Exception e)
            {
                Global.Error = e.Message;
                Helper.ShowErrorMsg();
                return;
            }

            LogTextBlock.Text = "Processing...";

            bool result = await VideoHelper.CopyRange();
            if (!string.IsNullOrEmpty(Global.Error))
            {
                Helper.ShowErrorMsg();
                LogTextBlock.Text = "Copy video range failed";
                return;
            }
            else if (!result)
            {
                LogTextBlock.Text = "Copy video range failed";
                return;
            }

            LogTextBlock.Text = "Done";
            return;

        }

        private async void File_Drop(object? sender, DragEventArgs e)
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

            await Global.VideoOpt.GetInfoByUri(item.Name, item.Path);

            VideoPathTextBlock.Text = Global.VideoOpt.path;
            VideoDurationTextBlock.Text = $"Duration: {Global.VideoOpt.duration}";
            VideoInfoPanel.IsVisible = true;

        }


    }
}
