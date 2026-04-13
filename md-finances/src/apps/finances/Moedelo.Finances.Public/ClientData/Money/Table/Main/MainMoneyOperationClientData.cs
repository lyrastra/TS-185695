using System.Collections.Generic;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Public.ClientData.Money.Table.Main
{
    public class MainMoneyOperationClientData : MoneyOperationClientData
    {
        public long Id { get; set; }
   
        public List<TaxSumRecClientData> Taxes { get; set; } = new List<TaxSumRecClientData>();

        public PassThruPaymentStateClientData PassThruPaymentState { get; set; }
        
        public int LinkedDocumentsCount { get; set; }
        
        public bool HasUnbindedSalaryChargePayments { get; set; }
        
        public bool CanDownload { get; set; }
        
        public PrimaryDocsStatus PrimaryDocsStatus { get; set; }
        
        public decimal UncoveredSum { get; set; }
        
        public bool CanSendToBank { get; set; }
        public IReadOnlyCollection<AppliedImportRuleClientData> OutsourceImportRules { get; set; }
    }
}