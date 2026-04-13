using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Stocks;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton]
    public class RequisitionWaybillClient : BaseApiClient, IRequisitionWaybillClient
    {
        private readonly SettingValue apiEndPoint;

        public RequisitionWaybillClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : 
            base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<RequisitionWaybillWithStockOperationDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var response = await GetAsync<RequisitionWaybillWithStockOperationDto>(
                "/RequisitionWaybill/GetByDocumentBaseId",
                new { firmId, userId, documentBaseId }).ConfigureAwait(false);

            return response;
        }

        public async Task<RequisitionWaybillWithStockOperationDto> GetByIdAsync(int firmId, int userId, long id)
        {
            var response = await GetAsync<RequisitionWaybillWithStockOperationDto>(
                "/RequisitionWaybill/GetById",
                new { firmId, userId, id }).ConfigureAwait(false);

            return response;
        }

        public async Task<List<RequisitionWaybillWithStockOperationDto>> GetByIdsAsync(int firmId, int userId,
            IReadOnlyList<long> ids)
        {
            var response = await PostAsync<IReadOnlyList<long>, List<RequisitionWaybillWithStockOperationDto>>(
                $"/RequisitionWaybill/GetByIds?firmId={firmId}&userId={userId}", ids).ConfigureAwait(false);

            return response;
        }

        public async Task<List<StockDocumentTaxInfoDto>> CheckForTaxableAsync(int firmId, int userId, IReadOnlyCollection<RequisitionWaybillDocumentDto> requsitionWaybillDtos)
        {
            if (requsitionWaybillDtos?.Any() != true)
            {
                return new List<StockDocumentTaxInfoDto>();
            }

            var response = await PostAsync<IReadOnlyCollection<RequisitionWaybillDocumentDto>, ListResponse<StockDocumentTaxInfoDto>>(
                $"/RequisitionWaybill/CheckForTaxable?firmId={firmId}&userId={userId}",
                requsitionWaybillDtos).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<List<RequisitionWaybillDocumentDto>> GetForPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var response = await PostAsync<PeriodDto, ListResponse<RequisitionWaybillDocumentDto>>(
                $"/RequisitionWaybill/GetForPeriod?firmId={firmId}&userId={userId}",
                new PeriodDto
                {
                    StartDate = startDate,
                    EndDate = endDate
                }).ConfigureAwait(false);

            return response.Items;
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return PostAsync($"/RequisitionWaybill/Delete?firmId={firmId}&userId={userId}&documentId={baseId}");
        }        
    }
}
