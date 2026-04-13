using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.ErptV2.Client.MainPageNotification
{
    public interface IMainPageNotificationClient : IDI
    {
        Task CloseOverdueNotificationAsync(int firmId, int userId);

        Task SetRequestRejectedNotificationAsync(int firmId, int userId);
    }
}
