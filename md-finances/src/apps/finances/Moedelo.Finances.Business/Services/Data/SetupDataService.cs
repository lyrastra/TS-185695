using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;
using Moedelo.Finances.Domain.Interfaces.Business.Data;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;
using Moedelo.Finances.Domain.Models;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Money.Client;
using Moedelo.RequisitesV2.Client.FirmRequisites;

namespace Moedelo.Finances.Business.Services.Data
{
    [InjectAsSingleton(typeof(ISetupDataService))]
    public class SetupDataService(
        IClosedPeriodsService closedPeriodsService,
        IBalanceMasterService balanceMasterService,
        IMoneyOperationsAccessClient moneyOperationsAccessClient,
        IFirmRequisitesClient firmRequisitesClient,
        IPaymentImportService paymentImportService,
        IIntegrationErrorsService integrationService) : ISetupDataService
    {
        public async Task<SetupData> GetAsync(IUserContext userContext, CancellationToken ctx)
        {
            var balanceMasterStatusTask = balanceMasterService.GetStatusAsync(userContext, ctx);
            var lastClosedDateTask = closedPeriodsService.GetLastClosedDateAsync(userContext, ctx);
            var accessRuleFlagsTask = GetAccessRuleFlagsAsync(userContext);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            var firmRequisitesDataTask = firmRequisitesClient.GetFirmByIdAsync(userContext.FirmId, ctx);
            var importMessagesTask= paymentImportService.GetImportMessagesAsync(userContext, ctx);
            var integrationErrorsTask = integrationService.GetIntegrationErrorsAsync(userContext.FirmId, ctx);

            await Task.WhenAll(
                balanceMasterStatusTask,
                lastClosedDateTask,
                accessRuleFlagsTask,
                contextExtraDataTask,
                firmRequisitesDataTask,
                importMessagesTask,
                integrationErrorsTask)
            .ConfigureAwait(false);

            return new SetupData
            {
                RegistrationDate = contextExtraDataTask.Result.FirmRegistrationDate,
                LastClosedDate = lastClosedDateTask.Result,
                BalanceDate = balanceMasterStatusTask.Result.IsCompleted
                    ? balanceMasterStatusTask.Result.Date
                    : (DateTime?)null,
                AccessRuleFlags = accessRuleFlagsTask.Result,
                RegistrationInService = firmRequisitesDataTask.Result.RegistrationInService,
                ImportMessages = importMessagesTask.Result,
                IntegrationErrors = integrationErrorsTask.Result
            };
        }

        private async Task<AccessRuleFlags> GetAccessRuleFlagsAsync(IUserContext userContext)
        {
            var HasAccessToPostingsTask = userContext.HasAnyRuleAsync(AccessRule.ViewPostings);
            var HasAccessToMoneyEditTask = userContext.HasAnyRuleAsync(AccessRule.AccessToEditAccountingBank, AccessRule.AccessToEditAccountingCash);
            var HasAccessToEditCurrencyOperationsTask = moneyOperationsAccessClient.GetAsync(userContext.FirmId, userContext.UserId);

            await Task.WhenAll(
                HasAccessToPostingsTask,
                HasAccessToMoneyEditTask,
                HasAccessToEditCurrencyOperationsTask)
            .ConfigureAwait(false);

            return new AccessRuleFlags
            {
                HasAccessToPostings = HasAccessToPostingsTask.Result,
                HasAccessToMoneyEdit = HasAccessToMoneyEditTask.Result,
                HasAccessToEditCurrencyOperations = HasAccessToEditCurrencyOperationsTask.Result.CanEditCurrencyOperations
            };
        }
    }
}
