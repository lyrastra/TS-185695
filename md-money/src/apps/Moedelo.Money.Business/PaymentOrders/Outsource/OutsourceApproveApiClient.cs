using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outsource;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(OutsourceApproveApiClient))]
    internal sealed class OutsourceApproveApiClient : BaseApiClient
    {
        private const string path = "/private/api/v1/Outsource/Approve";

        public OutsourceApproveApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<OutsourceApproveApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("PaymentOrderApiEndpoint"),
                  logger)
        {
        }

        public async Task<bool> GetIsApprovedAsync(long documentBaseId, DateTime initialDate)
        {
            try
            {
                var response = await GetAsync<ApiDataResponseWrapper<bool>>($"{path}/{documentBaseId}?initialDate={initialDate:yyyy-MM-dd}");
                return response.Data;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
        }

        public async Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(
            OutsourceAllOperationsApprovedRequestDto request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var response = await PostAsync<OutsourceAllOperationsApprovedRequestDto, ApiDataResponseWrapper<IReadOnlyDictionary<int, bool>>>(
                $"{path}/GetIsAllOperationsApproved", request, cancellationToken: ct);

            return response.Data;
        }

        public async Task<OutsourceApproveResponseDto[]> GetIsApprovedAsync(
            IReadOnlyCollection<long> documentBaseIds, DateTime initialDate)
        {
            var response = await PostAsync<OutsourceBatchApproveRequestDto, ApiDataResponseWrapper<OutsourceApproveResponseDto[]>>(
                $"{path}/GetByIds",
                new OutsourceBatchApproveRequestDto
                {
                    DocumentBaseIds = documentBaseIds,
                    InitialDate = initialDate
                });
            return response.Data;
        }

        public async Task SetIsApprovedAsync(long documentBaseId, bool isApproved, DateTime initialDate)
        {
            try
            {
                await PutAsync(
                    $"{path}/{documentBaseId}",
                    new OutsourceApproveRequestDto
                    {
                        IsApproved = isApproved,
                        InitialDate = initialDate
                    });
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
        }

        public Task SetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds, DateTime initialDate)
        {
            return PostAsync(
                $"{path}",
                new OutsourceBatchApproveRequestDto
                {
                    DocumentBaseIds = documentBaseIds,
                    InitialDate = initialDate
                });
        }
    }
}
