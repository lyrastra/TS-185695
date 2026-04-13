using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.Payroll;

namespace Moedelo.Money.Business.PaymentOrders
{
    [MultipleImplementationsPossible]
    public interface IConcretePaymentOrderWithMissingEmployeeUpdater
    {
        Task UpdateAsync(long documentBaseId, Employee employee);
    }
}