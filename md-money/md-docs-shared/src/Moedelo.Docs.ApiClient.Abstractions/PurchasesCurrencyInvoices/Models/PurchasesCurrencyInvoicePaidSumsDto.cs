namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices.Models
{
    public class PurchasesCurrencyInvoicePaidSumsDto
    {
        /// <summary>
        /// BaseId документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Покрытая валютными платежами сумма документа
        /// </summary>
        public decimal CurrencyPaidSum { get; set; }
        
        /// <summary>
        /// Покрытая бюджетными платежами сумма НДС
        /// </summary>
        public decimal NdsPaidSum { get; set; }
    }
}