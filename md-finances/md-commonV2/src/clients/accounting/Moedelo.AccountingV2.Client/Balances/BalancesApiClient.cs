using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Balances;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Balances
{
    [InjectAsSingleton]
    public class BalancesApiClient : BaseApiClient, IBalancesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BalancesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<DateTime?> GetDateAsync(int firmId, int userId, bool useReadOnlyDb = false, CancellationToken cancellationToken = default)
        {
            return GetAsync<DateTime?>("/Balances/Date", new { firmId, userId, useReadOnlyDb }, cancellationToken: cancellationToken);
        }

        public Task<List<BalanceDto>> GetByAccountCodesAsync(int firmId, int userId, IReadOnlyCollection<SyntheticAccountCode> codes)
        {
            return PostAsync<IReadOnlyCollection<SyntheticAccountCode>, List<BalanceDto>>($"/Balances/ByAccountCodes?firmId={firmId}&userId={userId}", codes);
        }

        public Task<IReadOnlyDictionary<int, AccountBalanceDto[]>> GetByFirmIdsAndAccountCodesAsync(GetBalanceByFirmIdsRequestDto request)
        {
            return PostAsync<GetBalanceByFirmIdsRequestDto, IReadOnlyDictionary<int, AccountBalanceDto[]>>(
                $"/Balances/ByAccountCodes", request);
        }

        public Task<List<BalanceDto>> GetBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds)
        {
            return PostAsync<IReadOnlyCollection<long>, List<BalanceDto>>($"/Balances/BySubcontoIds?firmId={firmId}&userId={userId}", subcontoIds);
        }

        public Task ReplaceKontragentsInResultsAsync(int firmId, int userId, KontragentsReplaceInResultsDto request)
        {
            return PostAsync($"/MoneyBalanceMasterApi/ReplaceKontragentsInResults?firmId={firmId}&userId={userId}", request);
        }
        
        public async Task<string> GetPeriodNameAsync(int firmId, int userId, long id)
        {
            var response = await GetAsync<DataResponseWrapper<string>>("/BalanceBizApi/GetPeriodName", new { firmId, userId, id }).ConfigureAwait(false);
            return response.Data;
        }
    }
}