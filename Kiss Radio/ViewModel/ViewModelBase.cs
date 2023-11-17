using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KissRadio.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifikuje o změně vlastnosti.
        /// </summary>
        /// <param name="propertyName">Název změněné vlastnosti.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Nastaví hodnotu vlastnosti a notifikuje o změně, pokud dojde ke změně.
        /// </summary>
        /// <typeparam name="T">Typ vlastnosti.</typeparam>
        /// <param name="storage">Odkaz na úložiště vlastnosti.</param>
        /// <param name="value">Nová hodnota vlastnosti.</param>
        /// <param name="propertyName">Název vlastnosti (nepovinný, odvozen automaticky).</param>
        /// <returns>True, pokud došlo ke změně hodnoty; jinak false.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
