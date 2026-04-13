using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money;

public interface IMoneyContractorDao
{
    Task<List<Contractor>> GetAsync(int firmId, MoneyContractorType type, CancellationToken cancellationToken);
}