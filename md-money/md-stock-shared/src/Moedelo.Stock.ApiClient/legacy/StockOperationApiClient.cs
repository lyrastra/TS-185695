using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.StockOperations;
using Moedelo.Stock.ApiClient.legacy.models;
using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IStockOperationApiClient))]
    internal sealed class StockOperationApiClient : BaseLegacyApiClient, IStockOperationApiClient
    {
        public StockOperationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockOperationApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }
        
        public Task SaveOrUpdateAsync(FirmId firmId, UserId userId, StockOperationDto operation)
        {
            if (operation?.OperationsOverProducts == null)
            {
                throw new ArgumentException("Пустая складская операция", nameof(operation));
            }
            
            // склад осилит примерно 8_000 OperationsOverProducts, не более 4Мб (TS-153810)
            // оформить бы исключением, но как его обрабатывать?
            
            return SaveOrUpdateAsync(firmId, userId, new[] { operation });
        }

        public async Task<long> CreateAsync(FirmId firmId, UserId userId, StockOperationCreateDto operation)
        {
            if (operation?.OperationsOverProducts == null)
            {
                throw new ArgumentException("Пустая складская операция", nameof(operation));
            }
            
            if (operation.TypeCode == StockOperationTypeEnum.RequisitionWaybill)
            {
                // такие операции создаются в самом stock (нет потребности в api), но обозначить следует; при необходимости завести отдельный метод
                throw new ArgumentException($"Не поддерживается создание операций типа {StockOperationTypeEnum.RequisitionWaybill.ToString()}", nameof(operation));
            }

            // ограничение вычислено приблизительно (не более 4Мб json)
            const int maxPositionsCount = 32_000;
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

        public Task SaveOrUpdateAsync(FirmId firmId, UserId userId, IReadOnlyCollection<StockOperationDto> operations)
        {
            if (operations == null || operations.Count == 0)
            {
                return Task.CompletedTask;
            }

            var uri = $"/StockOperation/SaveOperations?firmId={firmId}&userId={userId}";

            return PostAsync(uri, operations);
        }

        public Task DeleteBySourceDocumentIdAsync(FirmId firmId, UserId userId, long baseId)
        {
            return DeleteBySourceDocumentIdsAsync(firmId, userId, new[] { baseId });
        }

        public Task DeleteBySourceDocumentIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }
            
            var uri = $"/StockOperation/DeleteBySourceDocumentIds?firmId={firmId}&userId={userId}";
            
            return PostAsync(uri, baseIds.ToDistinctReadOnlyCollection());
        }

        public Task<List<StockOperationDto>> GetStockOperationsBySourceDocumentIdsAsync(int firmId, int userId,
            IReadOnlyCollection<long> sourceDocumentIds)
        {
            if (sourceDocumentIds == null || sourceDocumentIds.Count == 0)
            {
                return (Task<List<StockOperationDto>>) Task.CompletedTask;
            }

            var uri = $"/StockOperation/GetStockOperationsBySourceDocumentIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<StockOperationDto>>(uri, sourceDocumentIds);
        }
        
        public Task<StockOperationDto> CalculateSelfCostForOperationAsync(int firmId, int userId, StockOperationDto operationDto)
        {
            return PostAsync<StockOperationDto, StockOperationDto>(
                $"/SelfCost/CalculateForOperation?firmId={firmId}&userId={userId}",
                operationDto);
        }

        public async Task<List<StockOperationDto>> GetOperationsByTypeAsync(int firmId, int userId, StockOperationTypeEnum type)
        {
            var listReponse = await GetAsync<ListResult<StockOperationDto>>("/StockOperation/GetStockOperationByType",
                new {firmId, userId, type});
            return listReponse.Items;
        }
    }
}