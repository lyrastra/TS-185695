using System;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy
{
    public interface IMoneyClosedPeriodsApiClient
    {
        Task<DateTime> GetLastClosedDateAsync(FirmId firmId, UserId userId);
    }
}