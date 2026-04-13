using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    public interface IReconciliationNotificationSender : IDI
    {
        Task SendSuccesNotificationAsync(int firmId, int userId, Guid sessionId);

        Task SendNoDiffNotificationAsync(int firmId, int userId);
        Task SendErrorNotificationAsync(int firmId, int userId, string message);
    }
}
