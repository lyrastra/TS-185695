using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccountingV2.Dto.Balances
{
    public class GetBalanceByFirmIdsRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public IReadOnlyCollection<SyntheticAccountCode> SyntheticAccountCodes { get; set; }
    }
}
