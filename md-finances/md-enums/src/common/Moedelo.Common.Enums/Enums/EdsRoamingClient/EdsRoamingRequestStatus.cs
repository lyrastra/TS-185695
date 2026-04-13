namespace Moedelo.Common.Enums.Enums.EdsRoamingClient
{
    /// <summary>
    /// Возможные состояния заявки на настройку ЭДО. Возвращаются https://roaming.edo.keydisk.ru/abonent/status/2AE<ГУИД>/<ИД_заявки>
    /// </summary>
    public enum EdsRoamingRequestStatus
    {
        /// <summary>
        /// Несуществующий статус, нужен для тестов
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Заявка в работе
        /// </summary>
        InProgress = 0,

        /// <summary>
        /// Связь настроена. (старый коммент от Артём Ветров: узнать реальный код для настроенной связи)
        /// </summary>
        Completed = 1
    }
}
