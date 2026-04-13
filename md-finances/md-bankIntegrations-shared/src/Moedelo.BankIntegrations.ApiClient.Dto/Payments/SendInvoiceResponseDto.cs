using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Payments;
using Moedelo.BankIntegrations.Enums.Invoices;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class SendInvoiceResponseDto : BaseResponseDto
    {
        public ExternalDocumentDto ExternalDocument { get; set; }
        
        public InvoiceResponseStatusCode InvoiceResponseStatusCode { get; set; }

        public string PayerSettlementNumber { get; set; }
    }
}