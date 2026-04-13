using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.Money;

public interface IMoneyOperationsApiClient
{
    Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds);
}