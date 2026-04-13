using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Table.Main
{
    public class MainMoneyTableResponseWithOperationsClientData : MoneyTableResponseClientData<MainMoneyOperationClientData>
    {
        public IReadOnlyCollection<MainMoneyTableSummaryResponseClientData> Summaries { get; set; }

        public MainMoneyTableBankBalanceResponseClientData BankBalance { get; set; }
    }
}