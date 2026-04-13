using System.Collections.Generic;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class PartnerActiveIntegratedUsersRequestDto
    {
        public IntegrationPartners? Partner { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }

        public PaginationInfoDto Pagination { get; set; }

        public class PaginationInfoDto
        {
            public int Offset { get; set; }

            public int Count { get; set; }
        }
    }
}
