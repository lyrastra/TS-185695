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
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
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
    public interface ISuiteCrmApiClient : IDI
    {
        Task<bool> CreateCaseAsync(CaseDto dto);

        Task LoadAllLeadsAsync();

        Task LoadPartnerAsync(List<int> firmIds);

        Task FillEmptyLeadInfoDataAsync(string byDate);

        Task UpdateLeadInfoLiquidityAsync(UpdateLiqudityDto dto);

        Task UpdateLeadInfoBankInformedAsync(UpdateBankInformedDto dto);

        Task UpdateOpportunityStatusAsync(UpdateOpportunityStatusDto dto);

        Task RemoveByRenameByUserIdsAsync(IEnumerable<int> userIds);

        Task RemoveByRenameByFirmIdsAsync(IEnumerable<int> firmIds);

        Task RemoveByFirmIdsAsync(IEnumerable<int> firmIds);

        Task UpdatePartnerAsync(UpdatePartnerDto dto);

        Task UpdatePartner2Async(UpdatePartner2Dto dto);

        Task UpdateRequisitiesAsync(UpdateRequisitiesDto dto);
        Task UpdateRequisitiesAsync(int firmId);

        Task UpdateConvetInfoAsync(UpdateConvertInfoDto dto);

        Task UpdateConvetInfoAllAsync();

        Task LeadsReactivationAsync();

        #region Activity
        Task<ActivityResponseDto> LoadAllLeadsForActivityAsync();

        Task<ActivityResponseDto> UpdateLeadsActivityAsync();

        Task<ActivityResponseDto> UploadLeadsActivityAsync();

        Task<bool> UserWasInPayAsync(UserWasInPayEvent lead);

        Task<bool> UserWasInOfferAsync(UserWasInOfferEvent lead);

        Task<bool> UserWasInWebinarsAsync(UserWasInWebinarsEvent lead);

        Task<bool> UserLoginCountUpdatedAsync(UserLoginCountUpdateEvent lead);

        Task<bool> PrimaryNeedUpdateAsync(PrimaryNeedUserEvent dto);

        Task<bool> LastVisitPageUpdateAsync(CurrentStepUserEvent dto);

        Task RunUpsaleInOutsourceActivityAsync();
        #endregion

        Task<int> GetOwnerOperatorId(int firmId);

        Task<List<string>> MigrateUserAsync();

        Task<bool> CreateTaskNoTimeAsync(NoTimeEvent dto);

        [Obsolete("Use LeadLoadCommand")]
        Task<LoadLeadByButtonResponseDto> LoadByLoginWithAuth(int senderUserId, string login);

        Task UpdateLeadForInvolvement(InvolvementInfoDto dto);

        Task CalcChannelQualityAsync();

        Task TaskFromKayakoRequestAsync(TaskFromKayakoDto data);

        Task CreateSendMarketingTaskReport(string byDate);

        Task<bool> HandleLeadForInvolvement(CrmBlUserInvolvementEvent dto);

        Task<List<AssignmentRuleDto>> GetTrainingAssignmentRules();

        [Obsolete("Use GetByCriteriaAsync(new CrmUserCriteriaDto{ OnlyMdOperators = true })")]
        Task<List<CrmUserDto>> GetAllCrmUsers();

        [Obsolete("Use GetByCriteriaAsync(new CrmUserCriteriaDto{ OnlyMdOperators = true })")]
        Task<List<CrmUserDto>> GetMdOperatorsAsync();

        Task WriteLeadBucketsAsync(IEnumerable<LeadBucketDto> asteriskBuckets);

        Task CreateTasksForNoActiveAsync();

        Task CreateTasksCheckAccountsActivity90DaysAfterFirstPayAsync();

        Task CreateTasksCheckAccountsActivity150DaysAfterFirstPayAsync();

        Task CreateTasksCheckSberbankAccountsActivity();

        Task<LeadLiquidityDataDto> GetLeadLiquidityDataByFirmIdAsync(int firmId);

        Task<CrmUserListDto> GetUsersByCriteriaAsync(CrmUserCriteriaDto criteria);

        Task<bool> HasLeadOpenOpportunitiesAsync(int firmId);

        Task<bool> IsPhoneBusyAsync(string phone);
    }
}