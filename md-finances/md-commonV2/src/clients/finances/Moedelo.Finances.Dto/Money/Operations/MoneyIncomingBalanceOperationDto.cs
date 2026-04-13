using System;

namespace Moedelo.Finances.Dto.Money.Operations
{
    public class MoneyIncomingBalanceOperationDto
    {
        public int SettlementAccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }
}
