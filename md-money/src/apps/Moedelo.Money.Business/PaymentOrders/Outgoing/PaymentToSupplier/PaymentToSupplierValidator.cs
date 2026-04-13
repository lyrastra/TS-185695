using System;
using System.Linq;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierValidator))]
    internal sealed class PaymentToSupplierValidator : IPaymentToSupplierValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly PaymentToSupplierDocumentLinksValidator documentLinksValidator;
        private readonly NumberValidator numberValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IPaymentToSupplierReader reader;

        public PaymentToSupplierValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            PaymentToSupplierDocumentLinksValidator documentLinksValidator,
            NumberValidator numberValidator,
            TaxPostingsValidator taxPostingsValidator,
            IPaymentOrderGetter paymentOrderGetter,
            IPaymentToSupplierReader reader)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.documentLinksValidator = documentLinksValidator;
            this.numberValidator = numberValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.paymentOrderGetter = paymentOrderGetter;
            this.reader = reader;
        }

        public async Task ValidateAsync(PaymentToSupplierSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await kontragentsValidator.ValidateAsync(request.Kontragent);
            if (request.ContractBaseId.HasValue)
            {
                await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
            }
            await documentLinksValidator.ValidateAsync(request);
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

        private async Task ValidatePaymentNumber(PaymentToSupplierSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number);
            }
        }
    }
}