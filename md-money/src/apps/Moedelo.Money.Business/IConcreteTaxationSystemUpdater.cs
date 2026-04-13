using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business
{
    [MultipleImplementationsPossible]
    internal interface IConcreteTaxationSystemUpdater
    {
        Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType);
    }
}
