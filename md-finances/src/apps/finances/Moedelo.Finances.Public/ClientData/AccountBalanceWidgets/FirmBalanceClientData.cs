using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Finances.Public.ClientData.AccountBalanceWidgets
{
    public class FirmBalanceClientData
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<SettlementAccountBalanceClientData> SettlementAccountsBalances { get; set; }

        public int SettlementAccountsCount => SettlementAccountsBalances?.Count ?? 0;

        public decimal Balance => SettlementAccountsBalances?.Sum(sab => sab.Balance) ?? 0;
    }
}