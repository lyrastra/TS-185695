using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto
{
    public class NdsRatePeriodListDto
    {
        public NdsRatePeriodListItemDto[] Rates { get; set; } = Array.Empty<NdsRatePeriodListItemDto>();

        public DateTime? LastClosedPeriodEndDate { get; set; }
    }
}
