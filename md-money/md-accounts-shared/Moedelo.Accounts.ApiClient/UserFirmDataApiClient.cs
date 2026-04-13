using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.UserFirmData;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.Clients.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IUserFirmDataApiClient))]
    internal sealed class UserFirmDataApiClient : BaseLegacyApiClient, IUserFirmDataApiClient
    {
        public UserFirmDataApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UserFirmDataApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("UserFirmDataApiEndpoint"),
                logger)
        {
        }


        public Task<List<FirmNameInfoDto>> GetFirmNameListByUserIdAsync(int userId)
        {
            return GetAsync<List<FirmNameInfoDto>>("/V2/GetFirmNameListByUserId", new { userId });
        }

        public Task CreateAsync(int firmId, int userId)
        {
            return PostAsync($"/V2/Create?firmId={firmId}&userId={userId}");
        }

        public Task<List<UserFirmDataDto>> GetByFirmIdAsync(int firmId)
        {
            return GetAsync<List<UserFirmDataDto>>("/V2/GetByFirmId", new { firmId });
        }

        public Task<IReadOnlyCollection<UserFirmDataDto>> GetByFirmIdsAsync(
           IReadOnlyCollection<int> firmIds,
           CancellationToken cancellationToken = default)
        {
            firmIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<UserFirmDataDto>>(
                "/V2/GetByFirmIds", firmIds, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<UserFirmDataDto>> GetByUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default)
        {
            userIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<UserFirmDataDto>>(
                "/V2/GetByUserIds", userIds, cancellationToken: cancellationToken);
        }
        
        public Task<List<UserFirmDataDto>> GetByUserAndFirmIdsAsync(
            int userId, 
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default)
        {
            const string url = "/V2/GetByUserIdAndFirmIds"; 

            var request = new UserFirmDataByUserAndFirmsSearchCriteriaDto
            {
                UserId = userId,
                FirmIds = firmIds
            };

            return PostAsync<UserFirmDataByUserAndFirmsSearchCriteriaDto, List<UserFirmDataDto>>(
                url, request, cancellationToken: cancellationToken);
        }
    }
}