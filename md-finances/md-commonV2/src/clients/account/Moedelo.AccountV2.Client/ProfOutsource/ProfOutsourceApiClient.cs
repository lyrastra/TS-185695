using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.Filter;
using Moedelo.AccountV2.Dto.FirmOnSeivice;
using Moedelo.AccountV2.Dto.ProfOutsource;
using Moedelo.AccountV2.Dto.Role;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.ProfOutsource
{
    [InjectAsSingleton]
    public class ProfOutsourceApiClient : BaseApiClient, IProfOutsourceApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ProfOutsourceApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("ProfOutsourceApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<ProfOutsourceContextDto> GetOutsourceContextAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            var uri = $"/OutsourceContext?firmId={firmId}&userId={userId}";
            
            return GetAsync<ProfOutsourceContextDto>(uri, cancellationToken: cancellationToken);
        }

        public Task<List<InviteDto>> GetNewInvites(int firmId, int userId, CancellationToken cancellationToken = default)
        {
            const string uri = "/GetNewInvitesList";
            return GetAsync<List<InviteDto>>(uri, new { firmId, userId }, cancellationToken: cancellationToken);
        }

        public Task<InviteDto> SendInviteAsync(int firmId, int userId, InviteDto dto)
        {
            return PostAsync<InviteDto, InviteDto>($"/V2/SendInvite?firmId={firmId}&userId={userId}", dto);
        }

        public Task AcceptInviteAsync(int firmId, int userId, int inviteId)
        {
            // непонятно в чьей юрисдикции этот функционал при повторении таймаута нужно точно расбираться чье это а пока просто взвентил таймаут в небесаЦ
            var time = new HttpQuerySetting(TimeSpan.FromMinutes(2));
            return PostAsync($"/V2/AcceptInvite?firmId={firmId}&userId={userId}&inviteId={inviteId}", setting: time);
        }

        public Task<List<string>> ServiceGroupAutocompleteAsync(int firmId, int userId, string query = null, int count = 10)
        {
            return GetAsync<List<string>>("/V2/ServiceGroupAutocomplete", new { firmId, userId, query, count });
        }

        public async Task<List<FirmRolesDto>> GetFirmRolesAsync(int firmId, int userId, IReadOnlyCollection<int> firmIds)
        {
            firmIds = firmIds.AsSet();
            var listWrapper = await PostAsync<IReadOnlyCollection<int>, ListWrapper<FirmRolesDto>>($"/GetFirmRoles?firmId={firmId}&userId={userId}", firmIds).ConfigureAwait(false);
            return listWrapper.Items;
        }

        public Task<ListWithTotalCount<SlaveFirmDto>> MySlaveFirmsAsync(int firmId, int userId, FilterRequestDto<FirmFilterField> request)
        {
            return PostAsync<FilterRequestDto<FirmFilterField>, ListWithTotalCount<SlaveFirmDto>>($"/MySlaveFirms?firmId={firmId}&userId={userId}", request);
        }

        public Task<int> CountUserAccessibleFirmsAsync(int userId, CancellationToken ct)
        {
            var uri = $"/CountUserAccessibleFirms?userId={userId}";

            return GetAsync<int>(uri, cancellationToken: ct);
        }

        public async Task<AccountDto> GetProfOutsourceForFirmAsync(int firmId, int userId, int slaveFirmId, CancellationToken cancellationToken)
        {
            const string uri = "/GetProfOutsourceForFirm";
            var queryParams = new { firmId, userId, slaveFirmId };
            
            var result = await GetAsync<DataWrapper<AccountDto>>(
                    uri, queryParams, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<FirmOnServiceDto>> GetFirmsOnServiceAsync(int firmId, int userId, IReadOnlyCollection<int> slaveFirmIds)
        {
            slaveFirmIds = slaveFirmIds.AsSet();
            var result = await PostAsync<IReadOnlyCollection<int>, DataWrapper<List<FirmOnServiceDto>>>(
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

        public async Task<List<AutocompleteFirmDto>> MyFirmsAutocompleteAsync(int firmId, int userId, string query, int count)
        {
            var listWrapper = await GetAsync<ListWrapper<AutocompleteFirmDto>>("/MyFirmsAutocomplete", new { firmId, userId, query, count }).ConfigureAwait(false);
            return listWrapper.Items;
        }

        public Task<Result> RemovePpaAsync(int firmId, int userId, IReadOnlyCollection<int> ids)
        {
            ids = ids.AsSet();
            return PostAsync<IReadOnlyCollection<int>, Result> ($"/RemovePpa?firmId={firmId}&userId={userId}", ids);
        }

        public Task RejectInvitesAsync(int firmId, int userId, IReadOnlyCollection<int> inviteIds)
        {
            inviteIds = inviteIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, Result>($"/RejectInvites?firmId={firmId}&userId={userId}", inviteIds);
        }

        public Task DetachSlaveFirmsAsync(int firmId, int userId, IReadOnlyCollection<int> slaveFirmIds)
        {
            slaveFirmIds = slaveFirmIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, Result>($"/DetachSlaveFirms?firmId={firmId}&userId={userId}", slaveFirmIds);
        }

        public Task<Result> SetServiceGroupAsync(int firmId, int userId, int slaveFirmId, string name)
        {
            return PostAsync<Result>($"/SetServiceGroup?firmId={firmId}&userId={userId}&slaveFirmId={slaveFirmId}&name={name}");
        }

        public Task<FirmInfoDto> GetSlaveFirmInfoAsync(int firmId, int userId, int slaveFirmId)
        {
            return GetAsync<FirmInfoDto>("/GetSlaveFirmInfo", new { firmId, userId, slaveFirmId });
        }

        public Task<SaveOutsourceDto> SaveAsync(int firmId, int? userId, ProfOutsourceDto outsourceDto)
        {
            return PostAsync<ProfOutsourceDto, SaveOutsourceDto>($"/Save?firmId={firmId}&userId={userId}", outsourceDto);
        }

        public async Task<List<TariffDto>> MyOutsourceTariffsAsync(int firmId, int userId)
        {
            var listWrapper = await GetAsync<ListWrapper<TariffDto>>("/MyOutsourceTariffs", new { firmId, userId }).ConfigureAwait(false);
            return listWrapper.Items;
        }

        public Task<ListWithCountDto<ProfOutsourceDto>> GetAllAsync(int offset = 0, int count = 20)
        {
            return GetAsync<ListWithCountDto<ProfOutsourceDto>>("/GetAll", new { offset, count });
        }

        public async Task<ProfOutsourceDto> GetAsync(int id)
        {
            var data = await GetAsync<DataWrapper<ProfOutsourceDto>>("/Get", new { id }).ConfigureAwait(false);
            return data.Data;
        }

        public Task<ListWithCountDto<ProfOutsourceDto>> GetAvailableByRegionalPartnerIdAsync(int regionalPartnerId,
            int offset = 0,
            int count = 20)
        {
            return GetAsync<ListWithCountDto<ProfOutsourceDto>>(
                "/GetAvailableByRegionalPartnerId",
                new { regionalPartnerId, offset, count });
        }

        public Task<ListWithCountDto<ProfOutsourceDto>> GetAvailableAsync(int offset = 0, int count = 20)
        {
            return GetAsync<ListWithCountDto<ProfOutsourceDto>>("/GetAvailable", new { offset, count });
        }

        public Task AttachAsync(int regionalPartnerId, int professionalOutsourcerId)
        {
            return PostAsync($"/ReattachToRegionalPartner?" +
                             $"regionalPartnerId={regionalPartnerId}&professionalOutsourcerId={professionalOutsourcerId}");
        }

        public Task DetachFromRegionalPartnerAsync(int regionalPartnerId)
        {
            return PostAsync($"/DetachFromRegionalPartner?regionalPartnerId={regionalPartnerId}");
        }

        public Task<List<ProfOutsourceDto>> GetAttachedToGuAsync()
        {
            return GetAsync<List<ProfOutsourceDto>>("/GetAttachedToGu");
        }
    }
}
