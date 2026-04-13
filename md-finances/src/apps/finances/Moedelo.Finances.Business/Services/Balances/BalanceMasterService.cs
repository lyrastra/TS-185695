using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Client.Balances;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster;
using Moedelo.Finances.Domain.Models.BalanceMaster;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.RequisitesV2.Client.FirmCalendar;
using Moedelo.RequisitesV2.Client.FirmRequisites;

namespace Moedelo.Finances.Business.Services.Balances
{
    [InjectAsSingleton(typeof(IBalanceMasterService))]
    public class BalanceMasterService(
        IFirmCalendarApiClient calendarApiClient,
        IBalancesApiClient balancesApiClient,
        IFirmRequisitesClient requisitesApiClient) : IBalanceMasterService
    {
        private static readonly DateTime DateOfStartService = new DateTime(2010, 1, 1);

        public async Task<BalanceMasterStatus> GetStatusAsync(IUserContext userContext,
            CancellationToken cancellationToken)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            var getStausTask = isBiz
                ? GetBizStatusAsync(userContext.FirmId, userContext.UserId, cancellationToken)
                : GetAccStatusAsync(userContext.FirmId, userContext.UserId, cancellationToken);
            return await getStausTask.ConfigureAwait(false);
        }

        public async Task<BalanceMasterStatus> GetBizStatusAsync(int firmId, int userId, CancellationToken ctx)
        {
            var result = new BalanceMasterStatus();
            var events = await calendarApiClient
                .GetEventsListAsync(firmId, userId, CalendarEventProtoId.MoneyBalanceMasterCalendarEvent, cancellationToken: ctx)
                .ConfigureAwait(false);
            var moneyBalanceMasterEvent = events.OrderByDescending(x => x.Year).FirstOrDefault(x => x.Status == CalendarEventStatus.Complete);
            result.IsCompleted = moneyBalanceMasterEvent != null;
            var date = moneyBalanceMasterEvent != null
                ? new DateTime(moneyBalanceMasterEvent.Year, 1, 1)
                : await GetDefaultDateAsync(firmId, userId, ctx).ConfigureAwait(false);
            return result;
        }

        public async Task<BalanceMasterStatus> GetAccStatusAsync(int firmId, int userId,
            CancellationToken cancellationToken)
        {
            var result = new BalanceMasterStatus();
            var date = await balancesApiClient
                .GetDateAsync(firmId, userId, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            result.IsCompleted = date != null;
            result.Date = date ?? await GetDefaultDateAsync(firmId, userId, cancellationToken).ConfigureAwait(false);
            return result;
        }

        private async Task<DateTime> GetDefaultDateAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            var requisites = await requisitesApiClient
                .GetRegistrationDataAsync(firmId, userId, cancellationToken)
                .ConfigureAwait(false);

            var result = requisites?.RegistrationDate ?? DateOfStartService;
            return result.Date;
        }
    }
}