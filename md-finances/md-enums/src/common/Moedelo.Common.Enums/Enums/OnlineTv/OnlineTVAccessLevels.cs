namespace Moedelo.Common.Enums.Enums.OnlineTv
{
    public enum OnlineTvAccessLevels
    {
        /// <summary>
        /// Доступ для всех
        /// </summary>
        ForAll = 1,

        /// <summary>
        /// Доступ только для зарегистрированных на событие
        /// </summary>
        OnlyEventRegistered = 2,

        /// <summary>
        /// Доступ для пользователей, зарегистрированных в сервисе и оставивших заявку на участие
        /// </summary>
        OnlyUserAndEventRegistered = 3,

        /// <summary>
        /// Доступ для платных пользователей
        /// </summary>
        OnlyPaidUsers = 4,

        /// <summary>
        /// Доступ для платных про пользователей
        /// </summary>
        OnlyPaidProUsers = 5,

        /// <summary>
        /// Доступ для платных биз пользователей
        /// </summary>
        OnlyPaidBizUsers = 6
    }
}