using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.UserActivityAnalytics.Client.Dto;

namespace Moedelo.UserActivityAnalytics.Client.Clients
{
    public interface IUserActivityAnalyticsClient : IDI
    {
        Task SendEventAsync(int firmId, int userId, EventRequest request);
    }
}