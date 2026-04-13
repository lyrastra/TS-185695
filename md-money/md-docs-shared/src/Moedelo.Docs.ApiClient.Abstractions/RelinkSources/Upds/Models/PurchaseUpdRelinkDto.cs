using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Upds.Models
{
    public class PurchaseUpdRelinkDto : IRelinkSourceDocument
    {
        public long DocumentBaseId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public int KontragentId { get; set; }
        /// <summary>
        /// Учесть в СНО
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }
    }
}