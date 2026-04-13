namespace Moedelo.Docs.ApiClient.Abstractions.SalesInvoices
{
    public class SalesInvoiceReportOptionsDto
    {
        /// <summary>
        /// BaseId сч-фактуры
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Добавить печать и подпись (по умолчанию признак будет взят из состояния документа)
        /// </summary>
        public bool? UseStampAndSign { get; set; }
        
        /// <summary>
        /// Добавить экземпляр исполнителя(по умолчанию false)
        /// </summary>
        public bool? IncludeContractorCopy { get; set; }
    }
}