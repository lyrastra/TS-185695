using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;
using Moedelo.Payroll.Enums.Worker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IChargePaymentsApiClient))]
    internal sealed class ChargePaymentsApiClient : IChargePaymentsApiClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue payrollPrivateApiEndpoint;

        public ChargePaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecuter = httpRequestExecuter;
            payrollPrivateApiEndpoint = settingRepository.Get("PayrollPrivateApi");
        }

        public async Task<WorkerChargePaymentsListDto> GetByDocumentBaseIdAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var endpoint = payrollPrivateApiEndpoint.Value;
            var uri = $"{endpoint}/ChargePayments/Get/{documentBaseId}?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.GetAsync(uri).ConfigureAwait(false);
            return response.FromJsonString<WorkerChargePaymentsListDto>();
        }

        public async Task<WorkerChargePaymentsListDto[]> GetByDocumentBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds)
        {
            var endpoint = payrollPrivateApiEndpoint.Value;
            var uri = $"{endpoint}/ChargePayments/GetByDocumentBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, documentBaseIds.ToUtf8JsonContent());
            return response?.FromJsonString<WorkerChargePaymentsListDto[]>() ?? Array.Empty<WorkerChargePaymentsListDto>();
        }

        public Task BindPaymentEventChargePaymentsAsync(FirmId firmId, UserId userId, BindPaymentRequestDto chargePayments)
        {
            var endpoint = payrollPrivateApiEndpoint.Value;
            var uri = $"{endpoint}/ChargePayments/BindPaymentEventChargePayments?firmId={firmId}&userId={userId}";
            return httpRequestExecuter.PostAsync(uri, chargePayments.ToUtf8JsonContent());
        }

        public async Task<WorkerChargePaymentsListDto> GetUnboundForWorkerAsync(FirmId firmId, UserId userId, long? documentBaseId, int workerId, WorkerPaymentType workerPaymentType)
        {
            var endpoint = payrollPrivateApiEndpoint.Value;
            var uri = $"{endpoint}/ChargePayments/Get/Unbound?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}&workerId={workerId}&workerPaymentType={(int)workerPaymentType}";
            var response = await httpRequestExecuter.GetAsync(uri).ConfigureAwait(false);
            return response.FromJsonString<WorkerChargePaymentsListDto>();
        }
    }
}