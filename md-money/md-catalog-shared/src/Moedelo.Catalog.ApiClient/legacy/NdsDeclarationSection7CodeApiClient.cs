using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(INdsDeclarationSection7CodeApiClient))]
    internal sealed class NdsDeclarationSection7CodeApiClient : BaseLegacyApiClient, INdsDeclarationSection7CodeApiClient
    {
        public NdsDeclarationSection7CodeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<List<NdsDeclarationSection7CodeDto>> GetByIds(IReadOnlyCollection<int> ids)
        {
            return ids.NullOrEmpty()
                ? Task.FromResult(new List<NdsDeclarationSection7CodeDto>())
                : PostAsync<IReadOnlyCollection<int>, List<NdsDeclarationSection7CodeDto>>("/NdsDeclarationSection7Code/V2/GetByIds", ids);
        }

        public Task<List<NdsDeclarationSection7CodeDto>> GetByCodes(IReadOnlyCollection<string> codes)
        {
            return codes.NullOrEmpty()
                ? Task.FromResult(new List<NdsDeclarationSection7CodeDto>())
                : PostAsync<IReadOnlyCollection<string>, List<NdsDeclarationSection7CodeDto>>("/NdsDeclarationSection7Code/V2/GetByCodes", codes);
        }
    }
}