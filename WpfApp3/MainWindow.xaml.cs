using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public bool MusicFlag = true;
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
                lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            else
                lblStatus.Content = "No file selected...";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (MusicFlag) { mediaPlayer.Pause(); btnPlay.Content = "Play"; MusicFlag = false; }
            else { mediaPlayer.Play(); btnPlay.Content = "Pause"; MusicFlag = true; }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s1, s2;
            double x;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                //s1 = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm");
                //s2 = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"ss");
                //x = (double)s1 + (double)s2;
                if(mediaPlayer.NaturalDuration.HasTimeSpan) Slider1.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                mediaPlayer.Play(); //надо починить
            }
        }

        private void Slider1_ValueChanged(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
            mediaPlayer.Position = TimeSpan.FromSeconds(Slider1.Value);
            mediaPlayer.Play();
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volume = SliderVolume.Value;
            mediaPlayer.Volume = volume;
        }
    }
}
