namespace Moedelo.Common.Enums.Enums.Documents
{
    public enum DocumentType
    {
        Waybill = 1,
        Invoice = 2,
        Statement = 6,
        AccountingAdvanceStatement = 11,
        Bill = 13,
        Project = 14,
        MiddlemanReport = 25,
        /// <summary>
        /// Приказ
        /// </summary>
        PolicyOrder = 31,
        /// <summary>
        /// Учетная политика для налогового учета
        /// </summary>
        PolicyTaxAccounting = 32,
        /// <summary>
        /// Учетная политика для бухгалтерского учета
        /// </summary>
        PolicyForAccounting = 33
    }
}