using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Finances.Public.ClientData.AccountBalanceWidgets
{
    public class AccountBalanceWidgetClientData
    {
        public List<FirmBalanceClientData> FirmsBalances { get; set; }

        public int FirmsCount => FirmsBalances?.Count ?? 0;

        public int SettlementAccountsCount => FirmsBalances?.Sum(fb => fb.SettlementAccountsCount) ?? 0;

        public decimal Balance => FirmsBalances?.Sum(fb => fb.Balance) ?? 0;
    }
}