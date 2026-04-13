using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.AccountingV2.Dto.AutoBb;

namespace Moedelo.AccountingV2.Client.AutoBb
{
    public interface IAutoBbApiClient : IDI
    {
        Task<bool> CheckBalance(int firmId, int userId);
        Task<BalanceFileDto> GetBalanceFileAsync(int firmId, int userId, int year);
        Task<BalanceFileDto> GetBizBalanceFileAsync(int firmId, int userId, int year);
        Task<BalanceFileDto> GetBizTurnoverFileAsync(int firmId, int userId, int year);
        Task<BalanceFileDto> GetBizPostingsFileAsync(int firmId, int userId, int year);
    }
}