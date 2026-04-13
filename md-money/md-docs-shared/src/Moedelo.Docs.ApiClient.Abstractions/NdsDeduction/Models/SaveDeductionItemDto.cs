using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models
{
    public class SaveDeductionItemDto
    {
        public InvoiceType InvoiceType { get; set; }

        public long DocumentBaseId { get; set; }
       
        /// <summary>
        /// Принято к вычету
        /// </summary>
        public decimal Sum { get; set; }
    }
}