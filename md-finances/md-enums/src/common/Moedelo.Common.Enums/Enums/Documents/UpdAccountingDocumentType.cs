namespace Moedelo.Common.Enums.Enums.Documents
{
    public enum UpdAccountingDocumentType
    {
        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 1,
        
        /// <summary>
        /// Бухгалтерская справка
        /// </summary>
        AccountingStatement = 4,

        /// <summary>
        /// Акт
        /// </summary>
        Statement = 6, 

        /// <summary>
        /// Авансовый отчет
        /// </summary>
        AccountingAdvanceStatement = 11,

        /// <summary>
        /// Счет
        /// </summary>
        Bill = 13, 

        /// <summary>
        /// Отчет посредника
        /// </summary>
        MiddlemanReport = 25,

        /// <summary>
        /// Основной договор в учетке
        /// </summary>
        MainContract = 52,

        /// <summary>
        /// Договор
        /// </summary>
        Project = 14

    }
}