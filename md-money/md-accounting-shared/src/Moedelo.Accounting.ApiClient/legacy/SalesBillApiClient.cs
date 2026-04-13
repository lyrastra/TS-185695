using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ISalesBillApiClient))]
    internal sealed class SalesBillApiClient : BaseLegacyApiClient, ISalesBillApiClient
    {
        public SalesBillApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesBillApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<SalesBillFullCollectionDto> GetForInternalAsync(FirmId firmId, UserId userId, HttpQuerySetting setting = null)
        {
            return GetAsync<SalesBillFullCollectionDto>($"/api/v1/sales/bill/GetForInternal?firmId={firmId}&userId={userId}" +
                                                        $"&pageNo={1}&pageSize={int.MaxValue}", setting: setting);
        }

        public Task<SalesBillDto> SaveAsync(FirmId firmId, UserId userId, SalesBillSaveRequestDto dto)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent bill is not implemented. Waiting for PutAsync");
            }

            return PostAsync<SalesBillSaveRequestDto, SalesBillDto>($"/api/v1/sales/bill?firmId={firmId}&userId={userId}", dto);
        }

        public Task UpdatePaymentStatusAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Bill/UpdatePaymentStatus?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}