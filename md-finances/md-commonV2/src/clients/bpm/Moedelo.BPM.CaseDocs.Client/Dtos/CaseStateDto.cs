namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Состояние обращения
    /// </summary>
    public enum CaseStateDto
    {
        /// <summary>
        ///     Открыт
        /// </summary>
        Open = 1,

        /// <summary>
        ///     Окрыт повторно
        /// </summary>
        OpenAgain = 2,

        /// <summary>
        ///     Назначено
        /// </summary>
        Assigned = 3,

        /// <summary>
        ///     В работе
        /// </summary>
        InWork = 4,

        /// <summary>
        ///     Связаться с клиентом
        /// </summary>
        ContactClient = 5,

        /// <summary>
        ///     Закрыт
        /// </summary>
        Closed = 6,

        /// <summary>
        ///     Не доставлено
        /// </summary>
        NotDelivered = 7,

        /// <summary>
        ///     Новый
        /// </summary>
        New = 8,

        /// <summary>
        ///     Передано через kayako
        /// </summary>
        Kayko = 9,

        /// <summary>
        ///     Принято
        /// </summary>
        Accepted = 10
    }
}