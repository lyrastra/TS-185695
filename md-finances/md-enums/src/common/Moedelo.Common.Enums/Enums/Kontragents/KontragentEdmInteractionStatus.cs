namespace Moedelo.Common.Enums.Enums.Kontragents
{
    /// <summary>
    /// Статус ЭДО у контрагента пользователя
    /// </summary>
    public enum KontragentEdmInteractionStatus
    {
        /// <summary>
        /// Взаимодействие по ЭДО недоступно
        /// </summary>
        Default = 0,

        /// <summary>
        /// ЭДО включено
        /// </summary>
        EdmIsOn = 1,

        /// <summary>
        /// ЭДО доступно контрагенту
        /// </summary>
        EdmIsAvailable = 2,

        /// <summary>
        /// О доступности ЭДО у контрагента неизвестно
        /// </summary>
        UnknownEdmStatus = 3,

        /// <summary>
        /// Отправил пользователю приглашение (Пришло приглашение)
        /// </summary>
        SentInvitationToUser = 4,

        /// <summary>
        /// Отправили приглашение контрагенту (Приглашение отправлено) 
        /// </summary>
        SentInvitationToKontragent = 5,

        /// <summary>
        /// Приглашение от контрагента отклонено (Приглашение отклонено)
        /// </summary>
        DeclinedInvitationToUser = 6,

        /// <summary>
        /// Приглашение от пользователя отклонено контрагентом (Отправленное приглашение отклонено)
        /// </summary>
        DeclinedInvitationToKontragent = 7,

        /// <summary>
        /// Данный контрагент является дубликатом
        /// </summary>
        Duplicate = 8
    }
}