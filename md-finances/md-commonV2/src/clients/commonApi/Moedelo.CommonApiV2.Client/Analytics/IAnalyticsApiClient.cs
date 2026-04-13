using System;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.Analytics;

namespace Moedelo.CommonApiV2.Client.Analytics;

[Obsolete("Need to use IUserActivityAnalyticsClient")]
public interface IAnalyticsApiClient
{
    Task SendEventAsync(int firmId, int userId, EventRequest request);
}