using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outsource
{
    [InjectAsSingleton(typeof(IOutsourceApiClient))]
    internal class OutsourceApiClient : BaseApiClient, IOutsourceApiClient
    {
        // нужно синхронизировать с настройкой PaymentOrderDefaultSetting в md-money, иначе возможны таймауты (см. TS-177956)
        private static readonly HttpQuerySetting MassActionsSetting = new(TimeSpan.FromSeconds(30));
        
        public OutsourceApiClient(
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

        public async Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationApprovedAsync(
            OutsourceAllOperationsApprovedRequestDto request)
        {
            if (request?.FirmIds?.Any() != true)
            {
                return new Dictionary<int, bool>();
            }

            const string url = "/private/api/v1/PaymentOrders/Outsource/Approve/GetIsAllOperationsApproved";
            var result = await PostAsync<OutsourceAllOperationsApprovedRequestDto, ApiDataDto<IReadOnlyDictionary<int, bool>>>(url, request);
            return result.data;
        }

        public async Task<OutsourceConfirmResultDto> ConfirmAsync<T>(T request) where T : class, IConfirmPaymentOrderDto, new()
        {
            if (request is not { DocumentBaseId: > 0 })
            {
                throw new ArgumentException("Некорректные параметры платежа для подтверждения", nameof(request));
            }

            var url = request
                .GetType()
                .GetCustomAttribute<OperationTypeAttribute>()
                !.OperationType
                .GetConfirmRelativePath();
            
            var response = await PostAsync<T, ApiDataDto<OutsourceConfirmResultDto>>(
                url,
                request,
                setting: MassActionsSetting);

            return response.data;
        }

        public async Task<OutsourceDeleteResultDto> DeleteAsync(long documentBaseId)
        {
            var response = await PostAsync<ApiDataDto<OutsourceDeleteResultDto>>(
                $"/private/api/v1/PaymentOrders/Outsource/Delete/{documentBaseId}",
                setting: MassActionsSetting);

            return response?.data;
        }
    }
}
