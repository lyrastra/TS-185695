using Moedelo.Finances.Domain.Enums.Integrations;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class SendBankInvoiceRequest
    {
        public long OperationId { get; set; }

        public SendBankInvoiceSourceType SourceType { get; set; }

        public string BackUrl { get; set; }
    }
}