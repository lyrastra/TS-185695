using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(RefundToSettlementAccountImportValidator))]
    internal sealed class RefundToSettlementAccountImportValidator
    {
        private readonly NumberValidator numberValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;

        public RefundToSettlementAccountImportValidator(NumberValidator numberValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator)
        {
            this.numberValidator = numberValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(ImportRefundToSettlementAccountRequest request)
        {
            await numberValidator.ValidateAsync(request.Number);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Contractor.Id);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Contractor.Id);
                }
            }
        }
    }
}
