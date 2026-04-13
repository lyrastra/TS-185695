namespace Moedelo.TaxPostings.Enums
{
    public enum OsnoTransferType
    {
        Unknown = 0,

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
