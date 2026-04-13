using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.MissingEmployee
{
    public interface IPaymentOrdersWithMissingEmployeeUpdater
    {
        Task ApproveImportWithMissingEmployeeAsync(int employeeId, long[] paymentBaseIds);
    }
}