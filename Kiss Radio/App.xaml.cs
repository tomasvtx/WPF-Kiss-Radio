using System.Windows;

namespace KissRadio
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Získá sdílenou instanci třídy SongViewModel, která je používána v celé aplikaci.
        /// </summary>
        public static ViewModel.SongViewModel ViewModel = new();

        /// <summary>
        /// Obsluhuje událost spuštění aplikace.
        /// </summary>
        /// <param name="e">Argumenty události spuštění.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Vytvoří a zobrazí hlavní okno
            MainWindow mainWindow = new();
            mainWindow.Show();

            // Nastaví sdílený SongViewModel jako datový kontext pro hlavní okno
            mainWindow.DataContext = ViewModel;
        }
    }
}
