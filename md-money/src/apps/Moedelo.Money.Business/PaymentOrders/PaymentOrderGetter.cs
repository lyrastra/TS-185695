using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Business.PaymentOrders.Snapshot;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Snapshot;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderGetter))]
    internal sealed class PaymentOrderGetter : IPaymentOrderGetter
    {
        private readonly IPaymentOrderApiClient apiClient;

        public PaymentOrderGetter(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OperationType> GetOperationTypeAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OperationTypeDto>($"PaymentOrders/{documentBaseId}/OperationType");
            return dto.OperationType;
        }

        public async Task<long> GetOperationIdAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OperationIdDto>($"PaymentOrders/{documentBaseId}/Id");
            return dto.OperationId;
        }

        public async Task<long> GetOperationBaseIdAsync(long id)
        {
            var dto = await apiClient.GetAsync<OperationIdDto>($"PaymentOrders/GetBaseIdById?id={id}");
            return dto.OperationId;
        }

        public async Task<DuplicateDataResponse> GetDuplicateDataAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<DuplicateDataDto>($"PaymentOrders/{documentBaseId}/DuplicateData");
            return new DuplicateDataResponse
            {
                Date = dto.Date,
                OperationType = dto.OperationType,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState
            };
        }

        public async Task<OperationIsFromImportResponse> GetIsFromImportAsync(long documentBaseId)
        {
            var result = await apiClient.GetAsync<OperationIsFromImportDto>($"PaymentOrders/{documentBaseId}/IsFromImport");
            return new OperationIsFromImportResponse
            {
                IsFromImport = result.IsFromImport
            };
        }

        public async Task<OperationTypeResponse[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count == 0)
            {
                return Array.Empty<OperationTypeResponse>();
            }
            var uniqueDocumentBaseIds = documentBaseIds.Distinct().ToArray();
            var response = await apiClient.GetOperationTypeByBaseIdsAsync(uniqueDocumentBaseIds);
            return response.Select(MapOperationType).ToArray();
        }

        public Task<ReportFile> GetReportAsync(long documentBaseId, ReportFormat format)
        {
            return apiClient.DownloadFileAsync($"PaymentOrders/{documentBaseId}/Report?format={(int)format}");
        }

        private static OperationTypeResponse MapOperationType(OperationTypeDto dto)
        {
            return new OperationTypeResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                OperationType = dto.OperationType
            };
        }

        public Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year)
        {
            return apiClient.GetBaseIdsByOperationTypeAsync(operationType, year);
        }

        public Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut)
        {
            return apiClient.GetOutgoingNumbersAsync(settlementAccountId, year, cut);
        }

        public Task<bool> GetIsPaidAsync(long documentBaseId)
        {
            return apiClient.GetIsPaidAsync(documentBaseId);
        }

        public async Task<PaymentOrderSnapshotResponse> GetPaymentOrderSnapshotAsync(long documentBaseId)
        {
            var dto = await apiClient.GetPaymentOrderSnapshotAsync(documentBaseId);
            return dto?.Map();
        }
    }
}