namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    public class CurrencyInvoiceNdsPaymentsAutocompleteRequest
    {
        /// <summary>
        /// Количество записей в ответе 
        /// </summary>
        public int Count { get; set; }
            
        /// <summary>
        /// Поиск по номеру
        /// </summary>
        public string Query { get; set; }
            
        /// <summary>
        /// НДС уплачен на таможне (или в налоговой) - по типу КБК
        /// </summary>
        public bool IsNdsPaidAtCustoms { get; set; }

        /// <summary>
        /// DocumentBaseId инвойса
        /// </summary>
        public long? CurrencyInvoiceBaseId { get; set; }
    }
}