using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.Firm;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.Clients.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IFirmClient))]
    internal sealed class FirmClient : BaseLegacyApiClient, IFirmClient
    {
        public FirmClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  auditHeadersGetter,
                  settingRepository.Get("FirmApiEndpoint"),
                  logger)
        { }
        
        public async Task<int> GetLegalUserId(int firmId)
        {
            var response = await GetAsync<int>($"/GetLegalUserId?firmId={firmId}").ConfigureAwait(false);
            return response;
        }

        public Task<bool> GetIsInternal(int firmId)
        {
            return GetAsync<bool>($"/V2/GetIsInternal?firmId={firmId}");
        }

        public Task<int> CreateAsync(FirmDto firm)
        {
            return PostAsync<FirmDto, int>("/V2/Create", firm);
        }
        
        public Task<List<FirmLeadMarkDto>> GetFirmLeadMarksAsync(IReadOnlyCollection<int> firmIds)
        {
            return firmIds?.Any() != true
                ? Task.FromResult(new List<FirmLeadMarkDto>())
                : PostAsync<IReadOnlyCollection<int>, List<FirmLeadMarkDto>>("/V2/GetFirmLeadMarks", firmIds);
        }
        
        public async Task<List<FirmDto>> GetFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            if (firmIds == null || firmIds.Count == 0)
            {
                return new List<FirmDto>();
            } 
            firmIds = firmIds.ToHashSet();
            return await PostAsync<IReadOnlyCollection<int>, List<FirmDto>>("/V2/GetFirms", firmIds);
        }

        public Task<bool> IsDeletedAsync(int firmId)
        {
            return GetAsync<bool>($"/V2/IsDeleted?firmId={firmId}");
        }

        public Task<int?> GetTargetFirmIdAsync(int firmId)
        {
            return GetAsync<int?>("/V2/GetTargetFirmId", new { firmId });
        }

        public Task<IReadOnlyCollection<FirmIdLegalUserIdDto>> GetByLegalUsersAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<FirmIdLegalUserIdDto>>(
                "/V2/GetByLegalUsers", userIds, cancellationToken: cancellationToken);
        }

        public Task<List<FirmDto>> GetByInnsAsync(IReadOnlyCollection<string> inns, CancellationToken cancellationToken)
        {
            if (inns == null || inns.Count == 0)
            {
                return Task.FromResult(new List<FirmDto>());
            }
            inns = inns.ToHashSet();
            return PostAsync<IReadOnlyCollection<string>, List<FirmDto>>("/V2/GetByInns", inns, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<int>> FilterOutInternalAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<int>> (
                "/V2/Get/FilterOutInternal", firmIds, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default)
        {
            firmIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, bool>>(
                "/V2/GetFlagsIsDeletedForFirmIds", firmIds, cancellationToken: cancellationToken);
        }
    }
}
