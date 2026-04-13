using System;

namespace Moedelo.Money.BankBalanceHistory.Domain
{
    public class LastBankBalance
    {
        public LastBankBalance(int settlementAccountId, decimal balance, DateTime balanceDate, DateTime modifyDate)
        {
            SettlementAccountId = settlementAccountId;
            Balance = balance;
            BalanceDate = balanceDate;
            ModifyDate = modifyDate;
        }

        public int SettlementAccountId { get; }

        public decimal Balance { get; }

        public DateTime BalanceDate { get; }

        public DateTime ModifyDate { get; }
    }
}
