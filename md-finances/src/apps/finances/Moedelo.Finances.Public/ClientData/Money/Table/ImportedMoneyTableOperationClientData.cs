using System.Collections.Generic;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Public.ClientData.Money.Table
{
    public class ImportedMoneyTableOperationClientData : MoneyOperationClientData
    {
        public List<TaxSumRecClientData> Taxes { get; set; } = new List<TaxSumRecClientData>();
        public int LinkedDocumentsCount { get; set; }
        public PrimaryDocsStatus PrimaryDocsStatus { get; set; }
        public decimal UncoveredSum { get; set; }
        public bool CanDownload { get; set; }
        public bool HasUnbindedSalaryChargePayments { get; set; }
    }
}