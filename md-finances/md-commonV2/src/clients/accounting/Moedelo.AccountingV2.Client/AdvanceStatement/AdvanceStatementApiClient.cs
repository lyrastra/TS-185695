using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AdvanceStatement;
using Moedelo.AccountingV2.Dto.Api.ClientData;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AdvanceStatement
{
    [InjectAsSingleton]
    public class AdvanceStatementApiClient : BaseApiClient, IAdvanceStatementApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AdvanceStatementApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task SaveAsync(int firmId, int userId, NewAdvanceStatementClientData clientData)
        {
            return PostAsync($"/AdvanceStatementApi/Save?firmId={firmId}&userId={userId}", clientData);
        }

        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/AdvanceStatementApi/Provide?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return PostAsync($"/AdvanceStatementApi/DeleteByBaseId?firmId={firmId}&userId={userId}&documentBaseId={baseId}");
        }

        public Task<List<AdvanceStatementInfoDto>> GetListAsync(int firmId, int userId,
            AdvanceStatementType? type = null, CancellationToken cancellationToken = default)
        {
            return GetAsync<List<AdvanceStatementInfoDto>>(
                "/AdvanceStatementApi/GetAdvanceStatementListAsync",
                new { firmId, userId, type }, cancellationToken: cancellationToken);
        }

        public async Task<AdvanceStatementInfoDto> GetByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var response = await GetByBaseIdAsync(firmId, userId, documentBaseId).ConfigureAwait(false);

            return new AdvanceStatementInfoDto
            {
                Id = response.Id,
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                WorkerId = response.WorkerId,
                WorkerName = response.WorkerName,
                DocumentBaseId = response.DocumentBaseId
            };
        }

        public async Task<NewAdvanceStatementClientData> GetByIdAsync(int firmId, int userId, long id, HttpQuerySetting setting = null)
        {
            var result =
                await GetAsync<DataResponseWrapper<NewAdvanceStatementClientData>>(
                    "/AdvanceStatementApi/GetAdvanceStatementById", new
                    {
                        firmId,
                        userId,
                        id
                    }, setting: setting).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<NewAdvanceStatementClientData> GetOrCreateFromBusinessTripAsync(int firmId, int userId,
            long? advanceStatementBaseId, long businessTripId)
        {
            var result =
                await PostAsync<AdvanceStatementFromBusinessTripRequestDto,
                    DataResponseWrapper<NewAdvanceStatementClientData>>(
                    $"/AdvanceStatementApi/GetOrCreateFromBusinessTrip?firmId={firmId}&userId={userId}",
                    new AdvanceStatementFromBusinessTripRequestDto
                    {
                        AdvanceStatementBaseId = advanceStatementBaseId,
                        BusinessTripId = businessTripId
                    }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<NewAdvanceStatementClientData> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var result =
                await GetAsync<DataResponseWrapper<NewAdvanceStatementClientData>>(
                    "/AdvanceStatementApi/GetAdvanceStatementByBaseId", new
                    {
                        firmId,
                        userId,
                        documentBaseId
                    }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<DocumentBaseCreateModifyClientData> GetAdvanceStatementCreateModifyClientDataAsync(
            int firmId,
            int userId,
            long documentBaseId,
            AccountingDocumentType documentType)
        {
            var result = await GetAsync<DataResponseWrapper<DocumentBaseCreateModifyClientData>>(
                "/PrimaryDocumentRestApi/GetDocumentCreateModifyClientData",
                new
                {
                    firmId,
                    userId,
                    documentBaseId,
                    documentType
                }).ConfigureAwait(false);
            
            return result.Data;
        }

        public Task<List<AdvanceDocumentClientData>> GetRelatedDocumentsAsync(
            int firmId,
            int userId,
            long documentBaseId)
        {
            return GetAsync<List<AdvanceDocumentClientData>>("/AdvanceStatementApi/GetRelatedDocuments", new { firmId, userId, documentBaseId });
        }

        public Task MergeProductsAsync(int firmId, int userId, ProductMergeRequestDto data)
        {
            return PostAsync($"/AdvanceStatementApi/MergeProducts?firmId={firmId}&userId={userId}", data);
        }
    }
}
