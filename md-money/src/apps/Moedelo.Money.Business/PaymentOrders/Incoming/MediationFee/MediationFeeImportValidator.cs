using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(MediationFeeImportValidator))]
    class MediationFeeImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;

        public MediationFeeImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(MediationFeeImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
        }

        private async Task ValidateKontragentAsync(MediationFeeImportRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            await kontragentsValidator.ValidateAsync(request.Kontragent);

            if (request.OperationState == OperationState.MissingContract)
            {
                return;
            }
            await contractsValidator.ValidateAsync(request.ContractBaseId, request.Kontragent.Id);
        }
    }
}