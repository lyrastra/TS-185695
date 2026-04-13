namespace Moedelo.Common.Enums.Enums.Payroll
{
    /// <summary>
    /// Типы платежей в фонды
    /// </summary>
    public enum FundChargeType
    {
        /// <summary>
        /// На страховую часть пенсии в ПФР
        /// </summary>
        InsurancePfr = 0,

        /// <summary>
        /// На накопительную часть пенсии в ПФР
        /// </summary>
        AccumulatePfr = 1,

        /// <summary>
        /// В Федеральный ФОМС
        /// </summary>
        FederalFoms = 2,

        /// <summary>
        /// В Территориальный ФОМС
        /// </summary>
        TerretorialFoms = 3,

        /// <summary>
        /// На нетрудопособность в ФСС
        /// </summary>
        DisabilityFss = 4,

        /// <summary>
        /// На травматизм в ФСС
        /// </summary>
        InjuredFss = 5,
        
        /// <summary>
        /// Солидарная часть для ПФР, начисляемая на суммы, 
        /// превыщающие страховую базу (введена с 2012 года)
        /// </summary>
        InsuranceSolidaryPart = 6,

        /// <summary>
        /// Солидарная часть для ФСС, начисляемая на суммы, 
        /// превыщающие страховую базу (введена с 2012 года)
        /// </summary>
        DisabilityFssSolidaryPart = 7,

        /// <summary>
        /// ОПС, ОМС, ОСС по ВНИм
        /// </summary>
        Sfr = 8,
    }
}
