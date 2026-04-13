using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders
{
    [MultipleImplementationsPossible]
    internal interface IConcretePaymentOrderDeletedEventWriter
    {
        Task WriteDeletedEventAsync(long documentBaseId);
    }
}
