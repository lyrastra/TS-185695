using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Bills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.MiddlemanReports;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices;
using Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills;
using Moedelo.Infrastructure.System.Extensions;

namespace Moedelo.Money.Business.LinkedDocuments.Links
{
    [InjectAsSingleton(typeof(LinkedDocumentPaidSumReader))]
    internal sealed class LinkedDocumentPaidSumReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ILinksReader linksReader;
        private readonly ISalesBillApiClient billApiClient;
        private readonly ISalesWaybillApiClient salesWaybillApiClient;
        private readonly IPurchasesWaybillApiClient purchasesWaybillApiClient;
        private readonly IOutgoingStatementClient outgoingStatementClient;
        private readonly IIncomingStatementClient incomingStatementClient;
        private readonly ISalesUpdApiClient salesUpdApiClient;
        private readonly IPurchasesUpdApiClient purchasesUpdApiClient;
        private readonly IMiddlemanReportClient middlemanReportClient;
        private readonly ISalesCurrencyInvoicesApiClient salesCurrencyInvoicesApiClient;
        private readonly IPurchasesCurrencyInvoicesApiClient purchasesCurrencyInvoicesApiClient;
        private readonly IInventoryCardApiClient inventoryCardApiClient;

        public LinkedDocumentPaidSumReader(
            IExecutionInfoContextAccessor contextAccessor,
            ILinksReader linksReader,
            ISalesBillApiClient billApiClient,
            ISalesWaybillApiClient salesWaybillApiClient,
            IPurchasesWaybillApiClient purchasesWaybillApiClient,
            IOutgoingStatementClient outgoingStatementClient,
            IIncomingStatementClient incomingStatementClient,
            ISalesUpdApiClient salesUpdApiClient,
            IPurchasesUpdApiClient purchasesUpdApiClient,
            IMiddlemanReportClient middlemanReportClient,
            ISalesCurrencyInvoicesApiClient salesCurrencyInvoicesApiClient,
            IPurchasesCurrencyInvoicesApiClient purchasesCurrencyInvoicesApiClient,
            IInventoryCardApiClient inventoryCardApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.linksReader = linksReader;
            this.billApiClient = billApiClient;
            this.salesWaybillApiClient = salesWaybillApiClient;
            this.purchasesWaybillApiClient = purchasesWaybillApiClient;
            this.outgoingStatementClient = outgoingStatementClient;
            this.incomingStatementClient = incomingStatementClient;
            this.salesUpdApiClient = salesUpdApiClient;
            this.purchasesUpdApiClient = purchasesUpdApiClient;
            this.middlemanReportClient = middlemanReportClient;
            this.salesCurrencyInvoicesApiClient = salesCurrencyInvoicesApiClient;
            this.purchasesCurrencyInvoicesApiClient = purchasesCurrencyInvoicesApiClient;
            this.inventoryCardApiClient = inventoryCardApiClient;
        }

        public Task<IReadOnlyDictionary<long, decimal>> GetPaidSumsForIncomingPaymentAsync(
            long paymentBaseId,
            IReadOnlyCollection<BaseDocument> baseDocuments,
            IReadOnlyCollection<LinkedDocumentType> restrictTypes)
        {
            return PaidSumsForPaymentAsync(
                GetSalesDocPaidSumFuncByTypeAsync,
                paymentBaseId,
                baseDocuments,
                restrictTypes);
        }

        public Task<IReadOnlyDictionary<long, IReadOnlyDictionary<long, decimal>>> GetPaidSumsForIncomingPaymentAsync(
            IReadOnlyDictionary<long, LinkWithDocument[]> documentLinksByPaymentMap,
            IReadOnlyCollection<LinkedDocumentType> restrictTypes)
        {
            return PaidSumsForPaymentAsync(
                GetSalesDocPaidSumFuncByTypeAsync,
                documentLinksByPaymentMap,
                restrictTypes);
        }

        public Task<IReadOnlyDictionary<long, decimal>> GetPaidSumsForOutgoingPaymentAsync(
            long paymentBaseId,
            IReadOnlyCollection<BaseDocument> baseDocuments,
            IReadOnlyCollection<LinkedDocumentType> restrictTypes)
        {
            return PaidSumsForPaymentAsync(
                GetPurchaseDocPaidSumFuncByTypeAsync,
                paymentBaseId,
                baseDocuments,
                restrictTypes);
        }

