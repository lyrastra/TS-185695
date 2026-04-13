namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    /// <summary>
    /// Поддерживаемые направления для отправки по электронной подписи
    /// </summary>
    public enum OutgoingDirectionType
    {

        /// <summary>
        /// Пенсионный фонд (ПФР)
        /// </summary>
        PensionFund = 2,

        /// <summary>
        /// Федеральная служба статистики (РОССТАТ)
        /// </summary>
        StatisticalService = 4,

        /// <summary>
        /// Фонд социального страхования (ФСС)
        /// </summary>
        SocialInsuranceFund = 5,

        /// <summary>
        /// Федеральная налоговая служба (ФНС)
        /// </summary>
        TaxService = 7,

        /// <summary>
        /// Электронный документооборот для ФНС (ЭДО ФНС)
        /// </summary>
        EdmTaxService = 10
    }
}
