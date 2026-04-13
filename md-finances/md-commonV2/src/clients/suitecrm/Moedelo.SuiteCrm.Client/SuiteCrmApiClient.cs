using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.EventBus.Account;
using Moedelo.CommonV2.EventBus.CommonApi;
using Moedelo.CommonV2.EventBus.Crm;
using Moedelo.CommonV2.EventBus.Home;
using Moedelo.CommonV2.EventBus.Pay;
using Moedelo.CommonV2.EventBus.Requisities;
using Moedelo.CommonV2.EventBus.Sps;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto.Activity;
using Moedelo.SuiteCrm.Dto.Case;
using Moedelo.SuiteCrm.Dto.Convert;
using Moedelo.SuiteCrm.Dto.Involvement;
using Moedelo.SuiteCrm.Dto.LeadInfo;
using Moedelo.SuiteCrm.Dto.LoadLead;
using Moedelo.SuiteCrm.Dto.Marketing;
using Moedelo.SuiteCrm.Dto.Payments;
using Moedelo.SuiteCrm.Dto.TrainingAssignment;
using Moedelo.SuiteCrm.Dto.User;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class SuiteCrmApiClient : BaseApiClient, ISuiteCrmApiClient
    {
        private readonly HttpQuerySetting setting = new HttpQuerySetting(new TimeSpan(0, 0, 10, 0));
        private readonly SettingValue apiEndPoint;

        public SuiteCrmApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingsRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingsRepository.Get("SuiteCrmApiUrl");
        }

        public Task<bool> CreateCaseAsync(CaseDto dto)
        {
            return PostAsync<CaseDto, bool>("/Case", dto);
        }

        public Task LoadAllLeadsAsync()
        {
            return PostAsync("/Lead/BulkLoad");
        }

        public Task LoadPartnerAsync(List<int> firmIds)
        {
            return PostAsync("/Lead/LoadPartner", firmIds);
        }

        public Task UpdateLeadInfoLiquidityAsync(UpdateLiqudityDto dto)
        {
            return PostAsync("/LeadInfo/UpdateLiquidity", dto);
        }

        public Task UpdateLeadInfoBankInformedAsync(UpdateBankInformedDto dto)
        {
            return PostAsync("/LeadInfo/UpdateBankInformed", dto);
        }

        public Task UpdateOpportunityStatusAsync(UpdateOpportunityStatusDto dto)
        {
            return PostAsync("/LeadInfo/UpdateOpportunityStatus", dto);
        }

        public Task FillEmptyLeadInfoDataAsync(string byDate)
        {
            return PostAsync($"/LeadInfo/FillEmptyData?byDate={byDate}");
        }

        public Task<LeadLiquidityDataDto> GetLeadLiquidityDataByFirmIdAsync(int firmId)
        {
            return GetAsync<LeadLiquidityDataDto>($"/LeadInfo/LeadLiquidityData", new { firmId });
        }

        public Task<bool> HasLeadOpenOpportunitiesAsync(int firmId)
        {
            return GetAsync<bool>($"/LeadInfo/HasLeadOpenOpportunities", new { firmId });
        }

        public Task RemoveByRenameByUserIdsAsync(IEnumerable<int> userIds)
        {
            return PostAsync("/Lead/RemoveByRenameByUserIds", userIds);
        }

        public Task RemoveByRenameByFirmIdsAsync(IEnumerable<int> firmIds)
        {
            return PostAsync("/Lead/RemoveByRename", firmIds);
        }

        public Task RemoveByFirmIdsAsync(IEnumerable<int> firmIds)
        {
            return PostAsync("/Lead/Remove", firmIds);
        }

        public Task<ActivityResponseDto> LoadAllLeadsForActivityAsync()
        {
            return GetAsync<ActivityResponseDto>("/Activity/LoadAll");
        }

        public Task<ActivityResponseDto> UpdateLeadsActivityAsync()
        {
            return GetAsync<ActivityResponseDto>("/Activity/UpdateAll");
        }

        public Task<ActivityResponseDto> UploadLeadsActivityAsync()
        {
            return GetAsync<ActivityResponseDto>("/Activity/Send");
        }

        public Task<bool> UserWasInPayAsync(UserWasInPayEvent dto)
        {
            return PostAsync<UserWasInPayEvent, bool>("/Activity/UserWasInPay", dto);
        }

        public Task<bool> UserWasInOfferAsync(UserWasInOfferEvent dto)
        {
            return PostAsync<UserWasInOfferEvent, bool>("/Activity/UserWasInOffer", dto);
        }

        public Task<bool> UserWasInWebinarsAsync(UserWasInWebinarsEvent dto)
        {
            return PostAsync<UserWasInWebinarsEvent, bool>("/Activity/UserWasInWebinars", dto);
        }

        public Task<bool> UserLoginCountUpdatedAsync(UserLoginCountUpdateEvent dto)
        {
            return PostAsync<UserLoginCountUpdateEvent, bool>("/Activity/UserLoginCountUpdated", dto);
        }

        public Task<bool> PrimaryNeedUpdateAsync(PrimaryNeedUserEvent dto)
        {
            return PostAsync<PrimaryNeedUserEvent, bool>("/Activity/PrimaryNeedUpdate", dto);
        }

        public Task<bool> LastVisitPageUpdateAsync(CurrentStepUserEvent dto)
        {
            return PostAsync<CurrentStepUserEvent, bool>("/Activity/LastVisitPageUpdate", dto);
        }

        public Task CreateTasksForNoActiveAsync()
        {
            return PostAsync($"/Activity/CreateTasksForNoActive");
        }

        public Task CreateTasksCheckAccountsActivity90DaysAfterFirstPayAsync()
        {
            return PostAsync($"/Activity/CreateTasksCheckAccountsActivity90DaysAfterFirstPay");
        }

        public Task CreateTasksCheckAccountsActivity150DaysAfterFirstPayAsync()
        {
            return PostAsync($"/Activity/CreateTasksCheckAccountsActivity150DaysAfterFirstPay");
        }

        public Task CreateTasksCheckSberbankAccountsActivity()
        {
            return PostAsync($"/Activity/CreateTasksCheckSberbankAccountsActivity");
        }

        public Task RunUpsaleInOutsourceActivityAsync()
        {
            return PostAsync($"/Activity/RunUpsaleInOutsourceActivity");
        }

        public Task UpdatePartnerAsync(UpdatePartnerDto dto)
        {
            return PostAsync("/Account/UpdatePartner", dto);
        }

        public Task UpdatePartner2Async(UpdatePartner2Dto dto)
        {
            return PostAsync("/Account/UpdatePartner2", dto);
        }

        public Task UpdateRequisitiesAsync(UpdateRequisitiesDto dto)
        {
            return PostAsync("/Account/UpdateRequisities", dto);
        }

        public Task UpdateRequisitiesAsync(int firmId)
        {
            return PostAsync("/Account/UpdateFirmRequisities", new UpdateRequisitiesFirmDto {FirmId = firmId});
        }

        public Task UpdateConvetInfoAsync(UpdateConvertInfoDto dto)
        {
            return PostAsync("/Convert/UpdateConvertedInfo", dto);
        }

        public Task UpdateConvetInfoAllAsync()
        {
            return PostAsync("/Convert/UpdateConvertedInfoAll");
        }

        public Task LeadsReactivationAsync()
        {
            return PostAsync("/Reactivation/LoadAll");
        }

        public Task<int> GetOwnerOperatorId(int firmId)
        {
            return GetAsync<int>("/Lead/GetOwnerOperatorId", new {firmId});
        }

        public Task<List<string>> MigrateUserAsync()
        {
            return GetAsync<List<string>>("/Migration/MigrateUser", null);
        }

        public Task<bool> CreateTaskNoTimeAsync(NoTimeEvent dto)
        {
            return PostAsync<NoTimeEvent, bool>("/Task/CreateNoTimeOne", dto);
        }

        public Task<LoadLeadByButtonResponseDto> LoadByLoginWithAuth(int senderUserId, string login)
        {
            return GetAsync<LoadLeadByButtonResponseDto>("/lead/LoadByLoginWithAuth", new {userId = senderUserId, login});
        }

        public Task UpdateLeadForInvolvement(InvolvementInfoDto dto)
        {
            return PostAsync("/Lead/UpdateLeadForInvolvement", dto);
        }

        public Task CalcChannelQualityAsync()
        {
            return PostAsync("/Lead/CalcChannelQuality");
        }


        public Task TaskFromKayakoRequestAsync(TaskFromKayakoDto data)
        {
            return PostAsync("/Marketing/TaskFromKayakoRequest", data);
        }

        public Task CreateSendMarketingTaskReport(string byDate)
        {
            return PostAsync($"/Marketing/CreateSendReport?startDate={byDate}");
        }

        public Task<bool> HandleLeadForInvolvement(CrmBlUserInvolvementEvent dto)
        {
            return PostAsync<CrmBlUserInvolvementEvent, bool>($"/Lead/HandleLeadForInvolvement", dto);
        }

        public Task<List<AssignmentRuleDto>> GetTrainingAssignmentRules()
        {
            return GetAsync<List<AssignmentRuleDto>>("/TrainingAssignment/GetAssignmentRules");
        }

        public Task<List<CrmUserDto>> GetAllCrmUsers()
        {
            return GetAsync<List<CrmUserDto>>("/User/GetAllUsers");
        }

        public Task<List<CrmUserDto>> GetMdOperatorsAsync()
        {
            return GetAsync<List<CrmUserDto>>("/User/GetMdOperators");
        }

        public Task WriteLeadBucketsAsync(IEnumerable<LeadBucketDto> asteriskBuckets)
        {
            return PostAsync($"/AsteriskPrepare/WriteLeadBucketsForImport", asteriskBuckets);
        }

        public Task<CrmUserListDto> GetUsersByCriteriaAsync(CrmUserCriteriaDto criteria)
        {
            return PostAsync<CrmUserCriteriaDto, CrmUserListDto>("/User/GetByCriteria", criteria);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        protected override HttpQuerySetting DefaultHttpQuerySetting()
        {
            return setting;
        }

        public Task<bool> IsPhoneBusyAsync(string phone)
        {
            return GetAsync<bool>($"/Phones/IsPhoneBusy?phone={phone}");
        }
    }
}