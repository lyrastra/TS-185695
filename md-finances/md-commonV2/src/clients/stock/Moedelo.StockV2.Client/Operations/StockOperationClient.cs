using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Operations;
using Moedelo.StockV2.Dto.SelfCost;

namespace Moedelo.StockV2.Client.Operations
{
    [InjectAsSingleton]
    public class StockOperationClient : BaseApiClient, IStockOperationClient
    {
        private readonly SettingValue apiEndPoint;

        public StockOperationClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint= settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        
        public async Task<long> CreateAsync(int firmId, int userId, StockOperationCreateDto operation)
        {
            if (operation?.OperationsOverProducts == null)
            {
                throw new ArgumentException("Пустая складская операция", nameof(operation));
            }

            // ограничение вычислено приблизительно (не более 10Мб json)
            const int maxPositionsCount = 60_000;
            if (operation.OperationsOverProducts.Count > maxPositionsCount)
            {
                // самое простое - увеличить лимиты складе, но нужно узнать об этой необходимости
                throw new ArgumentException(
                    $"Превышен лимит ({maxPositionsCount}) по количеству позиций: {operation.OperationsOverProducts.Count}.",
                    nameof(operation)
                );
            }

            var uri = $"/StockOperation/Create?firmId={firmId}&userId={userId}";
            var result = await PostAsync<StockOperationCreateDto, StockOperationCreateDto.ResultDto>(uri, operation).ConfigureAwait(false);

            return result.Id;
        }

        public Task SaveOrUpdateAsync(int firmId, int userId, StockOperationDto operation)
        {
            if (operation?.OperationsOverProducts == null)
            {
                throw new ArgumentException("Пустая складская операция", nameof(operation));
            }
            
            // склад осилит примерно 8_000 OperationsOverProducts, не более 4Мб (TS-153810)
            // оформить бы исключением, но как его обрабатывать?
            
            return SaveOrUpdateAsync(firmId, userId, new[] {operation});
        }

        public Task SaveOrUpdateAsync(int firmId, int userId, IReadOnlyCollection<StockOperationDto> operations)
        {
            if (operations?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/StockOperation/SaveOperations?firmId={firmId}&userId={userId}", operations);
        }

        public Task DeleteBySourceDocumentIdAsync(int firmId, int userId, long baseId)
        {
            return DeleteBySourceDocumentIdsAsync(firmId, userId, new[] { baseId });
        }

        public Task DeleteBySourceDocumentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/StockOperation/DeleteBySourceDocumentIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task DeleteByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/StockOperation/DeleteStockOperations?firmId={firmId}&userId={userId}", ids);
        }

        public Task<List<StockOperationDto>> GetStockOperationsBySourceDocumentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> sourceDocumentIds)
        {
            if (sourceDocumentIds?.Any() != true)
            {
                return Task.FromResult(new List<StockOperationDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<StockOperationDto>>(
                $"/StockOperation/GetStockOperationsBySourceDocumentIds?firmId={firmId}&userId={userId}",
                sourceDocumentIds);
        }

        public Task<SelfCostResultDto> GetStockOperationsWithSelfCostForPeriodAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate)
        {
            return GetAsync<SelfCostResultDto>(
                "/SelfCost/OperationsForPeriod",
                new { firmId, userId, startDate, endDate, withForgotten = false });
        }

        public Task<SelfCostResultDto> GetStockOperationsWithSelfCostForPeriodWithFogottenAsync(int firmId, int userId,
           DateTime startDate, DateTime endDate, bool checkNegativeOperations = true)
        {
            return GetAsync<SelfCostResultDto>(
                "/SelfCost/OperationsForPeriod",
                new { firmId, userId, startDate, endDate, withForgotten = true, ukdSelfCostBySourceForDifferentYears = true, checkNegativeOperations });
        }

        public Task<SelfCostResultDto> GetStockOperationsWithSelfCostForPeriodAsync(int firmId, int userId, StockOperationRequestDto request)
        {
            return GetAsync<SelfCostResultDto>(
                "/SelfCost/OperationsForPeriod",
                new { firmId, userId, request.StartDate, request.EndDate, request.WithForgotten, request.CheckNegativeOperations, request.UkdSelfCostBySourceForDifferentYears, request.SuppressYearForSelfCost },
                setting: new HttpQuerySetting { Timeout = new TimeSpan(0,1,40)});
        }

        public Task UpdateSelfCostAsync(int firmId, int userId, IReadOnlyCollection<SelfCostUpdateRequestDto> updateRequest)
        {
            if (updateRequest?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/SelfCost/Update?firmId={firmId}&userId={userId}",
                updateRequest);
        }

        public Task<StockOperationDto> CalculateSelfCostForOperationAsync(int firmId, int userId, StockOperationDto operationDto)
        {
            return PostAsync<StockOperationDto, StockOperationDto>(
                $"/SelfCost/CalculateForOperation?firmId={firmId}&userId={userId}",
                operationDto);
        }

        public async Task<List<StockOperationDto>> GetOperationsByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<StockOperationDto>();
            }

            var response = await PostAsync<IReadOnlyCollection<long>, ListResponse<StockOperationDto>>(
                $"/StockOperation/GetStockOperationsByIds?firmId={firmId}&userId={userId}",
                ids).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<List<StockOperationDto>> GetOperationsByTypeAsync(int firmId, int userId, StockOperationTypeEnum type)
        {
            var response = await GetAsync<ListResponse<StockOperationDto>>(
                $"/StockOperation/GetStockOperationByType",
                new { firmId, userId, type}).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<List<StockOperationPlaneDto>> GetPlaneStockOperationsOnDateAsync(int firmId, int userId, DateTime date)
        {
            var response = await GetAsync<ListResponse<StockOperationPlaneDto>>(
                "/StockOperation/GetPlaneStockOperationsOnDate",
                new { firmId, userId, date }, setting: new HttpQuerySetting(new TimeSpan(0, 1, 15))).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<List<StockOperationDto>> GetOperationsByPeriodAsync(int firmId, int userId, DateTime start, DateTime end)
        {
            var response = await GetAsync<ListResponse<StockOperationDto>>(
                "/StockOperation/GetStockOperationsByPeriod",
                new { firmId, userId, start, end }, setting: new HttpQuerySetting(new TimeSpan(0, 5, 0))).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<StockOperationTypeDto> GetStockOperationTypeByCodeAsync(int firmId, int userId, StockOperationTypeEnum stockOperationTypeCode)
        {
            var response = await GetAsync<TemplateDto<StockOperationTypeDto>>(
                "/StockOperation/GetStockOperationType",
                new {firmId, userId, operationType = (int) stockOperationTypeCode}).ConfigureAwait(false);
            return response.Value;
        }

        public Task<StockOperationTypeDto> GetStockOperationTypeByIdAsync(int firmId, int userId, int id)
        {
            return GetAsync<StockOperationTypeDto>(
                "/StockOperation/GetOperationTypeById",
                new { firmId, userId, operationTypeId = id });
        }

        public Task<IReadOnlyCollection<CommissionAgentProductRemainsDto>> GetCommissionAgentProductRemainsAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<IReadOnlyCollection<CommissionAgentProductRemainsDto>>(
                "/SelfCost/GetCommissionAgentProductRemains",
                new { firmId, userId, startDate, endDate });
        }
    }
}