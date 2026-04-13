using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Cash;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Client.Cash
{
    public interface ICashApiClient : IDI
    {
        Task<List<CashDto>> GetAsync(int firmId, int userId);

        Task<CashDto> GetByIdAsync(int firmId, int userId, long id);

        Task<List<CashDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids);

        Task<int> GetOutgoingCashOrderNextNumber(int firmId, int userId, long cashId, int year);
    }
}