using System;
using System.Collections.Generic;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto
{
    public class NdsRatePeriodFilterByFirmIdsDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }
        public DateTime? OnDate { get; set; }
    }
}