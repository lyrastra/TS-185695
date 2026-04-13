using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.Finances.Public.ClientData.Money
{
    public class PassThruPaymentStateClientData
    {
        public string Message { get; set; }
        
        public InvoiceStatus Status { get; set; }
        
        public IntegrationPartners PartnerId { get; set; }
    }
}