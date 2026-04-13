using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders
{
    [MultipleImplementationsPossible]
    internal interface IConcreteCashOrderRemover
    {
        Task DeleteAsync(long documentBaseId);
    }
}
