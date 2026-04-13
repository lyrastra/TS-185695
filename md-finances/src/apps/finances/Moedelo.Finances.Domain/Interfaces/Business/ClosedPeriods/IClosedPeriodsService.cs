using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;

namespace Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;

public interface IClosedPeriodsService
{
    Task<DateTime> GetLastClosedDateAsync(IUserContext userContext, CancellationToken ctx);
}