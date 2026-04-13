using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountManagement.Client.OutsourcingMdUserRole
{
    public interface IOutsourcingMdUserRoleApiClient : IDI
    {
        Task SetRoleToDirectorAsync(int masterUserId, int masterFirmId, IReadOnlyCollection<int> firmIds);

        Task SetRoleToAdminAsync(int masterUserId, int masterFirmId, IReadOnlyCollection<int> firmIds);
    }
}