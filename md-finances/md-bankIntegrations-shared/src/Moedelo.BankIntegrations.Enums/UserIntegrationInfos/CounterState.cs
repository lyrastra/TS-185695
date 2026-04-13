namespace Moedelo.BankIntegrations.Enums.UserIntegrationInfos
{
    /// <summary>
    /// Состояние счетчика возможных интеграций в деньгах
    /// </summary>
    public enum UserIntegrationState
    {
        /// <summary>
        /// Ничего не показывать, если все возможные интеграции подключены и нет счета, по которому можно подключить интеграцию
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// Покзывать круг, если нет ни одного счета по которому можно подключить интеграцию
        /// </summary>
        Circle,
        /// <summary>
        /// Показывать колличество доступных интеграций, если есть счета по которым можно подключить интеграцию
        /// </summary>
        Counter,
    }
}