using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class MigrateSettlementAccountsRequestDto
    {
        public IReadOnlyCollection<MigrateSettlementAccountsRequestItemDto> Items { get; set; }
        /// <summary> Новый идентификатор банка </summary>
        public int NewBankId { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}
