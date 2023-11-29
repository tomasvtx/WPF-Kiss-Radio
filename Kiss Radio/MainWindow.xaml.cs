using JsonDownloader;
using Notification.Wpf;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KissRadio
{
    public partial class MainWindow : Window
    {
        private const string PLAY = "img\\play.png";
        private const string STOP = "img\\stop.png";
        private const string BACKGROUND = "img\\background.jpg";
        private const string LOGO = "img\\logo.png";
        private const string JSON = "https://www.kiss.cz/prave-hraje.json";

        private Stopwatch _stopwatch = Stopwatch.StartNew();
        private BitmapImage PlayImg;
        private BitmapImage StopImg;
        private bool isPlaying;
        private JsonDownload jsonDownload;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindowLoaded;
        }

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Uri play = new(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PLAY));
            PlayImg = (await ImageProcessing.ImageProcessing.GetBitmapImageFreezable(play)).IMG;

            Uri stop = new(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, STOP));
            StopImg = (await ImageProcessing.ImageProcessing.GetBitmapImageFreezable(stop)).IMG;

            Uri background = new(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BACKGROUND));
            App.ViewModel.BackgroundImg = (await ImageProcessing.ImageProcessing.GetBitmapImageFreezable(background)).IMG;

            Uri logo = new(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOGO));
            App.ViewModel.LogoImg = (await ImageProcessing.ImageProcessing.GetBitmapImageFreezable(logo)).IMG;

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
                string url = JSON;

                jsonDownload = new JsonDownload(new System.Net.Http.HttpClient());
                var songData = await jsonDownload.GetJsonAsync<SongData>(url);

                if (songData != null && songData?.DataJson != null)
                {
                    songData.DataJson.c_title = HttpUtility.HtmlDecode(songData?.DataJson?.c_title);
                    songData.DataJson.c_artist = HttpUtility.HtmlDecode(songData?.DataJson?.c_artist);
                    App.ViewModel.Song = songData?.DataJson;
                    App.ViewModel.Title = $"Radio KISS - hraje: {songData?.DataJson?.c_title} {songData?.DataJson?.c_artist} po dobu: {_stopwatch?.Elapsed}";

                    if (App.ViewModel?.Songs?.LastOrDefault()?.c_artist != App.ViewModel?.Song?.c_artist)
                    {
                        App.ViewModel.Songs.Add(new ListSong { c_artist = App.ViewModel?.Song?.c_artist , c_title = App.ViewModel?.Song?.c_title, DateTime = DateTime.Now});
                    }
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
            Play();
        }

        private void Play()
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

        private void ThumbnailPauseButtonClick(object sender, EventArgs e)
        {
            Play();
        }
    }
}
