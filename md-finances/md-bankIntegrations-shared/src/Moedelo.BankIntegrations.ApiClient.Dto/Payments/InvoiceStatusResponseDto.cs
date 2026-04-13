using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Enums.Invoices;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class InvoiceStatusResponseDto : BaseResponseDto
    {
        public string DescriptionStatus { get; set; }
        
        public InvoiceStatus Status { get; set; }
        
        /// <summary>
        /// Внешний идентификатор платежного документа назначенный банком
        /// </summary>
        public string ExternalDocumentId { get; set; }
    }
}