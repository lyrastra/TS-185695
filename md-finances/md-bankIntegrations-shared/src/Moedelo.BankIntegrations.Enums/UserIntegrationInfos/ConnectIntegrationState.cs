namespace Moedelo.BankIntegrations.Enums.UserIntegrationInfos
{
    /// <summary>
    /// Состояние чекбокса подключить интеграцию в окне добавления счета
    /// </summary>
    public enum ConnectIntegrationState
    {
        /// <summary>
        /// Не показыать чекбокс, если у банка нет интеграции, либо интеграция уже подключена
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// Показывать чекбокс, если есть возможность подключить интеграцию с банком
        /// </summary>
        Enabled,
        /// <summary>
        /// Показать задезейбленый чекбокс, если достигнут лимит интеграций
        /// </summary>
        Limited,
    }
}