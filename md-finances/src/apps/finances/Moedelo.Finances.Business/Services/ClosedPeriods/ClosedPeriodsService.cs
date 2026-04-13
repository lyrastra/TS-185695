using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;
using Moedelo.Finances.Domain.Interfaces.DataAccess.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.ClosedPeriods
{
    [InjectAsSingleton(typeof(IClosedPeriodsService))]
    public class ClosedPeriodsService(IClosedPeriodsDao dao) : IClosedPeriodsService
    {
        // минимально возможная дата закрытого периода
        private readonly DateTime minClosedDate = new DateTime(2012, 12, 31);

        public async Task<DateTime> GetLastClosedDateAsync(IUserContext userContext, CancellationToken ctx)
        {
            var isBizTask = userContext.HasAllRuleAsync(AccessRule.BizPlatform);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            await Task.WhenAll(isBizTask, contextExtraDataTask).ConfigureAwait(false);

            if (isBizTask.Result)
            {
                // в бизе нет закрытых периодов
                return contextExtraDataTask.Result.FirmRegistrationDate?.AddDays(-1) ?? minClosedDate;
            }
            var lastClosedDate = await dao.GetLastClosedDateAsync(userContext.FirmId, ctx).ConfigureAwait(false) ??
                                 minClosedDate;
            if (contextExtraDataTask.Result.FirmRegistrationDate.HasValue && contextExtraDataTask.Result.FirmRegistrationDate > lastClosedDate)
            {
                lastClosedDate = contextExtraDataTask.Result.FirmRegistrationDate.Value.AddDays(-1);
            }
            return lastClosedDate;
        }
    }
}