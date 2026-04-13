using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmReportsApiClient : IDI
    {
        Task<byte[]> GetRoamingInvitationsReportAsync(string date);
    }
}