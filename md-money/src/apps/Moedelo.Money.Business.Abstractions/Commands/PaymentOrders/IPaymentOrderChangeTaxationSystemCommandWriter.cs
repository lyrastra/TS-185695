using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders
{
    public interface IPaymentOrderChangeTaxationSystemCommandWriter
    {
        Task WriteAsync(PaymentOrderChangeTaxationSystemCommand command);
    }
}
