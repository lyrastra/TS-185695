using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonValidator))]
    internal sealed class RefundFromAccountablePersonValidator : IRefundFromAccountablePersonValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IEmployeeValidator employeeValidator;
        private readonly AdvanceStatementsValidator advanceStatementsValidator;

        public RefundFromAccountablePersonValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IEmployeeValidator employeeValidator,
            AdvanceStatementsValidator advanceStatementsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.employeeValidator = employeeValidator;
            this.advanceStatementsValidator = advanceStatementsValidator;
        }

        public async Task ValidateAsync(RefundFromAccountablePersonSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingWorker)
            {
                await employeeValidator.ValidateAsync(request.Employee, "Employee");
            }
            
            await ValidateAdvanceStatementAsync(request);
        }

        private async Task ValidateAdvanceStatementAsync(RefundFromAccountablePersonSaveRequest request)
        {
            if (request.AdvanceStatementBaseId == null)
            {
                return;
            }
            await advanceStatementsValidator.ValidateAsync(request.AdvanceStatementBaseId.Value, request.Employee.Id);
        }
    }
}