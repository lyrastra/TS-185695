using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneySourceReader : IDI
    {
        Task<List<MoneySource>> GetAsync(IUserContext userContext, CancellationToken cancellationToken = default);
        Task<List<MoneySource>> GetAsync(IUserContext userContext, IReadOnlyCollection<MoneySourceBase> moneySources);
    }
}