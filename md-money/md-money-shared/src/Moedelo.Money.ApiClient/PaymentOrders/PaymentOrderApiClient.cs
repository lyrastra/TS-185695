using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Snapshot;
using System.Threading;

namespace Moedelo.Money.ApiClient.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderApiClient))]
    internal sealed class PaymentOrderApiClient : BaseApiClient, IPaymentOrderApiClient
    {
        public PaymentOrderApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentOrderApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }

        public async Task<OperationType> GetTypeAsync(long documentBaseId)
        {
            var url = $"/api/v1/PaymentOrders/{documentBaseId}/Type";
            var result = await GetAsync<ApiDataDto<OperationType>>(url);
            return result.data;
        }

        public async Task<OperationTypeDto[]> GetTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Array.Empty<OperationTypeDto>();
            }

            var url = "/private/api/v1/PaymentOrders/GetTypeByBaseIds";
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<OperationTypeDto[]>>(url, documentBaseIds);
            return result.data;
        }

        public Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var url = "/private/api/v1/PaymentOrders/Provide";
            var setting = new HttpQuerySetting(TimeSpan.FromMinutes(1));
            return PostAsync(url, documentBaseIds, setting: setting);
        }

        public Task ApproveImportedAsync(ApproveImportedOperationsRequestDto dto)
        {
            if (dto.SourceType != null &&
                dto.SourceType != MoneySourceType.SettlementAccount)
            {
                return Task.CompletedTask;
            }

            var url = "/private/api/v1/PaymentOrders/Imported/Approve";
            return PostAsync(url, dto);
        }

        public async Task<IReadOnlyCollection<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year = null)
        {

            var url = "/private/api/v1/PaymentOrders/BaseIdsByOperationType";
            var result = await GetAsync<ApiDataDto<IReadOnlyCollection<long>>>(url, 
                new { operationType = (int)operationType, year },
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(90)));
            return result.data;
        }

        public async Task<IReadOnlyCollection<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year = null, int? cut = null)
        {
            var url = "/private/api/v1/PaymentOrders/GetOutgoingNumbers";
            var result = await GetAsync<ApiDataDto<IReadOnlyCollection<int>>>(url,
                new {
                    settlementAccountId,
                    year,
                    cut
                }, 
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(120)));
            return result.data;
        }

        public async Task<PaymentOrderSnapshotDto> GetPaymentOrderSnapshotAsync(
            long documentBaseId,
            CancellationToken ct)
        {
            var url = "/private/api/v1/PaymentOrders/GetPaymentOrderSnapshot";
            var result = await GetAsync<ApiDataDto<PaymentOrderSnapshotDto>>(url,
                new
                {
                    documentBaseId
                },
                cancellationToken: ct);
            return result.data;
        }
    }
}