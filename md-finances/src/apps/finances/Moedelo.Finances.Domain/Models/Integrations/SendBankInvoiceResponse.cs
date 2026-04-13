using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.Enums.Invoices;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class SendBankInvoiceResponse
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }
        public InvoiceResponseStatusCode InvoiceStatusCode { get; set; }
        public string Message { get; set; }
        public string InvoiceUrl { get; set; }
    }
}