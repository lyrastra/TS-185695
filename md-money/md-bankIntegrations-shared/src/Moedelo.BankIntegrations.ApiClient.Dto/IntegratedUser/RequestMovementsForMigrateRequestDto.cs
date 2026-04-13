using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class RequestMovementsForMigrateRequestDto
    {
        public IReadOnlyCollection<MigrateSettlementAccountsRequestItemDto> Items { get; set; }
    }
}
