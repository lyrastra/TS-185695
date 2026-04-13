using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee
{
    public interface IPaymentOrderSetMissingEmployeeCommandWriter
    {
        Task WriteAsync(PaymentOrderSetMissingEmployeeCommand command);
    }
}
