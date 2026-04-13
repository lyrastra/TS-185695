using System;
using System.Linq;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerValidator))]
    internal sealed class PaymentFromCustomerValidator : IPaymentFromCustomerValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly PaymentFromCustomerDocumentsLinksValidator documentLinksValidator;
        private readonly BillLinksValidator billLinksValidator;
        private readonly PaymentFromCustomerTaxPostingsValidator taxPostingsValidator;
        private readonly PatentValidator patentValidator;
        private readonly IPaymentFromCustomerReader reader;

        public PaymentFromCustomerValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            ITaxationSystemValidator taxationSystemValidator,
            PaymentFromCustomerDocumentsLinksValidator documentLinksValidator,
            BillLinksValidator billLinksValidator,
            PaymentFromCustomerTaxPostingsValidator taxPostingsValidator,
            PatentValidator patentValidator, 
            IPaymentFromCustomerReader reader)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.documentLinksValidator = documentLinksValidator;
            this.billLinksValidator = billLinksValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.patentValidator = patentValidator;
            this.reader = reader;
        }

        public async Task ValidateAsync(PaymentFromCustomerSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateMediation(request);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
                }
            }
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value);
            }
            await patentValidator.ValidateAsync(request.PatentId);
            await documentLinksValidator.ValidateAsync(request);
            await billLinksValidator.ValidateAsync(request.DocumentBaseId, request.BillLinks);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }

        public async Task ValidateAsync(SetReserveRequest request)
        {
            var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
            if (existent == null)
            {
                throw new OperationNotFoundException();
            }

            var documentLinks = existent
                .Documents
                .GetOrThrow()
                ?.Select(x => new DocumentLinkSaveRequest
                {
                    DocumentBaseId = x.DocumentBaseId,
                    LinkSum = x.LinkSum
                })
                .ToArray() ?? Array.Empty<DocumentLinkSaveRequest>();
            
            PaymentSumCoveringValidator.Validate(
                existent.Sum, 
                documentLinks, 
                request.ReserveSum);
        }

        private async Task ValidateMediation(PaymentFromCustomerSaveRequest request)
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