using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationAccountingPostingsReader : IDI
    {
        Task<List<AccountingPostingList>> GetByBaseIdAsync(IUserContext userContext, long baseId);
    }
}
