namespace Moedelo.Common.Enums.Enums.RegistrationService
{
    /// <summary>
    /// Способы регистрации
    /// </summary>
    public enum RegistrationMethod
    {
        /// <summary>
        /// Неопределено
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Лендинг
        /// </summary>
        Landing = 1,

        /// <summary>
        /// Лендинг по реферальной ссылке 
        /// </summary>
        LandingByReferralLink = 2,

        /// <summary>
        /// Партнерский кабинет
        /// </summary>
        PartnerCabinet = 3,

        /// <summary>
        /// X-WSSE
        /// </summary>
        XWSSE = 4,

        /// <summary>
        /// WhiteLabel
        /// </summary>
        WhiteLabel = 5,

        /// <summary>
        /// Account
        /// </summary>
        Account = 6,

        /// <summary>
        /// Мобильное приложение
        /// </summary>
        Mobile = 7
    }
}