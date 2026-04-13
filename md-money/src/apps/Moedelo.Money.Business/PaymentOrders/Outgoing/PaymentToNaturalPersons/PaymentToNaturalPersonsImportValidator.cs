using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(PaymentToNaturalPersonsImportValidator))]
    internal sealed class PaymentToNaturalPersonsImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IEmployeeValidator employeeValidator;

        public PaymentToNaturalPersonsImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IEmployeeValidator employeeValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.employeeValidator = employeeValidator;
        }

        public async Task ValidateAsync(PaymentToNaturalPersonsImportRequest request)
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