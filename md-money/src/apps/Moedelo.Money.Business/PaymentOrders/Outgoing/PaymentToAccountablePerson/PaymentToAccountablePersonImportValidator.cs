using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(PaymentToAccountablePersonImportValidator))]
    internal sealed class PaymentToAccountablePersonImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IEmployeeValidator employeeValidator;

        public PaymentToAccountablePersonImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IEmployeeValidator employeeValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.employeeValidator = employeeValidator;
        }

        public async Task ValidateAsync(PaymentToAccountablePersonImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);

            if (request.OperationState == OperationState.MissingWorker)
            {
                return;
            }

            await employeeValidator.ValidateAsync(request.EmployeeId.Value);
            EmployeeNameValidator.Validate256(request.EmployeeName, "EmployeeName");
        }
    }
}