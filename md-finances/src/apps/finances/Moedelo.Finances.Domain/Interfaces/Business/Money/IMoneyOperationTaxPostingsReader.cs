using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Finances.Domain.Models.Money.Operations.TaxPostings;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationTaxPostingsReader : IDI
    {
        Task<Dictionary<long, List<TaxSumRec>>> GetSumsByBaseIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds);

        Task<Dictionary<long, List<TaxSumRec>>> GetSumsBySubBaseIdsAsync(IUserContext userContext, Dictionary<long, List<long>> subBaseIds);
        Task<TaxPostingList> GetByBaseIdAsync(IUserContext userContext, long baseId);
    }
}
