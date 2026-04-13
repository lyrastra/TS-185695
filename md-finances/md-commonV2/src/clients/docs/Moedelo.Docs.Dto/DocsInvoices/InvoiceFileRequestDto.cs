namespace Moedelo.Docs.Dto.DocsInvoices
{
    public class InvoiceFileRequestDto
    {
        /// <summary>
        /// DocumentBaseId сч-фактуры
        /// </summary>
        public long BaseId { get; set; }
        
        /// <summary>
        /// Добавить печать и подпись
        /// </summary>
        public bool? UseStampAndSign { get; set; }
        
        /// <summary>
        /// Добавить экземпляр исполнителя(по умолчанию false)
        /// </summary> 
        public bool? IncludeContractorCopy { get; set; }
    }
}