using Aspose.Pdf.Annotations;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.IO;
using FFmpeg.AutoGen;
using System.Runtime.InteropServices;

namespace VIdeo_Editor
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private string outputFilePath1;
        private string outputFilePath2;
        private bool isCuttingComplete = false;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            timer.Tick += Timer_Tick;

            //Closing += MainWindow_Closing;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (videoPreview.NaturalDuration.HasTimeSpan)
            {
                timelineSlider.Value = videoPreview.Position.TotalSeconds;
            }
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Video Files|*.mp4;*.avi;*.mov;*.mkv|All Files|*.*";

            if (fileDialog.ShowDialog() == true)
            {
                string selectedFile = fileDialog.FileName;
                videoPreview.Source = new Uri(selectedFile);
                videoPreview.LoadedBehavior = MediaState.Manual;
                videoPreview.UnloadedBehavior = MediaState.Manual;
                videoPreview.Play();
                timer.Start(); // Bắt đầu cập nhật Slider khi video được phát
            }
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            videoPreview.Play();
            timer.Start(); // Bắt đầu cập nhật Slider khi video được phát
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            videoPreview.Pause();
            timer.Stop(); // Dừng cập nhật Slider khi video được tạm dừng
        }

        private void timelineSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (videoPreview.NaturalDuration.HasTimeSpan)
            {
                videoPreview.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            }
        }

        private async Task ExecuteFFmpegCommandAsync(string ffmpegPath, string arguments)
        {
            await Task.Run(() =>
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = ffmpegPath;
                psi.Arguments = arguments;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;

                using (Process ffmpegProcess = new Process())
                {
                    ffmpegProcess.StartInfo = psi;

                    try
                    {
                        ffmpegProcess.Start();
                        ffmpegProcess.WaitForExit(); // Chờ cho FFmpeg hoàn thành
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ nếu có
                        MessageBox.Show($"Error executing FFmpeg: {ex.Message}");
                    }
                }
            });
        }

        private async void cutBtn_Click_1(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem video đã được tải lên chưa
            if (videoPreview.Source == null)
            {
                MessageBox.Show("Please select a file to split.");
                return;
            }

            // Lấy giá trị của slider
            double sliderValue = timelineSlider.Value;
            TimeSpan startTime = TimeSpan.FromSeconds(0);
            TimeSpan endTime = TimeSpan.FromSeconds(sliderValue);

            string inputFilePath = videoPreview.Source.LocalPath;
            string ffmpegPath = @"D:\FFmPeg\bin\ffmpeg.exe"; // Đường dẫn đến tập lệnh ffmpeg

            // Tạo tên tạm thời cho tập tin đầu ra
            outputFilePath1 = Path.Combine(Path.GetTempPath(), "output1.mp4");
            outputFilePath2 = Path.Combine(Path.GetTempPath(), "output2.mp4");
            video1.LoadedBehavior = MediaState.Manual;
            video2.LoadedBehavior = MediaState.Manual;
            video1.UnloadedBehavior = MediaState.Manual;
            video2.UnloadedBehavior = MediaState.Manual;

            string arguments1 = $"-i \"{inputFilePath}\" -ss {startTime} -to {endTime} -c:v copy -c:a copy \"{outputFilePath1}\"";
            string arguments2 = $"-i \"{inputFilePath}\" -ss {endTime} -c:v copy -c:a copy \"{outputFilePath2}\"";

            // Thực hiện cắt video bằng FFmpeg
            ExecuteFFmpegCommandAsync(ffmpegPath, arguments1);
            ExecuteFFmpegCommandAsync(ffmpegPath, arguments2);

            // Xóa nội dung của video1 và video2 trước khi gán nguồn mới
            video1.Source = null;
            video2.Source = null;

            // Load video mới vào hai MediaElement và phát
            video1.Source = new Uri(outputFilePath1);
            video2.Source = new Uri(outputFilePath2);

            // Khi video đã được tải lên hoàn tất, thì phát video
            video1.MediaOpened += Video1_MediaOpened;
            video2.MediaOpened += Video2_MediaOpened;

            // Xóa video tạm sau khi ứng dụng đã hoàn thành việc phát video
            video1.MediaEnded += (sender, args) =>
            {
                File.Delete(outputFilePath1);
            };

            video2.MediaEnded += (sender, args) =>
            {
                File.Delete(outputFilePath2);
            };

            MessageBox.Show("Video has been cut.");
        }

        private void Video1_MediaOpened(object sender, RoutedEventArgs e)
        {
            video1.Play();
        }

        private void Video2_MediaOpened(object sender, RoutedEventArgs e)
        {
            video2.Play();
        }

        private void playV1_Click(object sender, RoutedEventArgs e)
        {
            video1.Play();
        }

        private void pauseV1_Click(object sender, RoutedEventArgs e)
        {
            video1.Pause();
        }

        private void playV2_Click(object sender, RoutedEventArgs e)
        {
            video2.Play();
        }

        private void pauseV2_Click(object sender, RoutedEventArgs e)
        {
            video2.Pause();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MP4 File (*.mp4)|*.mp4|All Files (*.*)|*.*";
            saveFileDialog.FileName = "output";

            if (saveFileDialog.ShowDialog() == true)
            {
                string outputPath1 = saveFileDialog.FileName.Replace(".mp4", "_part1.mp4");
                string outputPath2 = saveFileDialog.FileName.Replace(".mp4", "_part2.mp4");

                try
                {
                    File.Move(outputFilePath1, outputPath1);
                    File.Move(outputFilePath2, outputPath2);
                    MessageBox.Show("Videos saved successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving videos: {ex.Message}");
                }
            }
        }

        //private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    File.Delete(outputFilePath1);
        //    File.Delete(outputFilePath2);
        //}
    }
}