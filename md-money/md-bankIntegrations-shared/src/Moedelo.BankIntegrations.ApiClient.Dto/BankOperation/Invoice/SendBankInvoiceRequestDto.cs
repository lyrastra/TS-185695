using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.Models.PaymentOrder;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice
{
    public class SendBankInvoiceRequestDto
    {
        /// <summary> Идентификатор документа в МД </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary> Реквизиты п/п в МД </summary>
        public PaymentOrder PaymentOrder { get; set; }
        
        /// <summary> Данные интеграции </summary>
        public IntegrationIdentityDto Identity { get; set; }
        
        /// <summary> URL куда перенаправить пользователя в МД после подтверждения п/п на контуре банка </summary>
        public string BackUrl { get; set; }
        
        /// <summary> Ресурс откуда создан прямой(сквозной платеж) </summary>
        public InvoiceSource Source { get; set; } 
    }
}