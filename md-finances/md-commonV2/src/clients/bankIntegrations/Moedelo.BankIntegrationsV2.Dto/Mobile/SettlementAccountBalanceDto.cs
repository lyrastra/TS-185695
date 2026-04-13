using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.Mobile
{
    public class SettlementAccountBalanceRequestDto
    {
        public List<int> Ids { get; set; }

        public SettlementAccountBalanceRequestDto(List<int> ids)
        {
            Ids = ids;
        }
    }

    public class SettlementAccountBalanceDto
    {
        public int SettlementAccountId { get; set; }

        public DateTime? LastBankStatementDate { get; set; }

        public decimal Balance { get; set; }
    }

    public class SettlementAccountBalancesResponseDto
    {
        public List<SettlementAccountBalanceDto> Items { get; set; }
        public bool ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
    }
}