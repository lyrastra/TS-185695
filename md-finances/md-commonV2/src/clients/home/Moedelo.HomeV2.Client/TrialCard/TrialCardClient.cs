using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Billing;
using Moedelo.HomeV2.Dto.ClientSource;
using Moedelo.HomeV2.Dto.TrialCard;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.TrialCard
{
    [InjectAsSingleton]
    public class TrialCardClient: BaseApiClient, ITrialCardClient
    {
        private readonly SettingValue apiEndPoint;

        public TrialCardClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/TrialCard/";
        }

        public Task<TrialCardDto> GetTrialCardAsync(GetTrialCardRequestDto requestDto)
        {
            return GetAsync<TrialCardDto>("V2/GetTrialCard", requestDto);
        }

        public async Task<ClientSourceDto> GetClientSource(GetTrialCardRequestDto request)
        {
            var response = await PostAsync<GetTrialCardRequestDto, DataRequestWrapper<ClientSourceDto>>("GetClientSource", request).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<ClientSourceDto>> GetAllClientSource()
        {
            var list = await GetAsync<ListWrapper<ClientSourceDto>>("GetAllClientSource").ConfigureAwait(false);
            return list.Items;
        }

        public async Task<List<TrialCardDto>> GenerateTrialCardsWithDifferentNames(int clientSourceId, int quantity, TrialCardTypes cardType)
        {
            var response = await GetAsync<ListWrapper<TrialCardDto>>("GenerateTrialCardsWithDifferentNames", new { clientSourceId, quantity, cardType }).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<string> GenerateTrialCardsWithOneName(int clientSourceId, int quantity, TrialCardTypes cardType)
        {
            var response = await GetAsync<DataRequestWrapper<string>>("GenerateTrialCardsWithOneName", new { clientSourceId, quantity, cardType }).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<TrialCardsCountWithClientSourceDto>> GetTrialCardsCountWithClientSource()
        {
            var response = await GetAsync<ListWrapper<TrialCardsCountWithClientSourceDto>>("GetTrialCardsCountWithClientSource").ConfigureAwait(false);
            return response.Items;
        }

        public async Task<List<TrialCardsStatisticsDto>> GetTrialCardsStatistics(DateTime startDate, DateTime endDate)
        {
            var response = await GetAsync<ListWrapper<TrialCardsStatisticsDto>>("GetTrialCardsStatistics", new { startDate, endDate }).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<int> SaveClientSource(ClientSourceDto request)
        {
            var response = await PostAsync<ClientSourceDto, DataRequestWrapper<int>>("SaveClientSource", request).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<bool> AreExistTrialCardsByClientSourceId(int clientSourceId)
        {
            var response = await GetAsync<DataRequestWrapper<bool>>("AreExistTrialCardsByClientSourceId", new { clientSourceId }).ConfigureAwait(false);
            return response.Data;
        }

        public Task DeleteClientSource(int id)
        {
            return GetAsync("DeleteClientSource", new {id});
        }
    }
}