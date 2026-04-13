using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.MovementHash
{
    public static class HashRequestsMovementsMapper
    {
        public static HashRequestsMovementsDto Map(this HashRequestsMovementsModel model)
        {
            return new HashRequestsMovementsDto()
            {
                PartnerId = model.PartnerId,
                PartnerName = model.PartnerName,
                Qty = model.Qty
            };
        }

        public static HashRequestsMovementsModel Map(this HashRequestsMovementsDto model)
        {
            return new HashRequestsMovementsModel()
            {
                PartnerId = model.PartnerId,
                PartnerName = model.PartnerName,
                Qty = model.Qty
            };
        }

        public static List<HashRequestsMovementsDto> Map(this List<HashRequestsMovementsModel> list)
        {
            return list
                    .Select(model => model.Map())
                    .ToList();
        }

        public static List<HashRequestsMovementsModel> Map(this List<HashRequestsMovementsDto> list)
        {
            return list
                    .Select(model => model.Map())
                    .ToList();
        }

    }
}
