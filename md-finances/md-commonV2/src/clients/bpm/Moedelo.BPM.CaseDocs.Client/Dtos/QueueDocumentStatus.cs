namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Статус обработки документа
    /// </summary>
    public enum QueueDocumentStatus
    {
        /// <summary>
        ///     В обработке
        /// </summary>
        Proccessing = 1,

        /// <summary>
        ///     Дубликат
        /// </summary>
        Duplicate = 2,

        /// <summary>
        ///     Черновик
        /// </summary>
        Draft = 3,

        /// <summary>
        ///     Не учитывать, документ не содержит информации
        /// </summary>
        Disregard = 4,

        /// <summary>
        ///     Документ успешно обработан
        /// </summary>
        Done = 5,

        /// <summary>
        ///     Поврежден
        /// </summary>
        Damaged = 6,

        /// <summary>
        ///     Плохое качество
        /// </summary>
        PoorImageQuality = 7,

        /// <summary>
        ///     Неполный документ (не хватает страниц)
        /// </summary>
        Incomplete = 8,

        /// <summary>
        ///     Документ без подписи/печати
        /// </summary>
        WithoutStamp = 9
    }
}