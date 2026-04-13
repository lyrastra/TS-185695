namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class InvoiceStatusRequestDto
    {
        /// <summary>
        /// Внешний идентификатор документа присвоенный банком
        /// </summary>
        public string ExternalDocumentId { get; set; }
        
        /// <summary>
        /// Внешний идентификатор запроса присвоенный банком
        /// </summary>
        public string ExternalRequestId { get; set; }
        
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }
    }
}