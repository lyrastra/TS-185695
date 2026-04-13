namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class DocumentsCountByKontragentDto
    {
        public int KontragentId { get; set; }

        /// <summary>
        ///     Количество счетов, связанных с контрагентом
        /// </summary>
        public int BillCount { get; set; }

        /// <summary>
        ///     Количество счетов-договоров, связанных с контрагентом
        /// </summary>
        public int BillContractCount { get; set; }

        /// <summary>
        ///     Количество исходящих актов, связанных с контрагентом
        /// </summary>
        public int OutgoingStatementCount { get; set; }

        /// <summary>
        ///     Количество исходящих накладных, связанных с контрагентом
        /// </summary>
        public int OutgoingWaybillCount { get; set; }

        /// <summary>
        ///     Количество исходящих счетов-фактур, связанных с контрагентом
        /// </summary>
        public int OutgoingInvoiceCount { get; set; }

        /// <summary>
        ///     Количество входящих актов, связанных с контрагентом
        /// </summary>
        public int IncomingStatementCount { get; set; }

        /// <summary>
        ///     Количество входящих накладных, связанных с контрагентом
        /// </summary>
        public int IncomingWaybillCount { get; set; }

        /// <summary>
        ///     Количество входящих счетов-фактур, связанных с контрагентом
        /// </summary>
        public int IncomingInvoiceCount { get; set; }

        /// <summary>
        ///     Количество актов-сверок, связанных с контрагентом
        /// </summary>
        public int ReconciliationStatementCount { get; set; }
    }
}
