using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Registry;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.Domain.Registry;
using Moedelo.Money.Registry.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Domain.SelfCostPayments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Registry
{
    [InjectAsSingleton(typeof(IRegistryApiClient))]
    internal sealed class RegistryApiClient : BaseApiClient, IRegistryApiClient
    {
        private const string prefix = "/private/api/v1";
        private static readonly HttpQuerySetting RegistryDefaultSetting = new HttpQuerySetting(TimeSpan.FromSeconds(60));

        public RegistryApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<RegistryApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("RegistryApiEndpoint"),
                  logger)
        {
        }

        public async Task<RegistryResponse> GetAsync(RegistryQuery query)
        {
            var url = $"{prefix}/Registry";
            var request = new RegistryQueryDto
            {
                EndDate = query.EndDate,
                StartDate = query.StartDate,
                Limit = query.Limit,
                Offset = query.Offset,
                AfterDate = query.AfterDate,
                TaxationSystemType = query.TaxationSystemType,
                OperationSource = query.OperationSource,
                OperationTypes = query.OperationTypes,
                ContractorId = query.ContractorId,
                ContractorType = query.ContractorType,
                Query = query.Query,
                DocumentBaseIds = query.DocumentBaseIds
            };
            var response = await PostAsync<RegistryQueryDto, ApiPageResponseWrapper<RegistryOperationDto>>(url, request, setting: RegistryDefaultSetting);
            return MapToResponse(response);
        }

        public async Task<IReadOnlyCollection<RegistryOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate)
        {
            var url = $"{prefix}/Registry/GetOutgoingPaymentsForTaxWidgets";
            var request = new 
            {
                EndDate = startDate,
                StartDate = endDate
            };
            var response = await GetAsync<ApiDataResponseWrapper<IReadOnlyCollection<RegistryOperation>>>(url, request);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostSettlementAccountPaymentsAsync(SelfCostPaymentRequest request)
        {
            var url = $"{prefix}/Registry/GetSelfCostPayments";
            var query = new SelfCostPaymentRequestDto
            {
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Limit = request.Limit,
                Offset = request.Offset,
                Source = OperationSource.SettlementAccount
            };
            var response = await PostAsync<SelfCostPaymentRequestDto, ApiDataResponseWrapper<SelfCostPayment[]>>(url, query, setting: RegistryDefaultSetting);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostCashPaymentsAsync(SelfCostPaymentRequest request)
        {
            var url = $"{prefix}/Registry/GetSelfCostPayments";
            var query = new SelfCostPaymentRequestDto
            {
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Limit = request.Limit,
                Offset = request.Offset,
                Source = OperationSource.Cashbox
            };
            var response = await PostAsync<SelfCostPaymentRequestDto, ApiDataResponseWrapper<SelfCostPaymentResponseDto[]>>(url, query, setting: RegistryDefaultSetting);
            return response.Data.Select(RegistryMapper.MapPayments).ToArray();
        }

        private static RegistryResponse MapToResponse(ApiPageResponseWrapper<RegistryOperationDto> response)
        {
            var operations = response.Data.Select(RegistryMapper.MapOperation).ToArray();
            return new RegistryResponse
            {
                Operations = operations,
                Limit = response.Limit,
                Offset = response.Offset,
                TotalCount = response.TotalCount
            };
        }
    }
}
