namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    /// <summary> Статус отправки </summary>
    public enum ErptDocumentStatus
    {
        /// <summary> Нет ни какого статуса </summary>
        None = 0,

        /// <summary> Создан </summary>
        Created = 1,

        /// <summary> Отправленн </summary>
        SMSSended = 10,

        /// <summary> Подтверждён пользователем </summary>
        UserCommited = 20,

        /// <summary> Помечен администратором как проблемный </summary>
        Problem = 30,

        /// <summary> Ожидание подтверждения из контура </summary>
        Sended = 40,

        /// <summary> Доставлен </summary>
        Delivered = 45,
        
        /// <summary> Обработан. Ответ от фонда есть, но до расшифрования мы не знаем, приняли или отклонили.
        /// СТЭК на своей стороне определение статусов делал, чтоб не растаскивать логику и всегда на статусы стэка смотреть </summary>
        Processed = 46,
        
        /// <summary> Отклонено администратором </summary>
        AdministratorReject = 50,

        /// <summary> Отклонено провайдером </summary>
        ProviderReject = 51,

        /// <summary> Получен отрицательный ответ </summary>
        FundReject = 60,

        /// <summary> Получен положительный ответ </summary>
        FundConfirm = 70,
        
        /// <summary> Принят частично </summary>
        FundConfirmedPartially = 71,
        
        /// <summary> Принят с уведомлением </summary>
        FundConfirmedWithNotification = 72,
        
        /// <summary> Письмо получено </summary>
        Received = 80,

        /// <summary> Письмо просмотрено </summary>
        Viewed = 90,

        /// <summary> Помечено удалённым </summary>
        Removed = 100,
        
        /// <summary> Создан (новый раздел Отчеты) </summary>
        /// <remarks> Временный статус. После успешного создания сущностей в новом разделе Отчеты будет изменено на Created (1)</remarks>
        CreatedViaNewCabinet = 110,
    }
}
