namespace KissRadio
{
    /// <summary>
    /// Interface reprezentující data o aktuálně hrané písni.
    /// </summary>
    public interface ISongData
    {
        /// <summary>
        /// Získá nebo nastaví název písně.
        /// </summary>
        string? c_title { get; set; }

        /// <summary>
        /// Získá nebo nastaví umělce písně.
        /// </summary>
        string? c_artist { get; set; }

        /// <summary>
        /// Získá nebo nastaví cílový čas písně.
        /// </summary>
        object? to { get; set; }

        /// <summary>
        /// Získá nebo nastaví počáteční čas písně.
        /// </summary>
        object? from { get; set; }
    }


    /// <summary>
    /// Třída reprezentující data o aktuálně hrané písni.
    /// </summary>
    public class SongData : ISongData
    {
        /// <inheritdoc/>
        public string? c_title { get; set; }

        /// <inheritdoc/>
        public string? c_artist { get; set; }

        /// <inheritdoc/>
        public object? to { get; set; }

        /// <inheritdoc/>
        public object? from { get; set; }
    }
}