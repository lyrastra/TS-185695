using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.PromoCode.Technical;

namespace Moedelo.HomeV2.Client.PromoCode
{
    public interface ITechnicalPromoCodeApiClient
    {
        Task<GeneratedTechnicalPromoCodeDto> GenerateBizAccFixedSumPromoCodeAsync(int firmId, decimal fixedSum);

        Task<TechnicalPromoCodeDto> GetTechnicalPromoCodeByNameAsync(int firmId, string promoCode);
    }
}
