using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerImportValidator))]
    internal sealed class PaymentFromCustomerImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly PaymentFromCustomerDocumentsLinksValidator documentLinksValidator;
        private readonly BillLinksValidator billLinksValidator;

        public PaymentFromCustomerImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            PaymentFromCustomerDocumentsLinksValidator documentLinksValidator,
            BillLinksValidator billLinksValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.documentLinksValidator = documentLinksValidator;
            this.billLinksValidator = billLinksValidator;
        }

        public async Task ValidateAsync(PaymentFromCustomerImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateMediation(request);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent.Id);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
                }
            }

            var documentLinksId = request.DocumentLinks?.Select(l => l.DocumentBaseId).ToList();
            await DocumentLinksDuplicateValidator.Validate(documentLinksId);

            var billLinksId = request.BillLinks?.Select(l => l.BillBaseId).ToList();
            await DocumentLinksDuplicateValidator.Validate(billLinksId);
        }

        private async Task ValidateMediation(PaymentFromCustomerImportRequest request)
        {
            if (request.IsMediation == false)
            {
                return;
            }
            var taxationSystemType = await taxationSystemTypeReader.GetByYearAsync(request.Date.Year);
            if (taxationSystemType == TaxationSystemType.Osno ||
                taxationSystemType == TaxationSystemType.OsnoAndEnvd)
            {
                throw new BusinessValidationException("Mediation.IsMediation", "Нельзя включить посредничество для ОСНО");
            }
        }
    }
}