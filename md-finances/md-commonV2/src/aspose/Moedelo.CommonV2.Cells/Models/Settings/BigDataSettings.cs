namespace Moedelo.CommonV2.Cells.Models.Settings
{
    /// <summary>
    /// Настройки импорта больших данных
    /// </summary>
    public class BigDataSettings
    {
        /// <summary>
        /// Название импортируемой коллекции
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Начальная позиция по горизонтали
        /// </summary>
        public int StartRow { get; set; }

        /// <summary>
        /// Начальная позиция по вертикали
        /// </summary>
        public int StartColumn { get; set; }
    }
}