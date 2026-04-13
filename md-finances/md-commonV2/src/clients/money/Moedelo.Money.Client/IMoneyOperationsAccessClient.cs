using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto;

namespace Moedelo.Money.Client
{
    public interface IMoneyOperationsAccessClient : IDI
    {
        Task<OperationsAccessDto> GetAsync(int firmId, int userId);
    }
}