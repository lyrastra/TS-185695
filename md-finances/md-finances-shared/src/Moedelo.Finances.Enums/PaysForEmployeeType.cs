namespace Moedelo.Finances.Enums
{
    /// <summary>
    /// Тип платежа за сотрудников
    /// </summary>
    public enum PaysForEmployeeType
    {
        None = -1,

        /// <summary>
        /// Платеж в ПФР на накопительную часть
        /// </summary>
        PfrAccumulate = 0,

        /// <summary>
        /// Платеж в ПФР на страховую часть
        /// </summary>
        PfrInsurance = 1,

        /// <summary>
        /// Платеж в ФСС за травматизм
        /// </summary>
        FssInjured = 2,

        /// <summary>
        /// Платеж в ФСС за нетрудоспособность
        /// </summary>
        FssDisablity = 3,

        /// <summary>
        /// Платеж в ФФОМС
        /// </summary>
        Ffoms = 4,

        /// <summary>
        /// Платеж в ТФОМС
        /// </summary>
        Tfoms = 5,

        /// <summary>
        /// Платеж в ФСС на обязательное социальное страхование
        /// </summary>
        FssSocialInsurance = 6,
        
        /// <summary>
        /// ОПС, ОМС, ОСС по ВНИм
        /// </summary>
        Sfr = 7,
    }
}