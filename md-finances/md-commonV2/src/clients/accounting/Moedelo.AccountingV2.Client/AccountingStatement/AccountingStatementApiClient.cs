using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountingStatement;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AccountingStatement
{
    [InjectAsSingleton]
    public class AccountingStatementApiClient : BaseApiClient, IAccountingStatementApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountingStatementApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingsRepository) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, 
                auditTracer, 
                auditScopeManager)
        {
            apiEndPoint = settingsRepository.Get("AccountingApi");
        }

        public async Task<long> CreateAccountingStatementAsync(int firmId, int userId, AccountingStatementDto data)
        {
            var result = await PostAsync<AccountingStatementDto, CreateAccountingStatementResponse>(
                $"/SystemStatementApi/CreateAccountingStatement?firmId={firmId}&userId={userId}",
                data).ConfigureAwait(false);
            return result.Data;
        }

        public Task DeleteAccountingStatement(int firmId, int userId, long statementId)
        {
            return PostAsync(
                $"/SystemStatementApi/DeleteAccountingStatement?firmId={firmId}&userId={userId}",
                new AccountingStatementBaseDto {AccountingStatementId = statementId});
        }

        public Task<List<AccountingStatementSimpleDto>> GetByType(int firmId, int userId, AccountingStatementType type, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<AccountingStatementSimpleDto>>(
                "/SystemStatementApi/GetByType",
                new { firmId, userId, type, startDate, endDate });
        }

        public Task<List<AccountingStatementSimpleDto>> GetByBaseIds(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            return PostAsync<IReadOnlyCollection<long>, List<AccountingStatementSimpleDto>>($"/SystemStatementApi/GetByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public async Task<List<AccountingStatementDto>> GetBySubcontoAsync(int firmId, int userId, long subcontoId, DateTime startDate, DateTime endDate, bool isFromReadOnlyDb, HttpQuerySetting setting = null)
        {
            var response = await GetAsync<GetBySubcontoResponse>("/SystemStatementApi/GetBySubconto", new { firmId, userId, subcontoId, startDate, endDate, isFromReadOnlyDb }, setting: setting).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<List<long>> DeleteBySourceDocumentBaseIdAndTypeAsync(int firmId, int userId, long documentBaseId, AccountingStatementType statementType)
        {
            var response = await PostAsync<object, DataResponseWrapper<List<long>>>($"/SystemStatementApi/DeleteBySourceDocumentBaseIdAndType?firmId={firmId}&userId={userId}",
                new { documentBaseId, type = statementType }).ConfigureAwait(false);
            return response.Data;
        }

        public Task<SavedAccountingStatementDto> CreateAccountingStatementForDissmissFixedAssetAsync(int firmId, int userId, LinkedAccountingStatementDto data)
        {
            return PostAsync<LinkedAccountingStatementDto, SavedAccountingStatementDto>
            ($"/SystemStatementApi/CreateAccountingStatementForDissmissFixedAsset?firmId={firmId}&userId={userId}", data);
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync($"/AccountingStatementApi/Delete?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }

        public Task CreateAccountingStatementForUsnAsync(int firmId, int userId, ReportStatementDataDto dto)
        {
            return PostAsync($"/SystemStatementApi/CreateAccountingStatementForUsn?firmId={firmId}&userId={userId}", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task CreateAccountingStatementForEnvdAsync(int firmId, int userId, IList<ReportStatementDataDto> dto)
        {
            return PostAsync($"/SystemStatementApi/CreateAccountingStatementForEnvd?firmId={firmId}&userId={userId}", dto);
        }

        public Task CreateAccountingStatementForContractAsync(int firmId, int userId, int contractId, DateTime? byDate = null)
        {
            return PostAsync(
                $"/SystemStatementApi/CreateAccountingStatementForContract?firmId={firmId}&userId={userId}", new { contractId, byDate });
        }

        public async Task<List<long>> GetProductIdsWithManualAccDocumentLaterThanDateAsync(int firmId, int userId, AccDocumentsProductIdsListRequestDto request)
        {
            return (await PostAsync<AccDocumentsProductIdsListRequestDto, DataResponseWrapper<List<long>>>($"/SystemStatementApi/GetProductIdsWithManualAccDocumentLaterThanDateAsync?firmId={firmId}&userId={userId}", request))
                .Data;
        }

        public async Task<List<long>> GetProductIdsWithAnyAccDocumentBeforeDateAsync(int firmId, int userId, AccDocumentsProductIdsListRequestDto request)
        {
            return (await PostAsync<AccDocumentsProductIdsListRequestDto, DataResponseWrapper<List<long>>>($"/SystemStatementApi/GetProductIdsWithAnyAccDocumentBeforeDateAsync?firmId={firmId}&userId={userId}", request))
                .Data;
        }
    }
}
