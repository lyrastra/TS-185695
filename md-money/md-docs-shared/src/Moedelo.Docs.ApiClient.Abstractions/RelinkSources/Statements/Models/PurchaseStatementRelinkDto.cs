using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Statements.Models
{
    public class PurchaseStatementRelinkDto : IRelinkSourceDocument
    {
        public long DocumentBaseId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        /// <summary>
        /// Указано (оплачено) в остатках
        /// </summary>
        public decimal? BalanceSum { get; set; }
        public int KontragentId { get; set; }
        /// <remarks> Не DocumentBaseId! </remarks>
        public int ContractId { get; set; }
        /// <summary>
        /// Счет контрагента, вкладка "Учет"
        /// </summary>
        public int KontragentAccountCode { get; set; }
        /// <summary>
        /// Учесть в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}