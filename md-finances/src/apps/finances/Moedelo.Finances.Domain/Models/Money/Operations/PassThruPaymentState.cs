using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class PassThruPaymentState
    {
        /// <summary>
        /// Статус платежа в банке
        /// </summary>
        public InvoiceStatus Status { get; set; }
        
        /// <summary>
        /// Банковский комментарий к статусу документа 
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Партнер Id
        /// </summary>
        public IntegrationPartners PartnerId { get; set; }
    }
}