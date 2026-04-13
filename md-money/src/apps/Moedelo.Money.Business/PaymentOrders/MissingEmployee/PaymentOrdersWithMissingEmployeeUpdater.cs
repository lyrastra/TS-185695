using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.MissingEmployee;

namespace Moedelo.Money.Business.PaymentOrders.MissingEmployee
{
    [InjectAsSingleton(typeof(IPaymentOrdersWithMissingEmployeeUpdater))]
    public class PaymentOrdersWithMissingEmployeeUpdater : IPaymentOrdersWithMissingEmployeeUpdater
    {
        private readonly IPaymentOrderSetMissingEmployeeCommandWriter commandWriter;

        public PaymentOrdersWithMissingEmployeeUpdater(IPaymentOrderSetMissingEmployeeCommandWriter commandWriter)
        {
            this.commandWriter = commandWriter;
        }

        public Task ApproveImportWithMissingEmployeeAsync(int employeeId, long[] paymentBaseIds)
        {
            return commandWriter.WriteAsync(new PaymentOrderSetMissingEmployeeCommand
            {
                EmployeeId = employeeId,
                DocumentBaseIds = paymentBaseIds
            });
        }
    }
}