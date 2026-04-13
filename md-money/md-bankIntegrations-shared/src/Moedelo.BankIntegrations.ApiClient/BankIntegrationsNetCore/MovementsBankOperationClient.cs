using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore
{
    [InjectAsSingleton(typeof(IMovementsBankOperationClient))]
    public class MovementsBankOperationClient : BaseApiClient, IMovementsBankOperationClient
    {
        private const string RequestMovementsEndpoint = "/private/api/v1/BankOperation";

        private static readonly IReadOnlyCollection<IntegrationPartners> SyncBanks = IntegrationPartnersExtentions.GetBankPartners()
            .Except(IntegrationPartnersExtentions.GetBankQueuePartners())
            .Except(IntegrationPartnersExtentions.GetBankPushPartners())
            .Except(new List<IntegrationPartners>
            {
                IntegrationPartners.SberBank,
                IntegrationPartners.Alfa,
                IntegrationPartners.PointBank,
                IntegrationPartners.TinkoffBankSso
            })
            .ToArray();

        public MovementsBankOperationClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MovementsBankOperationClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationMovementsApi"),
                logger)
        {
        }

        public async Task<RequestMovementListResponseDto> RequestMovementsAsync(
            RequestMovementListRequestDto dto,
            CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<RequestMovementListRequestDto, ApiDataResult<RequestMovementListResponseDto>>(
                $"{RequestMovementsEndpoint}/RequestMovements",
                dto,
                cancellationToken: cancellationToken);

            return response.data;
        }

        public static bool Supports(IntegrationPartners partner)
        {
            return SyncBanks.Contains(partner);
        }
    }
}
