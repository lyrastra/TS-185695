using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PaymentOrder;
using Moedelo.Accounting.Enums.PaymentOrder;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPaymentOrderApiClient))]
    internal sealed class PaymentOrderApiClient : BaseLegacyApiClient, IPaymentOrderApiClient
    {
        public PaymentOrderApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentOrderApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task ProvideAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            var uri = $"/PaymentOrderApi/Provide?firmId={firmId}&userId={userId}";
            return PostAsync(uri, baseIds.ToDistinctReadOnlyCollection());
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            var uri = $"/PaymentOrderApi/Delete?firmId={firmId}&userId={userId}";
            return PostAsync(uri, baseIds.ToDistinctReadOnlyCollection());
        }

        public async Task<decimal> FindNextNumberByYearAsync(FirmId firmId, UserId userId, DateTime dateTime, 
            int? settlementAccountId)
        {
            var result =
                await GetAsync<DataResponseWrapper<decimal>>("/PaymentOrderApi/GetNextPaymentOrderNumberForYear", new
                {
                    firmId,
                    userId,
                    dateTime,
                    settlementAccountId
                });

            return result.Data;
        }

        public async Task<byte[]> GetFileAsync(FirmId firmId, UserId userId, GetFileRequestDto request)
        {
            var result = await PostAsync<GetFileRequestDto, string>($"/PaymentOrderApi/GetFile?firmId={firmId}&userId={userId}", request).ConfigureAwait(false);
            return Convert.FromBase64String(result);
        }
        
        public async Task<DocFileInfoDto> GetFileByBaseIdAsync(FirmId firmId, UserId userId, long id, FileFormat format)
        {
            var result =
                await GetAsync<DocFileInfoDto>("/PaymentOrderApi/GetFileByBaseId", new
                {
                    id,
                    format,
                    firmId,
                    userId
                });

            return result;
        }
    }
}