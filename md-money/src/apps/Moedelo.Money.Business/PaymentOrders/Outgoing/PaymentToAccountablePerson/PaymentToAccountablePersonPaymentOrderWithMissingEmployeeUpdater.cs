using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToAccountablePerson)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderWithMissingEmployeeUpdater))]
    public class PaymentToAccountablePersonPaymentOrderWithMissingEmployeeUpdater : IConcretePaymentOrderWithMissingEmployeeUpdater
    {
        private readonly IPaymentToAccountablePersonReader reader;
        private readonly IPaymentToAccountablePersonUpdater updater;

        public PaymentToAccountablePersonPaymentOrderWithMissingEmployeeUpdater(IPaymentToAccountablePersonReader reader,
            IPaymentToAccountablePersonUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task UpdateAsync(long documentBaseId, Employee employee)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState != OperationState.MissingWorker)
            {
                return;
            }

            response.Employee = new Employee();
            var request = PaymentToAccountablePersonMapper.MapToSaveRequest(response);
            request.DocumentBaseId = documentBaseId;
            request.OperationState = OperationState.Default;
            request.Employee = employee;

            await updater.UpdateAsync(request);
        }
    }
}