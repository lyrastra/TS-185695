using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.Clients.Dto;
using Moedelo.Accounts.Clients.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IProfOutsourceClient))]
    internal sealed class ProfOutsourceClient : BaseLegacyApiClient, IProfOutsourceClient
    {
        public ProfOutsourceClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AccountClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("ProfOutsourceApiEndpoint"),
                logger)
        {
        }

        public Task<ProfOutsourceContextDto> GetOutsourceContext(int firmId, int userId)
        {
            return GetAsync<ProfOutsourceContextDto>("/OutsourceContext", new { firmId, userId });
        }

        public async Task<AccountDto> GetProfOutsourceForFirmAsync(int firmId, int userId, int slaveFirmId)
        {
            var result = await GetAsync<DataWrapper<AccountDto>>(
                "/GetProfOutsourceForFirm", 
                new { firmId, userId, slaveFirmId });
            return result.Data;
        }

        public async Task<IReadOnlyCollection<FirmOnServiceDto>> GetFirmsOnServiceAsync(int firmId, int userId, IReadOnlyCollection<int> slaveFirmIds)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, DataWrapper<IReadOnlyCollection<FirmOnServiceDto>>>(
                $"/GetFirmsOnService?firmId={firmId}&userId={userId}", 
                slaveFirmIds).ConfigureAwait(false);

            return result.Data;
        }

        public Task<IReadOnlyCollection<FirmOnServiceDto>> GetProfOutsourceFirmsOnServiceByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default)
        {
            firmIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<FirmOnServiceDto>>(
                "/V2/GetProfOutsourceFirmsOnServiceByFirmIds", firmIds, cancellationToken: cancellationToken);
        }
    }
}
