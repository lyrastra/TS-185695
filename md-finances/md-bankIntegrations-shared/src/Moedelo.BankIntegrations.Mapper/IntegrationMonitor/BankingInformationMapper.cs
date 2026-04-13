using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class BankingInformationMapper
    {
        public static BankingInformationDto Map(this BankingInformationModel model)
        {
            return new BankingInformationDto()
            {
                PartnerId = model.PartnerId,
                PartnerName = model.PartnerName,
                Comment = model.Comment,
                IsError = model.IsError
            };
        }

        public static BankingInformationModel Map(this BankingInformationDto dto)
        {
            return new BankingInformationModel()
            {
                PartnerId = dto.PartnerId,
                PartnerName = dto.PartnerName,
                Comment = dto.Comment,
                IsError = dto.IsError
            };
        }

        public static List<BankingInformationDto> Map(this List<BankingInformationModel> models)
        {
            return models.Select(model => model.Map()).ToList();
        }

        public static List<BankingInformationModel> Map(this List<BankingInformationDto> dtoList)
        {
            return dtoList.Select(dto => dto.Map()).ToList();
        }

    }
}
