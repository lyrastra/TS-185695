using Moedelo.Contracts.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(AgencyContractImportValidator))]
    class AgencyContractImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;

        public AgencyContractImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(AgencyContractImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
        }

        private async Task ValidateKontragentAsync(AgencyContractImportRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            await kontragentsValidator.ValidateAsync(request.Kontragent.Id);

            if (request.OperationState == OperationState.MissingContract)
            {
                return;
            }
            var contract = await contractsValidator.ValidateAsync(request.ContractBaseId, request.Kontragent.Id);
            if (contract.ContractKind != ContractKind.Mediation)
            {
                throw new BusinessValidationException(nameof(request.ContractBaseId), $"Договор с ид {request.ContractBaseId} не является посредническим договором");
            }
        }
    }
}