namespace Moedelo.Common.Enums.Enums.TaxPostings
{
    public enum OsnoTransferType
    {
        Default = 0,

        /// <summary>
        /// Прямой
        /// </summary>
        Direct = 1,

        /// <summary>
        /// Косвенный
        /// </summary>
        Indirect = 2,

        /// <summary>
        /// Внереализационный
        /// </summary>
        NonOperating = 3,

        /// <summary>
        /// Доход от реализации
        /// </summary>
        OperationIncome = 4,
    }
}