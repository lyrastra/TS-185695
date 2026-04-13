using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.AccountingStatements.ApiClient.Abstractions;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.AccountingStatements.ApiClient
{
    [InjectAsSingleton(typeof(IAccountingStatementApiClient))]
    public class AccountingStatementApiClient : BaseApiClient, IAccountingStatementApiClient
    {
        public AccountingStatementApiClient(IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AccountingStatementApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AccountingApi"),
                logger
                )
        {
        }
        
        public Task<List<AccountingStatementSimpleDto>> GetByBaseIds(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            return PostAsync<IReadOnlyCollection<long>, List<AccountingStatementSimpleDto>>($"/SystemStatementApi/GetByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public async Task<long> CreateAccountingStatementAsync(int firmId, int userId, AccountingStatementDto data)
        {
            var result = await PostAsync<AccountingStatementDto, CreateAccountingStatementResponse>(
                $"/SystemStatementApi/CreateAccountingStatement?firmId={firmId}&userId={userId}",
                data);
            return result.Data;
        }
        public async Task<AccountingStatementSaveDto> CreateAccountingStatementWithBaseIdAsync(int firmId, int userId, AccountingStatementDto data)
        {
            var result = await PostAsync<AccountingStatementDto, AccountingStatementSaveDto>(
                $"/SystemStatementApi/CreateAccountingStatementWithBaseId?firmId={firmId}&userId={userId}",
                data);
            return result;
        }

        public Task DeleteAccountingStatement(int firmId, int userId, long statementId)
        {
            return PostAsync(
                $"/SystemStatementApi/DeleteAccountingStatement?firmId={firmId}&userId={userId}",
                new AccountingStatementBaseDto {AccountingStatementId = statementId});
        }
        
        public Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync($"/AccountingStatementApi/Delete?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }
    }
}