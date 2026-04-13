using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Estate.ApiClient.Abstractions.legacy.FixedAsset;
using Moedelo.Estate.ApiClient.Abstractions.legacy.FixedAsset.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Estate.ApiClient.legacy.FixedAssset
{
    [InjectAsSingleton(typeof(IFixedAssetApiClient))]
    internal sealed class FixedAssetApiClient : BaseLegacyApiClient, IFixedAssetApiClient
    {
        public FixedAssetApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FixedAssetApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<FixedAssetDto> GetByBaseId(FirmId firmId, UserId userId, long baseId)
        {
            var uri = $"/FixedAssetInvestmentApi/Get?firmId={firmId}&userId={userId}&baseId={baseId}";
            return GetAsync<FixedAssetDto>(uri);
        }
    }
}