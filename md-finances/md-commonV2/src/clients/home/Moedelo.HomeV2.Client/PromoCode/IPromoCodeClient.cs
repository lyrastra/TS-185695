using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.PromoCode;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.PromoCode
{
    public interface IPromoCodeClient : IDI
    {
        Task<List<PromoCodeDto>> GetActualPromoCodesAsync(string namePattern);

        Task<List<PromoCodeDto>> GetActualPromoCodesAsync(string namePattern, int limit);

        Task<List<PromoCodeInfoDto>> GetActualPromoCodeListAsync(string searchNameFilter);

        Task<List<PromoCodeInfoDto>> GetExpiredPromoCodeList(string searchNameFilter);

        Task<PromoCodeDto> GetPromoCodeByIdAsync(int id);

        Task<PromoCodeDto> GetPromoCodeByNameAsync(string name);

        Task<int> SaveFriendInvitePromoCodeWithUserInviteDataAsync(PromoCodeFriendInviteDto dto);

        Task SavePromoCodeList(List<PromoCodeDto> promoCodeDtoList);

        Task<PromoCodeDto> GetPromoCodeByCriteriaAsync(PromoCodeCriteriaDto criteria);

        Task ActivatePromoCodeAsync(ActivatePromoCodeRequestDto requestDto);

        Task<CostWithPromoCodeDto> GetCostWithPromoCodeAsync(GetCostWithPromoCodeRequestDto requestDto);

        Task<List<PromoCodeDto>> GetActivePromoCodeListByMrkActionAsync(int mrkActionId);

        Task<List<PromoCodeDto>> GetConsumedPromoCodeListByMrkActionAsync(int mrkActionId);

        Task DeletePromoCodeAsync(int promoCodeId);

        Task<bool> CheckPromoCodeNameIsFree(string promoCodeName);

        Task<List<MrkActionsDto>> GetMrkActions();

        Task<MrkActionsDto> GetMrkActionById(int actionId);

        Task DeleteMrkActionsAsync(int id);

        Task<bool> CheckMrkActionPrefixIsFree(string name);

        Task<int> SaveMrkActions(MrkActionsDto mrkActionsDto);

        Task<int> SavePromoCodeAsync(PromoCodeDto promoCodeDto);

        Task<PromoCodeDto> GetLoyalityProgramPromoCodeByNameAsync(string promoCode, int firmId);

        Task<string> GetLoyaltyProgramPersonalPromoCodeAsync(int firmId);

        Task<PromoCodeDto> GetFriendInvitePromoCodeByFirmIdAsync(string promoCode, int firmId);
        Task<PromoCodeDto> GetOnBoardingPromoCodeAsync(string promoCode, int firmId);
    }
}