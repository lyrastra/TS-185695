using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class IntegratedUserStatsMapper
    {
        public static IntegratedUserStats Map(this IntegratedUserStatsDto dto)
        {
            return new IntegratedUserStats()
            {
                PartnerId = dto.PartnerId,
                Qty = dto.Qty
            };
        }

        public static IntegratedUserStatsDto Map(this IntegratedUserStats model)
        {
            return new IntegratedUserStatsDto()
            {
                PartnerId = model.PartnerId,
                Qty = model.Qty
            };
        }


        public static List<IntegratedUserStats> Map(this List<IntegratedUserStatsDto> list)
        {
            return list.Select(dto => dto.Map()).ToList();
        }

        public static List<IntegratedUserStatsDto> Map(this List<IntegratedUserStats> list)
        {
            return list.Select(model => model.Map()).ToList();
        }


    }
}
