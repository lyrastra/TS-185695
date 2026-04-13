using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent.Validation;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IIncomeFromCommissionAgentValidator))]
    internal sealed class IncomeFromCommissionAgentValidator : IIncomeFromCommissionAgentValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly CommissionAgentValidator commissionAgentValidator;
        private readonly IContractsValidator contractsValidator;

        public IncomeFromCommissionAgentValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            CommissionAgentValidator commissionAgentValidator,
            IContractsValidator contractsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.commissionAgentValidator = commissionAgentValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(IncomeFromCommissionAgentSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await commissionAgentValidator.ValidateAsync(request.Kontragent);
            await contractsValidator.ValidateAsync(request.ContractBaseId, request.Kontragent.Id);
        }
    }
}