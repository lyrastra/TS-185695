using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Duplicates
{
    [MultipleImplementationsPossible]
    internal interface IConcreteDuplicateImporter
    {
        Task ImportAsync(long documentBaseId);
    }
}
