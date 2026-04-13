using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MainMoneyMultiCurrencyTableResponse
    {
        public IReadOnlyCollection<MainMoneyTableOperation> Operations { get; set; }

        public IReadOnlyCollection<MainMoneyMultiCurrencyTableSummary> Summaries { get; set; }

        public MainMoneyTableBankBalance BankBalance { get; set; }
    }
}