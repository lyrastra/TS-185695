using Moedelo.Common.Enums.Enums.Catalog;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.CatalogV2.Client.Banks
{
    public interface IBankIconApiClient: IDI
    {
        Task<IReadOnlyDictionary<int, string>> GetByBankIdsAsync(IReadOnlyCollection<int> bankIds);

        Task<IReadOnlyDictionary<BankRegistrationNumber, string>> GetByRegistrationNumberAsync(IReadOnlyCollection<BankRegistrationNumber> registrationNumbers);
    }
}
