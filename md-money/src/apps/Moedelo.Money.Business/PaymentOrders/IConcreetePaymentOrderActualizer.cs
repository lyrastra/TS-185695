using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;

namespace Moedelo.Money.Business.PaymentOrders
{
    [MultipleImplementationsPossible]
    public interface IConcreetePaymentOrderActualizer
    {
        Task ActualizeAsync(ActualizeRequestItem request);
    }
}
