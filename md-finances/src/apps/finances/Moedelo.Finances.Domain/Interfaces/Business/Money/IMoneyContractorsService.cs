using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money;

public interface IMoneyContractorsService
{
    Task<List<Contractor>> GetAsync(int firmId, int userId, string query, int count, MoneyContractorType type,
        CancellationToken cancellationToken);
    Task<Contractor> GetByIdAsync(int firmId, int userId, int id, MoneyContractorType type);
}