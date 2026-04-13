using System;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class LastBankBalanceResponseDto
    {
        public int SettlementAccountId { get; set; }

        public DateTime BalanceDate { get; set; }

        public decimal Balance { get; set; }
    }
}
