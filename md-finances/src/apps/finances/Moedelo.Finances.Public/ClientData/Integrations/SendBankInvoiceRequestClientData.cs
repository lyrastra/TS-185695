using Moedelo.Finances.Domain.Enums.Integrations;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class SendBankInvoiceRequestClientData
    {
        public long OperationId { get; set; }

        public SendBankInvoiceSourceType SourceType { get; set; }

        public string BackUrl { get; set; }
    }
}