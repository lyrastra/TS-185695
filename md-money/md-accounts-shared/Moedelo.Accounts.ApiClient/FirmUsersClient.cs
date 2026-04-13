using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto.FirmUsers;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.Clients.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IFirmUsersClient))]
    internal sealed class FirmUsersClient : BaseApiClient, IFirmUsersClient
    {
        public FirmUsersClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AccountClient> logger)
            : base(
                httpRequestExecutor,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("FirmUsersApiEndpoint"),
                logger)
        {
        }


        public async Task<UsersWithAccessToFirmDto> GetUsersHavingAccessToFirmAsync(int firmId, int userId, int companyId, CancellationToken ct)
        {
            var dataWrapper = await PostAsync<DataWrapper<UsersWithAccessToFirmDto>>(
                $"/GetUsersHavingAccessToFirm?firmId={firmId}&userId={userId}&companyId={companyId}", cancellationToken: ct)
                .ConfigureAwait(false);
            return dataWrapper.Data;
        }
    }
}