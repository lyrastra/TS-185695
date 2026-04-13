using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [MultipleImplementationsPossible]
    internal interface IConcretePaymentOrderProvider
    {
        Task ProvideAsync(long documentBaseId);
    }
}
