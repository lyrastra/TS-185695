using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PurseOperations
{
    public interface IPurseOperationTaxationSystemUpdater
    {
        Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid);
    }
}
