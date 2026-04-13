using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OurPartners.Dto;
using Moedelo.OurPartners.Dto.Promotions;

namespace Moedelo.OurPartners.Client.Promotions
{
    [Obsolete("Функционал не поддерживается. Реализация в процессе удаления")]
    public interface IPromotionApiClient : IDI
    {
        Task<ApiListDto<PromotionDto>> GetByCriteriaAsync(int firmId, int userId, PromotionCriterionDto criterion);
        
        Task<ApiDto<PromotionDto>> GetAsync(int firmId, int userId, int id);

        Task<ApiDto<int>> CreateAsync(int firmId, int userId, PromotionSaveRequestDto saveRequest);
        
        Task UpdateAsync(int firmId, int userId, int id, PromotionSaveRequestDto saveRequest);
        
        Task DeleteAsync(int firmId, int userId, int id);

        Task<ApiDto<string>> SaveImageAsync(int firmId, int userId, HttpFileModel file);
    }
}