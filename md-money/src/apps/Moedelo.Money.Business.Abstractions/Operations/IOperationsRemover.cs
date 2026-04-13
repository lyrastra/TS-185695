using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Operations
{
    public interface IOperationsRemover
    {
        Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
