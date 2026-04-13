namespace Moedelo.BankIntegrations.Enums
{
    public enum SsoRequestStatus
    {
        /// <summary>
        /// Авторизация прошла успешно
        /// </summary>
        Success = 0,

        /// <summary>
        /// Ошибка получения информации о клиенте из банка
        /// </summary>
        ErrorClientInfo = 1,

        /// <summary>
        /// Ошибка валидации клиентских данных
        /// </summary>
        ValidationError = 2,

        /// <summary>
        /// Ошибка при регистрации пользователя
        /// </summary>
        RegistationError = 3,

        /// <summary>
        /// Ошибка при авторизации
        /// </summary>
        AuthorizationError = 4,

        /// <summary>
        /// Ошибка получения выписки
        /// </summary>
        RequestMovementsError = 5,

        /// <summary>
        /// Любая другая ошибка
        /// </summary>
        Error = 99,

    }
}
