using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent.Validation;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IncomeFromCommissionAgentImportValidator))]
    class IncomeFromCommissionAgentImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly CommissionAgentValidator commissionAgentValidator;
        private readonly IContractsValidator contractsValidator;

        public IncomeFromCommissionAgentImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            CommissionAgentValidator commissionAgentValidator,
            IContractsValidator contractsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.commissionAgentValidator = commissionAgentValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(IncomeFromCommissionAgentImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
        }

        private async Task ValidateKontragentAsync(IncomeFromCommissionAgentImportRequest request)
        {
            if (request.OperationState == OperationState.MissingCommissionAgent)
            {
                return;
            }
            await commissionAgentValidator.ValidateAsync(request.Kontragent);

            if (request.OperationState == OperationState.MissingContract)
            {
                return;
            }
            await contractsValidator.ValidateAsync(request.ContractBaseId, request.Kontragent.Id);
        }
    }
}