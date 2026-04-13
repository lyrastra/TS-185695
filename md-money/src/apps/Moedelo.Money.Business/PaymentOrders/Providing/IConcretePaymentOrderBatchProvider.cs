using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [MultipleImplementationsPossible]
    internal interface IConcretePaymentOrderBatchProvider
    {
        Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
