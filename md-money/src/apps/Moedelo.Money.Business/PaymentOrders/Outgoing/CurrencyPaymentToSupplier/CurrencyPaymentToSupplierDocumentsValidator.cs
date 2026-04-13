using Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(CurrencyPaymentToSupplierDocumentsValidator))]
    internal class CurrencyPaymentToSupplierDocumentsValidator
    {
        private static readonly IReadOnlyCollection<LinkedDocumentType> RestrictTypes = new[]
        {
            LinkedDocumentType.PurchaseCurrencyInvoice
        };

        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;
        private readonly IPurchasesCurrencyInvoicesApiClient currencyInvoicesApiClient;

        public CurrencyPaymentToSupplierDocumentsValidator(
            IBaseDocumentReader baseDocumentReader,
            LinkedDocumentPaidSumReader paidSumReader,
            IPurchasesCurrencyInvoicesApiClient currencyInvoicesApiClient)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.paidSumReader = paidSumReader;
            this.currencyInvoicesApiClient = currencyInvoicesApiClient;
        }

        public async Task ValidateAsync(
            CurrencyPaymentToSupplierSaveRequest request,
            SettlementAccount settlementAccount)
        {
            var documentLinks = request.DocumentLinks;
            if (documentLinks?.Any() != true)
            {
                return;
            }

            var documentBaseIds = documentLinks.Select(x => x.DocumentBaseId).ToArray();
            await DocumentLinksDuplicateValidator.Validate(documentBaseIds);

            var baseDocuments = await baseDocumentReader.GetByIdsAsync(documentBaseIds);
            var paidSums = await paidSumReader.GetPaidSumsForOutgoingPaymentAsync(request.DocumentBaseId, baseDocuments, RestrictTypes);
            var currencyInvoices = await currencyInvoicesApiClient.GetByBaseIdsAsync(documentBaseIds);

            var i = 0;
            foreach (var link in documentLinks)
            {
                var document = currencyInvoices.FirstOrDefault(x => x.DocumentBaseId == link.DocumentBaseId);
                if (document == null)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId", $"Не найден документ #{link.DocumentBaseId}");
                }

                if (document.KontragentId != request.Kontragent?.Id)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId", $"Контрагент в документе #{link.DocumentBaseId} не совпадает с контрагентом в платеже");
                }

                var requestCurrency = (int)settlementAccount.Currency;
                if (document.Currency != requestCurrency)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId", $"Валюта документа #{link.DocumentBaseId} не совпадает с валютой расчетного счета в платеже");
                }

                var accessibleSum = document.Sum - paidSums.GetValueOrDefault(document.DocumentBaseId, 0);
                if (accessibleSum < link.LinkSum)
                {
                    throw new BusinessValidationException($"Documents[{i}].Sum", $"Сумма документа #{document.DocumentBaseId} доступная к оплате меньше, чем сумма указаная к оплате");
                }

                i++;
            }
        }
    }
}