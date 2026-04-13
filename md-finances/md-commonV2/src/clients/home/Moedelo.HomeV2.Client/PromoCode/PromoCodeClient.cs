using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.PromoCode;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.PromoCode
{
    [InjectAsSingleton]
    public class PromoCodeClient : BaseApiClient, IPromoCodeClient
    {
        private readonly SettingValue apiEndPoint;

        public PromoCodeClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint= settingRepository.Get("HomePrivateApiEndpoint");
        }

        public Task<List<PromoCodeDto>> GetActualPromoCodesAsync(string namePattern)
        {
            return GetAsync<List<PromoCodeDto>>("/V2/GetActualPromoCodes", new {namePattern});
        }

        public Task<List<PromoCodeDto>> GetActualPromoCodesAsync(string namePattern, int limit)
        {
            return GetAsync<List<PromoCodeDto>>("/V2/GetActualPromoCodes", new {namePattern, limit});
        }

        public Task<PromoCodeDto> GetPromoCodeByIdAsync(int id)
        {
            return GetAsync<PromoCodeDto>("/V2/GetPromoCodeById", new {id});
        }

        public Task<int> SaveFriendInvitePromoCodeWithUserInviteDataAsync(PromoCodeFriendInviteDto dto)
        {
            return PostAsync<PromoCodeFriendInviteDto, int>("/V2/SaveFriendInvitePromoCodeWithUserInviteData", dto);
        }

        public async Task<List<PromoCodeInfoDto>> GetActualPromoCodeListAsync(string searchNameFilter)
        {
            var response = await GetAsync<ListWrapper<PromoCodeInfoDto>>("/GetActualPromoCodeList", new { searchNameFilter }).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<List<PromoCodeInfoDto>> GetExpiredPromoCodeList(string searchNameFilter)
        {
            var response = await GetAsync<ListWrapper<PromoCodeInfoDto>>("/GetExpiredPromoCodeList", new { searchNameFilter }).ConfigureAwait(false);
            return response.Items;
        }

        public Task<PromoCodeDto> GetPromoCodeByNameAsync(string name)
        {
            return GetAsync<PromoCodeDto>("/GetPromoCodeByName", new { name });
        }

        public Task SavePromoCodeList(List<PromoCodeDto> promoCodeDtoList)
        {
            return PostAsync("/SavePromoCodeList", promoCodeDtoList);
        }

        public Task<CostWithPromoCodeDto> GetCostWithPromoCodeAsync(GetCostWithPromoCodeRequestDto requestDto)
        {
            return PostAsync<GetCostWithPromoCodeRequestDto, CostWithPromoCodeDto>("/GetCostWithPromoCode", requestDto);
        }

        public Task<PromoCodeDto> GetPromoCodeByCriteriaAsync(PromoCodeCriteriaDto criteria)
        {
            return PostAsync<PromoCodeCriteriaDto, PromoCodeDto>("/GetPromoCodeByCriteria", criteria);
        }

        public Task ActivatePromoCodeAsync(ActivatePromoCodeRequestDto requestDto)
        {
            return PostAsync("/ActivatePromoCode", requestDto);
        }


        public Task<List<PromoCodeDto>> GetActivePromoCodeListByMrkActionAsync(int mrkActionId)
        {
            return GetAsync<List<PromoCodeDto>>("/GetActivePromoCodeListByMrkAction", new { mrkActionId });
        }

        public Task<List<PromoCodeDto>> GetConsumedPromoCodeListByMrkActionAsync(int mrkActionId)
        {
            return GetAsync<List<PromoCodeDto>>("/GetConsumedPromoCodeListByMrkAction", new { mrkActionId });
        }

        public async Task<bool> CheckPromoCodeNameIsFree(string promoCodeName)
        {
            var result = await PostAsync<DataRequestWrapper<string>, DataRequestWrapper<bool>>("/CheckPromoCodeNameIsFree", new DataRequestWrapper<string> { Data = promoCodeName }).ConfigureAwait(false);
            return result.Data;
        }

        public Task DeletePromoCodeAsync(int promoCodeId)
        {
            return PostAsync("/DeletePromoCode", new IdInfoWrapper<int> { Id = promoCodeId });
        }

        public async Task<List<MrkActionsDto>> GetMrkActions()
        {
            var result = await GetAsync<ListWrapper<MrkActionsDto>>("/GetMrkActions").ConfigureAwait(false);
            return result.Items;
        }

        public Task<MrkActionsDto> GetMrkActionById(int actionId)
        {
            return GetAsync<MrkActionsDto>("/GetMrkActionById", new { actionId });
        }

        public async Task<bool> CheckMrkActionPrefixIsFree(string name)
        {
            var result = await PostAsync<DataRequestWrapper<string>, DataRequestWrapper<bool>>("/CheckMrkActionPrefixIsFree", new DataRequestWrapper<string> { Data = name }).ConfigureAwait(false);
            return result.Data;
        }

        public Task DeleteMrkActionsAsync(int id)
        {
            return PostAsync("/DeleteMrkActions", new IdInfoWrapper<int> { Id = id });
        }

        public async Task<int> SaveMrkActions(MrkActionsDto mrkActionsDto)
        {
            var result = await PostAsync<MrkActionsDto, IdInfoWrapper<int>>("/SaveMrkActions", mrkActionsDto).ConfigureAwait(false);
            return result.Id;
        }

        public async Task<int> SavePromoCodeAsync(PromoCodeDto promoCodeDto)
        {
            var result = await PostAsync<PromoCodeDto, IdInfoWrapper<int>>("/SavePromoCode", promoCodeDto).ConfigureAwait(false);
            return result.Id;
        }

        public async Task<PromoCodeDto> GetLoyalityProgramPromoCodeByNameAsync(string promoCode, int firmId)
        {
            var result = await GetAsync<PromoCodeDto>("/V2/GetLoyalityProgramPromoCodeByName", queryParams: new { promoCode, firmId }).ConfigureAwait(false);

            return result;
        }

        public async Task<string> GetLoyaltyProgramPersonalPromoCodeAsync(int firmId)
        {
            var result = await GetAsync<string>("/V2/GetLoyaltyProgramPersonalPromoCode", queryParams: new {firmId})
                .ConfigureAwait(false);

            return result;
        }

        public async Task<PromoCodeDto> GetFriendInvitePromoCodeByFirmIdAsync(string promoCode, int firmId)
        {
            var result = await GetAsync<PromoCodeDto>("/V2/GetFriendInvitePromoCodeByFirmId", queryParams: new { promoCode, firmId })
                .ConfigureAwait(false);

            return result;
        }

        public async Task<PromoCodeDto> GetOnBoardingPromoCodeAsync(string promoCode, int firmId)
        {
            var result = await GetAsync<PromoCodeDto>("/V2/GetOnBoardingPromoCode", queryParams: new { promoCode, firmId })
                .ConfigureAwait(false);

            return result;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/PromoCode";
        }
    }
}