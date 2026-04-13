namespace Moedelo.Common.Enums.Enums
{
    /// <summary>
    /// используется для отображения состояния загрузки выписки на главной
    /// для пользователей с тарифом Сбербанка "ИП 6%"
    /// </summary>
    public enum MovementRequestStatusType
    {
        Default = 0,
        
        // В реквизитах не заполнен ИНН
        NoInn = 1,
        
        // Не включена интеграция
        NoIntegration = 2,
        
        // Идет процесс получения выписки
        InProgress = 3,
        
        // Выписка получена
        MovementRequestReceived = 4,
        
        // В процессе получения статуса произошла ошибка
        Error = 666
    }
}