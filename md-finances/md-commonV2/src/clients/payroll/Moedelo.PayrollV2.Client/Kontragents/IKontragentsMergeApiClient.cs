using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Client.Kontragents.Dto;
using System.Threading.Tasks;

namespace Moedelo.PayrollV2.Client.Kontragents
{
    public interface IKontragentsMergeApiClient : IDI
    {
        Task ReplaceKontragentsInSalaryObjectsAsync(int firmId, int userId, KontragentReplaceDto request);
    }
}