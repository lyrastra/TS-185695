using System;

namespace Moedelo.Money.Dto.BankBalanceHistory
{
    public class BankBalanceRequestDto
    {
        public int SettlementAccountId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}