using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Enums.Invoices;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class InvoiceAccessResponseDto : BaseResponseDto
    {
        /// <summary> Дата и время начала срока действия правила </summary>
        public DateTime CreationDateTime { get; set; }

        /// <summary> Дата и время истечения срока действия правила </summary>
        public DateTime ExpirationDateTime { get; set; }
        
        /// <summary> Статус доступности для отправки сквозного платежа в банк </summary>
        public InvoiceAccessStatus InvoiceAccessStatus { get; set; }
        
        /// <summary> Статус ответа по сквозному платежу  </summary>
        public InvoiceResponseStatusCode InvoiceResponseStatusCode { get; set; }
    }
}