using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legasy.Dto;
using Moedelo.Requisites.Enums.FirmRequisites;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFirmImageApiClient))]
    internal sealed class FirmImageApiClient : BaseLegacyApiClient, IFirmImageApiClient
    {
        public FirmImageApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmImageApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<Dictionary<FirmImageType, byte[]>> GetImagesAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<FirmImageType> types)
        {
            if (types?.Any() != true)
            {
                return Task.FromResult(new Dictionary<FirmImageType, byte[]>());
            }

            return PostAsync<IEnumerable<FirmImageType>, Dictionary<FirmImageType, byte[]>>(
                $"/FirmImages/GetByTypes?firmId={firmId}&userId={userId}",
                types.ToDistinctReadOnlyCollection());
        }

        public Task<Dictionary<FirmImageType, FirmImageWithOffsetDto>> GetImagesWithOffsetsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<FirmImageType> types)
        {
            if (types?.Any() != true)
            {
                return Task.FromResult(new Dictionary<FirmImageType, FirmImageWithOffsetDto>());
            }

            return PostAsync<IEnumerable<FirmImageType>, Dictionary<FirmImageType, FirmImageWithOffsetDto>>(
                $"/FirmImages/GetByTypesWithOffsets?firmId={firmId}&userId={userId}",
                types);
        }
    }
}