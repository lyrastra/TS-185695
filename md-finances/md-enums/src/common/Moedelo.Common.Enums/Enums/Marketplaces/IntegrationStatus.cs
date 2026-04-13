namespace Moedelo.Common.Enums.Enums.Marketplaces
{
    public enum IntegrationStatus
    {
        /// <summary>
        /// Не установлен
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// В рабочем состоянии
        /// </summary>
        Ok = 1,

        /// <summary>
        /// Время жизни токена истекло
        /// </summary>
        TokenDateExpired = 2,

        /// <summary>
        /// Токен не работоспособен
        /// </summary>
        NotValidToken = 3,

        /// <summary>
        /// Интеграция существует, но не активна
        /// </summary>
        Disabled = 4,

        /// <summary>
        /// Интеграция не существует
        /// </summary>
        NotExist = 5,
    }
}