        public Task<IReadOnlyDictionary<long, IReadOnlyDictionary<long, decimal>>> GetPaidSumsForOutgoingPaymentAsync(
            IReadOnlyDictionary<long, LinkWithDocument[]> documentLinksByPaymentMap,
            IReadOnlyCollection<LinkedDocumentType> restrictTypes)
        {
            return PaidSumsForPaymentAsync(
                GetPurchaseDocPaidSumFuncByTypeAsync,
                documentLinksByPaymentMap,
                restrictTypes);
        }

        private async Task<IReadOnlyDictionary<long, decimal>> PaidSumsForPaymentAsync(
            Func<LinkedDocumentType, IReadOnlyCollection<long>, Task<IReadOnlyCollection<PaidSumDto>>> docPaidSumFuncByTypeFunc,
            long paymentBaseId,
            IReadOnlyCollection<BaseDocument> baseDocuments,
            IReadOnlyCollection<LinkedDocumentType> restrictTypes)
        {
            if (paymentBaseId == 0 || baseDocuments.Count == 0)
            {
                return new Dictionary<long, decimal>();
            }

            var paidSumTasks = await baseDocuments
                .Where(baseDocument => restrictTypes.Contains(baseDocument.Type))
                .GroupBy(baseDocument => baseDocument.Type)
                .SelectParallelAsync(docsByType =>
                {
                    var type = docsByType.Key;
                    var baseIds = docsByType.Select(baseDocument => baseDocument.Id).ToArray();

                    return docPaidSumFuncByTypeFunc(type, baseIds);
                });

            var paidSums = paidSumTasks.SelectMany(paidSum => paidSum).ToArray();
            var paidSumsByIds = paidSums
                .ToDictionary(
                    paidSum => paidSum.DocumentBaseId,
                    paidSum => paidSum.PaidSum);

            if (paymentBaseId > 0)
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(paymentBaseId);
                foreach (var link in links)
                {
                    if (paidSumsByIds.ContainsKey(link.Document.Id))
                    {
                        paidSumsByIds[link.Document.Id] -= link.Sum;
                    }
                }
            }

