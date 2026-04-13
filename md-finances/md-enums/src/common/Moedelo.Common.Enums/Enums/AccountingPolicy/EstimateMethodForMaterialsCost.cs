namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ оценки стоимости материалов.
    /// </summary>
    public enum EstimateMethodForMaterialsCost
    {
        /// <summary>
        /// По фактическим ценам приобретения (заготовления).
        /// </summary>
        ByActualCostOfAcquisition = 1,

        /// <summary>
        /// По учетным ценам без использования
        /// счета 16 «Отклонения в стоимости материальных ценностей».
        /// </summary>
        ByAccountingPricesWithoutUsingAccountNumber16 = 2,

        /// <summary>
        /// По учетным ценам с использованием
        /// счетов 15 «Заготовление и приобретение материальных ценностей»
        /// и 16 «Отклонение в стоимости материальных ценностей».
        /// </summary>
        ByAccountingPricesWithUsingAccountsNumber15And16 = 3,

        /// <summary>
        /// В стоимость материалов включается цена, комиссионное вознаграждение, 
        /// ввозные пошлины и т.д.
        /// </summary>
        WithCostAndOther = 4
    }
}