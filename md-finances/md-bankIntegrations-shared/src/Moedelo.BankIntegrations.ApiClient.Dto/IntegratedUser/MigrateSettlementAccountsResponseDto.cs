using System.Collections.Generic;
using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class MigrateSettlementAccountsResponseDto : BaseResponseDto
    {
        public IReadOnlyCollection<MigratedDataInfoResponseDto> SettlementAccountIdsByFirmIds { get; set; }
    }
}
