using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.BankBalanceHistory;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.BankBalanceHistory
{
    public interface IMoneyBankBalanceApiClient : IDI
    {
        Task<BankBalanceResponseDto> GetAsync(int firmId, int userId, BankBalanceRequestDto request);
    }
}
