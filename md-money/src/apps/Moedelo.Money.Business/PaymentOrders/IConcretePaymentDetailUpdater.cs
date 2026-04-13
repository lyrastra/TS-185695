using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders
{
    [MultipleImplementationsPossible]
    internal interface IConcretePaymentDetailUpdater
    {
        Task UpdateAsync(ChangeIsPaidRequestItem item);
    }
}
