using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace KissRadio.ViewModel
{
    public class SongViewModel : ViewModelBase
    {
        private SongData? _song;
        private string? _title;
        private BitmapImage? _playicon;
        private double? _volume;
        private ObservableCollection<ListSong?> _songs;

        public SongViewModel()
        {
            Song = new SongData();
            Volume = 0.5;
            Songs = new ObservableCollection<ListSong?>();
        }

        /// <summary>
        /// Reprezentuje informace o aktuální písni, včetně umělce, názvu a dalších údajů.
        /// </summary>
        public SongData? Song
        {
            get => _song;
            set => SetProperty(ref _song, value);
        }

        /// <summary>
        /// Obsahuje název aktuální písně nebo zvoleného titulu.
        /// </summary>
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public BitmapImage? PlayIcon
        {
            get => _playicon;
            set => SetProperty(ref _playicon, value);
        }

        public double? Volume
        {
            get => _volume;
            set => SetProperty(ref _volume, value);
        }
        public ObservableCollection<ListSong?> Songs
        {
            get => _songs;
            set => SetProperty(ref _songs, value);
        }

        public BitmapImage LogoImg { get; set; }
        public BitmapImage BackgroundImg { get; set; }
    }
}
