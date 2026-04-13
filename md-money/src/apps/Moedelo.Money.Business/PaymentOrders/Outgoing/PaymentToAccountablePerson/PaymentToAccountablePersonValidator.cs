using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonValidator))]
    internal sealed class PaymentToAccountablePersonValidator : IPaymentToAccountablePersonValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IEmployeeValidator employeeValidator;
        private readonly AdvanceStatementsValidator advanceStatementsValidator;
        private readonly NumberValidator numberValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public PaymentToAccountablePersonValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IEmployeeValidator employeeValidator,
            AdvanceStatementsValidator advanceStatementsValidator, 
            NumberValidator numberValidator,
            TaxPostingsValidator taxPostingsValidator,
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.employeeValidator = employeeValidator;
            this.advanceStatementsValidator = advanceStatementsValidator;
            this.numberValidator = numberValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        public async Task ValidateCreateRequestAsync(PaymentToAccountablePersonSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);

            if (request.OperationState != OperationState.MissingWorker)
            {
                await employeeValidator.ValidateAsync(request.Employee, "Employee").ConfigureAwait(false);
            }

            if (request.AdvanceStatementBaseIds.Any())
            {
                await advanceStatementsValidator.ValidateAsync(request.AdvanceStatementBaseIds, request.Employee.Id);
            }

            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }

        public async Task ValidateUpdateRequestAsync(PaymentToAccountablePersonSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            await employeeValidator.ValidateAsync(request.Employee, "Employee").ConfigureAwait(false);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }

        private async Task ValidatePaymentNumber(PaymentToAccountablePersonSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number).ConfigureAwait(false);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId).ConfigureAwait(false);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number)
                    .ConfigureAwait(false);
            }
        }
    }
}