            return paidSumsByIds;
        }

        private static async Task<IReadOnlyDictionary<long, IReadOnlyDictionary<long, decimal>>>
            PaidSumsForPaymentAsync(
                Func<LinkedDocumentType, IReadOnlyCollection<long>, Task<IReadOnlyCollection<PaidSumDto>>> docPaidSumFuncByTypeFunc,
                IReadOnlyDictionary<long, LinkWithDocument[]> documentLinksByPaymentMap,
                IReadOnlyCollection<LinkedDocumentType> restrictTypes)
        {
            if (documentLinksByPaymentMap.Count == 0)
            {
                return new Dictionary<long, IReadOnlyDictionary<long, decimal>>();
            }

            var paidSumTasks = await documentLinksByPaymentMap.Values
                .SelectMany(x => x)
                .Where(link => restrictTypes.Contains(link.Document.Type))
                .GroupBy(link => link.Document.Type)
                .SelectParallelAsync(docsByType =>
                {
                    var type = docsByType.Key;
                    var baseIds = docsByType
                        .Select(link => link.Document.Id)
                        .Distinct()
                        .ToArray();

                    return docPaidSumFuncByTypeFunc(type, baseIds);
                });

            var paidSumsByIdsMap = paidSumTasks
                .SelectMany(paidSum => paidSum)
                .ToDictionary(
                    paidSum => paidSum.DocumentBaseId,
                    paidSum => paidSum.PaidSum);

            return documentLinksByPaymentMap
                .ToDictionary(
                x => x.Key,
                x => (IReadOnlyDictionary<long, decimal>) x.Value
                    .Where(link => restrictTypes.Contains(link.Document.Type))
                    .GroupBy(link => link.Document.Id) // тут может прилететь дубль связи по документу
                    .ToDictionary(
                        link => link.Key,
                        link =>
                        {
                            var documentLink = link.First();
                            var documentPaidSum = paidSumsByIdsMap.GetValueOrDefault(link.Key);
                            return documentPaidSum - documentLink.Sum;
                        }));
        }

        private async Task<IReadOnlyCollection<PaidSumDto>> GetSalesDocPaidSumFuncByTypeAsync(
            LinkedDocumentType type,
            IReadOnlyCollection<long> documentBaseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            switch (type)
            {
                case LinkedDocumentType.Bill:
                    return await billApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.Waybill:
                    return await salesWaybillApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.Statement:
                    return await outgoingStatementClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.SalesUpd:
                    return await salesUpdApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.SalesCurrencyInvoice:
                    return await salesCurrencyInvoicesApiClient.GetPaidSumByBaseIdsAsync(documentBaseIds);
                default:
                    throw new ArgumentOutOfRangeException($"Not supported document type: {type}");
            }
        }

        private async Task<IReadOnlyCollection<PaidSumDto>> GetPurchaseDocPaidSumFuncByTypeAsync(
            LinkedDocumentType type,
            IReadOnlyCollection<long> documentBaseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            switch (type)
            {
                case LinkedDocumentType.Waybill:
                    return await purchasesWaybillApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.Statement:
                    return await incomingStatementClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.Upd:
                    return await purchasesUpdApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                case LinkedDocumentType.InventoryCard:
                    var inventoryCardsPaidSums = await inventoryCardApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
                    return inventoryCardsPaidSums.Select(r => new PaidSumDto
                    {
                        DocumentBaseId = r.DocumentBaseId,
                        PaidSum = r.PaidSum
                    }).ToArray();
                case LinkedDocumentType.ReceiptStatement:
                    var receiptStatementPaidSums = await GetPaidSumForReceiptStatement(documentBaseIds);
                    return receiptStatementPaidSums.Select(r => new PaidSumDto
                    {
                        DocumentBaseId = r.Key,
                        PaidSum = r.Value
                    }).ToArray();
                case LinkedDocumentType.PurchaseCurrencyInvoice:
                    var currencyInvoicesPaidSums = await purchasesCurrencyInvoicesApiClient.GetPaidSumByBaseIdsAsync(documentBaseIds);
                    return currencyInvoicesPaidSums.Select(dto => new PaidSumDto
                    {
                        DocumentBaseId = dto.DocumentBaseId,
                        PaidSum = dto.CurrencyPaidSum
                    }).ToArray();
                default:
                    throw new ArgumentOutOfRangeException($"Not supported document type: {type}");
            }
        }

        public async Task<Dictionary<long, decimal>> GetPaidSumsForMediationFeeAsync(long paymentBaseId, IReadOnlyCollection<long> documentBaseIds, IReadOnlyCollection<LinkedDocumentType> types)
        {
            if (documentBaseIds.Count == 0)
            {
                return new Dictionary<long, decimal>();
            }

            var context = contextAccessor.ExecutionInfoContext;
            var paidSums = new List<PaidSumDto>(documentBaseIds.Count);
            paidSums.AddRange(await billApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds).ConfigureAwait(false));
            paidSums.AddRange(await purchasesWaybillApiClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds).ConfigureAwait(false));
            paidSums.AddRange(await incomingStatementClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds).ConfigureAwait(false));
            paidSums.AddRange(await middlemanReportClient.GetPaidSumByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds).ConfigureAwait(false));
            var paidSumsByIds = paidSums.ToDictionary(x => x.DocumentBaseId, x => x.PaidSum);

            if (paymentBaseId > 0)
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(paymentBaseId);
                foreach (var link in links)
                {
                    if (paidSumsByIds.ContainsKey(link.Document.Id))
                    {
                        paidSumsByIds[link.Document.Id] -= link.Sum;
                    }
                }
            }

            return paidSumsByIds;
        }

        private async Task<Dictionary<long, decimal>> GetPaidSumForReceiptStatement(IReadOnlyCollection<long> documentBaseIds)
        {
            var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseIds);
            return links.ToDictionary(x => x.Key, MapLinksSum);
        }

        private static decimal MapLinksSum(KeyValuePair<long, LinkWithDocument[]> links)
        {
            return links.Value
                .Where(x => x.Document.Type == LinkedDocumentType.PaymentOrder ||
                            x.Document.Type == LinkedDocumentType.OutcomingCashOrder)
                .Sum(l => l.Sum);
        }
    }
}
