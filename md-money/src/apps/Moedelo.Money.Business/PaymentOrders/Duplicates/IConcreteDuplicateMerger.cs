using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Duplicates
{
    [MultipleImplementationsPossible]
    internal interface IConcreteDuplicateMerger
    {
        Task MergeAsync(PaymentOrderDuplicateMergeRequest request);
    }
}
