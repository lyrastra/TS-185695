using System.ComponentModel.DataAnnotations;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    public class CurrencyInvoiceNdsPaymentsAutocompleteRequestDto
    {
        /// <summary>
        /// Количество записей в ответе 
        /// </summary>
        [RequiredValue]
        [Range(1, 100)]
        public int Count { get; set; }
            
        /// <summary>
        /// Поиск по номеру
        /// </summary>
        [ValidateXss]
        public string Query { get; set; }
            
        /// <summary>
        /// НДС уплачен на таможне (или в налоговой) - по типу КБК
        /// </summary>
        [RequiredValue]
        public bool IsNdsPaidAtCustoms { get; set; }

        /// <summary>
        /// DocumentBaseId инвойса
        /// </summary>
        [IdLongValue]
        public long? CurrencyInvoiceBaseId { get; set; }
    }
}