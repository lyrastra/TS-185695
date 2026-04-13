using Moedelo.AccountingV2.Dto.AdvanceStatement;
using Moedelo.AccountingV2.Dto.FinancialOperations;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Money
{
    [InjectAsSingleton]
    public class FinancialApiClient : BaseApiClient, IFinancialApiClient
    {
        private readonly SettingValue apiEndPoint;

        public FinancialApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<string> GetIncomingMoneyNextNumber(int firmId, int userId, int year, string settlement)
        {
            var result = await GetAsync<DataResponseWrapper<string>>("/FinancialObjects/GetNextIncomingNumber", new { firmId, userId, year, settlement }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<FinancialOperationDto> GetByBaseDocumentIdAsync(
            int firmId,
            int userId,
            long documentBaseId)
        {

            var result = await GetAsync<DataResponseWrapper<string>>(
                $"/FinancialObjects/GetFinancialOperationByBaseDocumentId?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}").ConfigureAwait(false);

            if (string.IsNullOrEmpty(result.Data))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<FinancialOperationDto>(result.Data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            });
        }

        public async Task<int> GetLastOutgoingNumberAsync(int firmId, int userId)
        {
            var result = await GetAsync<DataResponseWrapper<int>>("/FinancialObjects/GetLastOutgoingNumber", new { firmId, userId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<int> GetLastIncomingNumberAsync(int firmId, int userId)
        {
            var result = await GetAsync<DataResponseWrapper<int>>("/FinancialObjects/GetLastIncomingNumber", new { firmId, userId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<WorkerBalanceDto> GetWorkerBalanceAsync(int firmId, int userId, int workerId, DateTime advanceStatementDate,
            long? advanceStatementId)
        {
            var result = await GetAsync<WorkerBalanceDto>("/FinancialObjects/GetWorkerBalance", new
            {
                firmId,
                userId,
                workerId,
                advanceStatementDate,
                advanceStatementId
            }).ConfigureAwait(false);

            return result;
        }

        public async Task<List<AdvanceStatementFinancialObjectDto>> GetAdvanceDocumentsAsync(int firmId, int userId, long advanceStatementId)
        {
            var result = await GetAsync<ListResponseWrapper<AdvanceStatementFinancialObjectDto>>(
                "/FinancialObjects/GetAdvanceDocuments", new
                {
                    firmId,
                    userId,
                    advanceStatementId
                }).ConfigureAwait(false);

            return result?.Items;
        }


        public async Task<AdvanceStatementFinancialObjectDto> GetAdvanceDocumentByBaseIdAsync(int firmId, int userId, long advanceDocumentBaseId)
        {
            var result = await GetAsync<DataResponseWrapper<AdvanceStatementFinancialObjectDto>>("/FinancialObjects/GetAdvanceDocumentByBaseId", new
            {
                firmId,
                userId,
                advanceDocumentBaseId
            }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<AdvanceStatementFinancialObjectDto>> GetDebtDocumentsAsync(int firmId, int userId, long advanceStatementId)
        {
            var result = await GetAsync<ListResponseWrapper<AdvanceStatementFinancialObjectDto>>("/FinancialObjects/GetDebtDocuments", new
            {
                firmId,
                userId,
                advanceStatementId
            }).ConfigureAwait(false);

            return result?.Items;
        }

        public async Task<List<AdvanceStatementFinancialObjectDto>> GetAllAdvanceDocumentsAsync(int firmId, int userId,
            int workerId, DateTime date, long? advanceDocumentId = null, int? takeCount = null)
        {
            var result = await GetAsync<ListResponseWrapper<AdvanceStatementFinancialObjectDto>>("/FinancialObjects/GetAllAdvanceDocuments", new
            {
                firmId,
                userId,
                workerId,
                date,
                advanceDocumentId,
                takeCount
            })
                .ConfigureAwait(false);

            return result?.Items;
        }

        public async Task<List<AdvanceStatementFinancialObjectDto>> GetAllDebtDocumentsAsync(int firmId, int userId, int workerId, long advanceStatementId, DateTime date,
            PrimaryDocumentsTransferDirection direction)
        {
            var result = await GetAsync<ListResponseWrapper<AdvanceStatementFinancialObjectDto>>("/FinancialObjects/GetAllDebtDocuments", new
            {
                firmId,
                userId,
                workerId,
                advanceStatementId,
                date,
                direction
            }).ConfigureAwait(false);

            return result?.Items;
        }

        public async Task<AdvanceStatementFinancialDto> ProvideAdvanceStatementFinancialObjectsAsync(int firmId, int userId, AdvanceStatementFinancialDto dto)
        {
            var result = await PostAsync<AdvanceStatementFinancialDto, DataResponseWrapper<AdvanceStatementFinancialDto>>($"/FinancialObjects/Provide?firmId={firmId}&userId={userId}", dto)
                .ConfigureAwait(false);

            return result.Data;
        }

        public Task ProvideAccountingPostingsAsync(int firmId, int userId, long advanceStatementId)
        {
            return PostAsync($"/FinancialObjects/ProvideAccountingPostings?firmId={firmId}&userId={userId}&advanceStatementId={advanceStatementId}");
        }
    }
}