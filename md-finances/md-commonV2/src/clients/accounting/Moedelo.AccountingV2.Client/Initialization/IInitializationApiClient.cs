using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Initialization
{
    public interface IInitializationApiClient : IDI
    {
        Task<AccountingInitializationState> GetInitializationStatusAsync(int firmId, int userId);

        Task StartInitializationAsync(int firmId, int userId);
    }
}