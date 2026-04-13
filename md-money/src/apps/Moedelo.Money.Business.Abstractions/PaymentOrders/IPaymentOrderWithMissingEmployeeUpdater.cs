using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderWithMissingEmployeeUpdater
    {
        Task UpdateAsync(int employeeId, long[] paymentOrdersBaseIds);
    }
}