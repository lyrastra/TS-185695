using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Operations.TaxationSystemChangingSync
{
    public interface ITaxationSystemChangingSyncObjectManager
    {
        Task ChangeStateAsync(Guid guid, long documentBaseId);
    }
}