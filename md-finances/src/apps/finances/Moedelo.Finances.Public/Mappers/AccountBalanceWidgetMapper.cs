using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Domain.Models.AccountBalances;
using Moedelo.Finances.Public.ClientData.AccountBalanceWidgets;

namespace Moedelo.Finances.Public.Mappers
{
    public static class AccountBalanceWidgetMapper
    {
        public static AccountBalanceWidgetClientData Map(IReadOnlyCollection<FirmSettlementAccountBalance> balanceList)
        {
            var clientData = new AccountBalanceWidgetClientData
            {
                FirmsBalances = new List<FirmBalanceClientData>(),
            };

            foreach (var balance in balanceList)
            {
                var firmBalance =
                    clientData.FirmsBalances.FirstOrDefault(fb => fb.Id == balance.FirmId);

                if (firmBalance == null)
                {
                    firmBalance = new FirmBalanceClientData
                    {
                        Id = balance.FirmId,
                        Name = balance.FirmName,
                        SettlementAccountsBalances = new List<SettlementAccountBalanceClientData>(),
                    };
                    
                    clientData.FirmsBalances.Add(firmBalance);
                }

                firmBalance.SettlementAccountsBalances.Add(new SettlementAccountBalanceClientData
                {
                    Id = balance.SettlementAccountId,
                    Name = balance.SettlementAccountName,
                    Number = balance.SettlementAccountNumber,
                    BankName = balance.SettlementAccountBankName,
                    Balance = balance.Balance,
                });
            }

            return clientData;
        }
    }
}