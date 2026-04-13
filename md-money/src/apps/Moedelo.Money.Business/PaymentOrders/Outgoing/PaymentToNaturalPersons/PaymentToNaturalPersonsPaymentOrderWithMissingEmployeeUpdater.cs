using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderWithMissingEmployeeUpdater))]
    public class PaymentToNaturalPersonsPaymentOrderWithMissingEmployeeUpdater : IConcretePaymentOrderWithMissingEmployeeUpdater
    {
        private readonly IPaymentToNaturalPersonsReader reader;
        private readonly IPaymentToNaturalPersonsUpdater updater;

        public PaymentToNaturalPersonsPaymentOrderWithMissingEmployeeUpdater(IPaymentToNaturalPersonsReader reader,
            IPaymentToNaturalPersonsUpdater updater)
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

            var request = PaymentToNaturalPersonsMapper.MapToSaveRequest(response);
            request.DocumentBaseId = documentBaseId;
            request.OperationState = OperationState.Default;
            request.PaymentType = employee.IsNotStaff
                ? PaymentToNaturalPersonsType.Gpd
                : PaymentToNaturalPersonsType.WorkContract;
            request.EmployeePayments = new[]
            {
                new EmployeePaymentsSaveModel
                {
                    EmployeeId = employee.Id,
                    EmployeeName = employee.Name,
                    ChargePayments = new []
                    {
                        new ChargePayment()
                        {
                            Sum = response.EmployeePayments.Sum(x => x.Sum)
                        }
                    }
                }
            };

            await updater.UpdateAsync(request);
        }
    }
}