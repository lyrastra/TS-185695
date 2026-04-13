using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.RequisitesV2.Client.SettlementAccounts
{
    [InjectAsSingleton]
    public class SettlementAccountClient : BaseApiClient, ISettlementAccountClient
    {
        private readonly SettingValue apiEndPoint;

        public SettlementAccountClient(
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
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<SettlementAccountDto>> GetAsync(int firmId, int userId)
        {
            return GetAsync(firmId, userId, CancellationToken.None);
        }

        public Task<List<SettlementAccountDto>> GetAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            return GetAsync<List<SettlementAccountDto>>("/SettlementAccount", new { firmId, userId }, cancellationToken: cancellationToken);
        }

        public Task<List<SettlementAccountDto>> GetWithDeletedAsync(int firmId, int userId)
        {
            return GetAsync<List<SettlementAccountDto>>("/SettlementAccount/WithDeleted", new { firmId, userId });
        }

        public Task<List<SettlementAccountDto>> GetSettlementAccountListAsync(int firmId, int userId)
        {
            return GetAsync<List<SettlementAccountDto>>("/SettlementAccount/list", new { firmId, userId });
        }

        public Task<SettlementAccountDto> GetPrimaryAsync(int firmId, int userId)
        {
            return GetAsync<SettlementAccountDto>("/SettlementAccount/Primary", new { firmId, userId });
        }

        public Task<SettlementAccountDto> GetByIdAsync(int firmId, int userId, int id)
        {
            return GetAsync<SettlementAccountDto>($"/SettlementAccount/{id}", new { firmId, userId });
        }

        public Task<bool> ExistsForeignCurrencyAccountsAsync(int firmId, int userId)
        {
            return GetAsync<bool>("/SettlementAccount/ExistsForeignCurrencyAccounts", new { firmId, userId });
        }

        public Task<List<SettlementAccountDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids)
        {
            if (ids.Count == 0)
            {
                return Task.FromResult(new List<SettlementAccountDto>());
            }
            return PostAsync<IReadOnlyCollection<int>, List<SettlementAccountDto>>($"/SettlementAccount/ByIds?firmId={firmId}&userId={userId}", ids);
        }

        public Task<List<SettlementAccountDto>> GetByNumbersAsync(int firmId, int userId, IReadOnlyCollection<string> numbers)
        {
            if (numbers.Count == 0)
            {
                return Task.FromResult(new List<SettlementAccountDto>());
            }
            return PostAsync<IReadOnlyCollection<string>, List<SettlementAccountDto>>($"/SettlementAccount/ByNumbers?firmId={firmId}&userId={userId}", numbers);
        }

        public Task<SavedSettlementAccountDto> SaveAsync(int firmId, int userId, SettlementAccountDto settlementAccount)
        {
            return PostAsync<SettlementAccountDto, SavedSettlementAccountDto>($"/SettlementAccount?firmId={firmId}&userId={userId}", settlementAccount);
        }

        public Task<int> ValidateSettlementAccountAsync(int firmId, int userId, SettlementAccountDto settlementAccount)
        {
            return PostAsync<SettlementAccountDto, int>($"/ValidateSettlementAccount?firmId={firmId}&userId={userId}", settlementAccount);
        }

        public Task DearchiveAsync(int firmId, int userId, int id)
        {
            return PostAsync($"/SettlementAccount/Dearchive/{id}?firmId={firmId}&userId={userId}");
        }

        public async Task<List<SettlementAccountDto>> GetBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds)
        {
            if (subcontoIds?.Any() != true)
            {
                return new List<SettlementAccountDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<long>, ListWrapper<SettlementAccountDto>>(
                $"/SettlementAccounts/GetBySubcontoIds?firmId={firmId}&userId={userId}",
                subcontoIds).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<SettlementAccountDto>> GetAutocompleteAsync(
            int firmId,
            int userId,
            string query,
            int count,
            IReadOnlyCollection<int> exceptIds = null,
            SettlementAccountType settlementAccountType = SettlementAccountType.Default)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, ListWrapper<SettlementAccountDto>>(
                $"/SettlementAccounts/GetAutocomplete?firmId={firmId}&userId={userId}&query={query}&count={count}&settlementAccountType={(int)settlementAccountType}",
                exceptIds).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<bool> ExistsAnyAsync(int firmId, int userId)
        {
            var result = await GetAsync<DataWrapper<bool>>(
                "/SettlementAccounts/HasAny", new
                {
                    firmId = firmId,
                    userId = userId
                }).ConfigureAwait(false);

            return result.Data;
        }

        public Task ArchiveAsync(int firmId, int userId, int id)
        {
            return PostAsync($"/SettlementAccount/Archive/{id}?firmId={firmId}&userId={userId}");
        }
    }
}