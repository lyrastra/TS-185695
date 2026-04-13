using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.EntityMapping;
using Moedelo.AccountV2.Dto.Firm;
using Moedelo.Common.Enums.Enums.EntityTypes;
using Moedelo.Common.Enums.Enums.Leads;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.Firm
{
    [InjectAsSingleton]
    public class FirmApiClient : BaseApiClient, IFirmApiClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmApiEndpoint");
        }

        public Task<FirmDto> GetAsync(int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<FirmDto>($"/V2?firmId={firmId}", cancellationToken: cancellationToken);
        }

        public Task<int> GetLegalUserId(int firmId)
        {
            return GetAsync<int>("/GetLegalUserId", new { firmId });
        }

        public Task<int> GetLegalUserIdAsync(int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<int>("/GetLegalUserId", new { firmId }, cancellationToken: cancellationToken);
        }

        public Task<int> CreateAsync(FirmDto firm)
        {
            return PostAsync<FirmDto, int>("/V2/Create", firm);
        }

        public Task<int> CreateAsync(int userId, int firmId, FirmDto firm)
        {
            return PostAsync<FirmDto, int>($"/V2/Create?firmId={firmId}&userId={userId}", firm);
        }

        public Task<bool> IsSourceFirmAsync(int firmId)
        {
            return GetAsync<bool>("/V2/IsSourceFirm", new { firmId });
        }

        public Task<int?> GetSourceFirmIdAsync(int firmId)
        {
            return GetAsync<int?>("/V2/GetSourceFirmId", new { firmId });
        }

        public Task<int?> GetTargetFirmIdAsync(int firmId)
        {
            return GetAsync<int?>("/V2/GetTargetFirmId", new { firmId });
        }

        public Task<EntityMappingDto> GetBySourceOrTargetAsync(long firmId, CancellationToken cancellationToken)
        {
            return GetAsync<EntityMappingDto>("/V2/GetBySourceOrTarget", new { firmId }, cancellationToken: cancellationToken);
        }

        public Task<EntityMappingDto> GetEntityMappingBySourceIdAsync(int sourceFirmId, EntityType entityType, long sourceEntityId)
        {
            return GetAsync<EntityMappingDto>("/V2/GetEntityMappingBySourceId", new { sourceFirmId, entityType, sourceEntityId });
        }

        public Task<List<FirmDto>> GetFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            firmIds = firmIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<FirmDto>>("/V2/GetFirms", firmIds);
        }

        public Task<List<FirmIdLegalUserIdDto>> GetByLegalUsersAsync(IReadOnlyCollection<int> userIds)
        {
            userIds = userIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<FirmIdLegalUserIdDto>>("/V2/GetByLegalUsers", userIds);
        }

        public Task<int?> GetByLegalUserAsync(int userId, CancellationToken cancellationToken)
        {
            return GetAsync<int?>("/V2/GetByLegalUser", new { userId }, cancellationToken: cancellationToken);
        }

        public Task SetOfficeOperatorIdAsync(IReadOnlyCollection<SetOfficeOperatorIdRequestDto> requests)
        {
            return PostAsync("/V2/SetOfficeOperatorId", requests);
        }

        public Task<List<SetOfficeOperatorIdRequestDto>> GetOfficeOperatorsIdAsync(IReadOnlyCollection<int> firmIds)
        {
            firmIds = firmIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<SetOfficeOperatorIdRequestDto>>("/V2/GetOfficeOperatorsId", firmIds);
        }

        public Task<LeadMarkType> GetFirmLeadMarkTypeAsync(int firmId, CancellationToken cancellationToken)
        {
            var uri = $"/V2/FirmLeadMarkType?firmId={firmId}";

            return GetAsync<LeadMarkType>(uri, cancellationToken: cancellationToken);
        }

        public Task<List<FirmLeadMarkDto>> GetFirmLeadMarksAsync(IReadOnlyCollection<int> firmIds)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult(new List<FirmLeadMarkDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<FirmLeadMarkDto>>("/V2/GetFirmLeadMarks", firmIds.AsSet());
        }

        public Task SetFirmLeadMarksAsync(FirmLeadMarkDto leadMarkDto, int logUserId, int logFirmId)
        {
            var uri = $"/V2/SetFirmLeadMarks?userId={logUserId}&firmId={logFirmId}";
            return PostAsync<FirmLeadMarkDto>(uri, leadMarkDto);
        }

        public Task SetFirmLeadMarksAsync(SetLeadMarksDto leadMarksDto, int logUserId, int logFirmId, HttpQuerySetting setting = null)
        {
            return PostAsync($"/SetFirmLeadMarks?userId={logUserId}&firmId={logFirmId}", leadMarksDto, setting: setting);
        }

        public async Task<List<CompanyWithApiKeyDto>> GetCompaniesWithApiKeyAsync(int userId)
        {
            var dataWrapper = await GetAsync<DataWrapper<List<CompanyWithApiKeyDto>>>("/GetCompaniesWithApiKey", new { userId }).ConfigureAwait(false);
            return dataWrapper.Data;
        }

        public Task ResetFirmLeadMarkByOperatorIdAsync(int operatorId)
        {
            return PostAsync("/ResetFirmLeadMarkByOperatorId", new { operatorId = new IdInfoDto<int>(operatorId) });
        }

        public Task ResetFirmLeadMarkByRegionalPartnerUserIdAsync(int regionalPartnerUserId, CancellationToken cancellationToken = default)
        {
            return PostAsync("/V2/ResetFirmLeadMarkByRegionalPartnerUserId", new IdInfoDto<int>(regionalPartnerUserId), cancellationToken: cancellationToken);
        }

        public async Task<bool> GetEmployerModeAsync(int firmId)
        {
            var data = await GetAsync<DataWrapper<bool>>("/GetEmployerMode", new { firmId }).ConfigureAwait(false);
            return data.Data;
        }

        public async Task<FirmRegistrationType> GetFirmTypeByFirmIdAsync(int firmId)
        {
            var data = await GetAsync<DataWrapper<FirmRegistrationType>>("/GetFirmTypeByFirmId", new { firmId }).ConfigureAwait(false);
            return data.Data;
        }

        public Task<int?> GetReferralIdByFirmIdAsync(int firmId)
        {
            return GetAsync<int?>("/V2/GetReferralIdByFirmId", new { firmId });
        }

        public Task<bool> CheckUserFirmIdAsync(int userId, int firmId)
        {
            return GetAsync<bool>("/V2/CheckUserFirmId", new { userId, firmId });
        }

        public Task<bool> CheckUserHasAccessToFirmAsync(int userId, int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<bool>("/V2/CheckUserFirmId", new { userId, firmId }, cancellationToken: cancellationToken);
        }

        public Task SetIsInternalAsync(int firmId, bool isInternal)
        {
            return PostAsync("/V2/SetIsInternal", new FirmInternalDto { FirmId = firmId, IsInternal = isInternal });
        }

        public Task<bool> GetIsInternalAsync(int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<bool>("/V2/GetIsInternal", new {  firmId }, cancellationToken: cancellationToken);
        }

        public Task<int[]> FilterOutInternalAsync(IReadOnlyCollection<int> firmIds)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<int>());
            }

            return PostAsync<IReadOnlyCollection<int>, int[]>("/V2/Get/FilterOutInternal", firmIds);
        }

        public Task<bool> IsDeletedAsync(int firmId)
        {
            return GetAsync<bool>("/V2/IsDeleted", new {  firmId });
        }
        
        public Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default)
        {
            firmIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, bool>>(
                "/V2/GetFlagsIsDeletedForFirmIds", firmIds, cancellationToken: cancellationToken);
        }

        public Task MarkAsDeletedAsync(int firmId, CancellationToken cancellationToken)
        {
            var uri = $"/v2?firmId={firmId}";

            return DeleteAsync(uri, cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
