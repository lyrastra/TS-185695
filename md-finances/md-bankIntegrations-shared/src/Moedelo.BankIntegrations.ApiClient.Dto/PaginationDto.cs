namespace Moedelo.BankIntegrations.ApiClient.Dto
{

    public class PaginatedCollectionDto<T>
    {
        /// <summary>
        /// Смещение в общем списке, начиная с которого выбраны элементы
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Использованное ограничение на количество выбранных элементов
        /// </summary>
        public int RequestedMaxCount { get; set; }

        /// <summary>
        /// Количество элементов в общем списке (без ограничений)
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Выбранный подсписок элементов из общего списка: начиная с <see cref="Offset"/> не более <see cref="RequestedMaxCount"/> элементов 
        /// </summary>
        public T[] Items { get; set; }
    }
}
