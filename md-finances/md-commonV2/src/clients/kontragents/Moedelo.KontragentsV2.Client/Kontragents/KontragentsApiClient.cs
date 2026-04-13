using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.KontragentsV2.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.KontragentsV2.Client.DtoWrappers;
using Moedelo.KontragentsV2.Dto.Enums;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentsApiClient : BaseApiClient, IKontragentsClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentsApiClient(
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
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<KontragentDto> GetByIdAsync(int firmId, int userId, int id)
        {
            var response = await GetAsync<KontragentResponse<KontragentDto>>(
                "/GetById",
                new {firmId, userId, id}).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<KontragentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids,
            CancellationToken cancellationToken)
        {
            if (ids?.Any() != true)
            {
                return new List<KontragentDto>();
            }

            var url = $"/Kontragents/GetByIds?firmId={firmId}&userId={userId}"; 
            
            var response = await PostAsync<IReadOnlyCollection<int>, KontragentResponse<List<KontragentDto>>>(
                url, ids, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response.Data;
        }

        public Task<List<BasicKontragentInfoDto>> GetBasicInfoByIdsAsync(IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<BasicKontragentInfoDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<BasicKontragentInfoDto>>($"/Kontragents/GetBasicInfoByIds",
                ids);
        }

        public async Task<List<KontragentDto>> GetByNamesAsync(int firmId, int userId,
            IReadOnlyCollection<string> names, CancellationToken cancellationToken)
        {
            if (!names.Any())
            {
                return new List<KontragentDto>();
            }

            const string uri = "/GetByNames";
            var queryParams = new { firmId, userId, names = string.Join(",", names) };
            
            var response = await GetAsync<KontragentResponse<List<KontragentDto>>>(uri, queryParams)
                .ConfigureAwait(false);

            return response.Data;
        }

        public Task<List<KontragentDto>> GetAsync(int firmId, int userId)
        {
            return GetAsync<List<KontragentDto>>("/V2/Get", new {firmId, userId});
        }

        public Task<int> SaveAsync(int firmId, int userId, KontragentDto kontragent)
        {
            return PostAsync<KontragentDto, int>($"/V2/Save?firmId={firmId}&userId={userId}", kontragent);
        }

        public Task<KontragentsPageDto> GetPageAsync(int firmId, int userId, KontragentsPageRequestDto request)
        {
            return PostAsync<KontragentsPageRequestDto, KontragentsPageDto>($"/V2/GetPage?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<KontragentShortDto>> KontragentsAutocompleteAsync(int firmId, int userId, string query,
            int count = 100, CancellationToken cancellationToken = default)
        {
            return GetAsync<List<KontragentShortDto>>("/V2/KontragentsAutocomplete", new {firmId, userId, query, count}, cancellationToken: cancellationToken);
        }

        public Task<KontragentDto> GetBySubcontoIdAsync(int firmId, int userId, long subcontoId)
        {
            return GetAsync<KontragentDto>("/V2", new { firmId, userId, subcontoId });
        }

        public Task<List<KontragentDto>> GetByInnAsync(int firmId, int userId, string inn)
        {
            return GetAsync<List<KontragentDto>>("/V2", new { firmId, userId, inn });
        }

        public Task<List<KontragentDto>> GetNonresidentByTaxpayerNumber(int firmId, int userId, string taxpayerNumber)
        {
            return GetAsync<List<KontragentDto>>("/V2/GetNonresidentByTaxpayerNumber", new { firmId, userId, taxpayerNumber });
        }

        public async Task<List<KontragentDto>> GetByInnsAsync(int firmId, int userId, IReadOnlyCollection<string> inns)
        {
            var response =
                await GetAsync<KontragentResponse<List<KontragentDto>>>("/GetByInns",
                    new {firmId, userId, inns = string.Join(",", inns)}).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<int> CreatePopulationKontragentIfNotExistAsync(int firmId, int userId)
        {
            var dataDto = await PostAsync<DataDto<int>>($"/Kontragents/CreatePopulationKontragentIfNotExist?firmId={firmId}&userId={userId}").ConfigureAwait(false);
            return dataDto.Data;
        }

        public async Task<List<DebtorDto>> GetDebtorsAsync(int firmId, int userId, int count)
        {
            var result = await GetAsync<DataDto<List<DebtorDto>>>(
                    "/Kontragents/GetDebtors", 
                    new {firmId, userId, count}).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<KontragentWithSettlementAccountDto>> GetWithSettlementAccountAsync(int firmId, int userId, string query, int count)
        {
            var result = await GetAsync<ListDto<KontragentWithSettlementAccountDto>>(
                "/GetWithSettlementAccount",
                new {firmId, userId, query, count}).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<List<KontragentEmailDto>> GetEmailKontragentAutocompleteAsync(int firmId, int userId, string query, int count, List<int> withoutIds)
        {
            var result = await GetAsync<ListDto<KontragentEmailDto>>(
                "/GetEmailKontragentAutocomplete",
                new {firmId, userId, query, count, withoutIds = string.Join(",", withoutIds)})
                .ConfigureAwait(false);
            return result.Items;
        }

        public async Task<List<KontragentWithAccountingPaymentOrderDto>> GetWithAccountingPaymentOrderAsync(int firmId, int userId, string query, DateTime paymentOrderDate, int count)
        {
            var result = await GetAsync<ListDto<KontragentWithAccountingPaymentOrderDto>>(
                    "/GetWithAccountingPaymentOrder",
                    new {firmId, userId, query, count, paymentOrderDate})
                .ConfigureAwait(false);
            return result.Items;
        }

        public async Task<List<KontragentDto>> GetByNameExceptIdsOrderByNameAsync(int firmId, int userId, string query, int count, bool onlyFounders = false,
            List<int> exceptIds = null)
        {
            var result = await PostAsync<GetByNameExceptIdsOrderByNameParams, DataDto<List<KontragentDto>>>(
                $"/GetByNameExceptIdsOrderByName?firmId={firmId}&userId={userId}",
                new GetByNameExceptIdsOrderByNameParams
                {
                    Query = query,
                    ExceptIds = exceptIds ?? new List<int>(),
                    Count = count,
                    OnlyFounders = onlyFounders
                }).ConfigureAwait(false);
            return result.Data;
        }

        public Task ResaveWithSubcontoAsync(int firmId, int userId)
        {
            return PostAsync($"/Kontragents/ResaveWithSubconto?firmId={firmId}&userId={userId}");
        }

        public async Task<KontragentDto> GetByPurseIdAsync(int firmId, int userId, int purseId)
        {
            var result = await GetAsync<DataDto<KontragentDto>>("/Kontragents/GetByPurseId", new { firmId, userId, purseId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<bool> IsKontragentsExistAsync(int firmId, int userId)
        {
            var result =
                await PostAsync<DataDto<bool>>($"/Kontragents/IsKontragentsExist?firmId={firmId}&userId={userId}")
                    .ConfigureAwait(false);

            return result.Data;
        }

        public Task<List<KontragentDto>> GetPopulationAndPurseAgentsAsync(int firmId, int userId)
        {
            return GetAsync<List<KontragentDto>>("/V2/GetPopulationAndPurseAgents", new { firmId, userId });
        }

        public Task<KontragentsRequestStatusCode> DeleteAsync(int firmId, int userId, int id)
        {
            return PostAsync<KontragentsRequestStatusCode>($"/V2/Delete?firmId={firmId}&userId={userId}&id={id}");
        }

        public Task<List<KontragentDto>> GetForImportAsync(int firmId, int userId)
        {
            return GetAsync<List<KontragentDto>>("/V2/GetForImport", new { firmId, userId });
        }

        public Task<List<KontragentDto>> AutocompleteAsync(int firmId, int userId, string query, string description, string inn, int count)
        {
            return GetAsync<List<KontragentDto>>("/V2/Autocomplete", new { firmId, userId, query, description, inn, count });
        }
    }
}