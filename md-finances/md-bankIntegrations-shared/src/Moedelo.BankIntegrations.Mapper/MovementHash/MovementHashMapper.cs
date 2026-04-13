using Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.MovementHash
{
    public static class MovementHashMapper
    {
        public static Models.MovementHash.MovementHash Map(this MovementHashDto dto) 
        {
            return new Models.MovementHash.MovementHash()
            {
                FirmId = dto.FirmId,
                NumberDoc = dto.NumberDoc,
                Date = dto.Date,
                PartnerId = dto.PartnerId,
                SettlementNumber = dto.SettlementNumber,
                Sum = dto.Sum,
                IntegrationCallType = dto.IntegrationCallType
            };
        }

        public static MovementHashDto Map(this Models.MovementHash.MovementHash model)
        {
            return new MovementHashDto()
            {
                FirmId = model.FirmId,
                NumberDoc = model.NumberDoc,
                Date = model.Date,
                PartnerId = model.PartnerId,
                SettlementNumber = model.SettlementNumber,
                Sum = model.Sum,
                IntegrationCallType = model.IntegrationCallType
            };
        }

        public static List<MovementHashDto> Map(this List<Models.MovementHash.MovementHash> models)
        {
            return models.Select(x => x.Map()).ToList();
        }

        public static List<Models.MovementHash.MovementHash> Map(this List<MovementHashDto> listDto)
        {
            return listDto?.Select(x => x.Map()).ToList();
        }
    }
}
