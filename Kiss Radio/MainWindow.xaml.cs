using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KissRadio
{
    public partial class MainWindow : Window
    {
        private Stopwatch _stopwatch = Stopwatch.StartNew();
        private BitmapImage PlayImg;
        private BitmapImage StopImg;
        private bool isPlaying;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
        }

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            PlayImg = (await ImageProcessing.ImageProcessing.GetBitmapImageFreezable(new Uri("https://www.kiss.cz/assets/player/images/play.png"))).IMG;
            StopImg = (await ImageProcessing.ImageProcessing.GetBitmapImageFreezable(new Uri("https://www.kiss.cz/assets/player/images/stop.png"))).IMG;

            mediaElement.Play();
            isPlaying = true;
            App.ViewModel.PlayIcon = StopImg;

            while (true)
            {
                await UpdateViewModelAsync();
                await Task.Delay(100);
            }
        }


        private async Task UpdateViewModelAsync()
        {
            try
            {
                string url = "https://www.kiss.cz/prave-hraje.json";
                var songData = await JsonDownloader.JsonDownloader.DownloadJsonAsync<SongData>(url);

                if (songData != null)
                {
                    songData.c_title = HttpUtility.HtmlDecode(songData.c_title);
                    songData.c_artist = HttpUtility.HtmlDecode(songData.c_artist);
                    App.ViewModel.Song = songData;
                    App.ViewModel.Title = $"Radio KISS - hraje: {songData.c_title} {songData.c_artist} po dobu: {_stopwatch.Elapsed}";
                }
            }
            catch (Exception ex)
            {
                App.ViewModel.Song = null;
            }
        }

        private void MediaElementMediaEnded(object sender, RoutedEventArgs e)
        {
            isPlaying = false;
            App.ViewModel.PlayIcon = PlayImg;
        }

        private void Play(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isPlaying)
            {
                // Pokud se přehrává, zastavte přehrávání
                mediaElement.Pause();
                isPlaying = false;
                App.ViewModel.PlayIcon = PlayImg; // Nastavte ikonu pro přehrávání
            }
            else
            {
                // Pokud se nepřehrává, spusťte přehrávání
                mediaElement.Play();
                isPlaying = true;
                App.ViewModel.PlayIcon = StopImg; // Nastavte ikonu pro zastavení
            }
        }
    }
}
