using System.Collections.Generic;
using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances
{
    public class GetBalanceByFirmIdsRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public IReadOnlyCollection<SyntheticAccountCode> SyntheticAccountCodes { get; set; }
    }
}
