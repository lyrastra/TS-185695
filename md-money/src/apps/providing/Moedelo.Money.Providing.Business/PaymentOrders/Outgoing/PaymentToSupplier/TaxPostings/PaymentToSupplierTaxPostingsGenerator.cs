using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Exceptions;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.Documents;
using Moedelo.Money.Providing.Business.Estate;
using Moedelo.Money.Providing.Business.Estate.Models;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.Stock;
using Moedelo.Money.Providing.Business.Stock.Models;
using Moedelo.Money.Providing.Business.TaxationSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.FirmRequisites;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.NdsRatePeriods;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings
{
    [InjectAsSingleton(typeof(IPaymentToSupplierTaxPostingsGenerator))]
    class PaymentToSupplierTaxPostingsGenerator : IPaymentToSupplierTaxPostingsGenerator
    {
        private readonly TaxationSystemReader taxationSystemReader;
        private readonly BaseDocumentReader baseDocumentReader;
        private readonly KontragentReader kontragentReader;
        private readonly PurchasesWaybillReader waybillReader;
        private readonly PurchasesStatementReader statementReader;
        private readonly PurchasesUpdReader updReader;
        private readonly InventoryCardReader inventoryCardReader;
        private readonly ReceiptStatementReader receiptStatementReader;
        private readonly StockProductReader stockProductReader;
        private readonly FirmRequisitesReader requisitesReader;
        private readonly NdsRatePeriodsReader ndsRatePeriodsReader;

        public PaymentToSupplierTaxPostingsGenerator(
            TaxationSystemReader taxationSystemReader,
            BaseDocumentReader baseDocumentReader,
            KontragentReader kontragentReader,
            PurchasesWaybillReader waybillReader,
            PurchasesStatementReader statementReader,
            PurchasesUpdReader updReader,
            InventoryCardReader inventoryCardReader,
            ReceiptStatementReader receiptStatementReader,
            StockProductReader stockProductReader,
            FirmRequisitesReader requisitesReader,
            NdsRatePeriodsReader ndsRatePeriodsReader)
        {
            this.taxationSystemReader = taxationSystemReader;
            this.baseDocumentReader = baseDocumentReader;
            this.kontragentReader = kontragentReader;
            this.waybillReader = waybillReader;
            this.statementReader = statementReader;
            this.updReader = updReader;
            this.inventoryCardReader = inventoryCardReader;
            this.receiptStatementReader = receiptStatementReader;
            this.stockProductReader = stockProductReader;
            this.requisitesReader = requisitesReader;
            this.ndsRatePeriodsReader = ndsRatePeriodsReader;
        }

        public async Task<ITaxPostingsResponse<ITaxPosting>> GenerateAsync(PaymentToSupplierTaxPostingsGenerateRequest request)
        {
            var taxSystem = await taxationSystemReader.GetByYearAsync(request.Date.Year);
            if (taxSystem == null)
            {
                throw new TaxationSystemNotFoundException(request.Date.Year);
            }

            if (taxSystem.IsUsn)
            {
                return await GenerateUsnPostingsAsync(request, taxSystem);
            }

            if (taxSystem.IsOsno)
            {
                if (!await requisitesReader.IsOooAsync())
                {
                    return PaymentToSupplierIpOsnoPostingsGenerator.Generate(request);
                }
                
                return new OsnoTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            return new TaxPostingsResponse
            {
                TaxationSystemType = taxSystem.TaxationSystemType
            };
        }

        private async Task<UsnTaxPostingsResponse> GenerateUsnPostingsAsync(PaymentToSupplierTaxPostingsGenerateRequest request, TaxationSystem taxSystem)
        {
            var docBaseIds = request.DocumentLinks.Select(x => x.DocumentBaseId).ToArray();
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(docBaseIds);

            var baseDocumentsById = baseDocuments.ToDictionary(x => x.Id);
            foreach (var documentLink in request.DocumentLinks)
            {
                SetLinkType(documentLink, baseDocumentsById);
            }

            var baseDocumentsByType = baseDocuments.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, v => v.Select(x => x.Id).ToArray());
            var waybillsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.Waybill)
                ? waybillReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.Waybill])
                : Task.FromResult(Array.Empty<PurchasesWaybill>());
            var statementsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.Statement)
                ? statementReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.Statement])
                : Task.FromResult(Array.Empty<PurchasesStatement>());
            var updsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.Upd)
                ? updReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.Upd])
                : Task.FromResult(Array.Empty<PurchasesUpd>());
            var kontragentTask = kontragentReader.GetByIdAsync(request.KontragentId);
            var ndsRatePeriodsTask = ndsRatePeriodsReader.GetAsync();

            await Task.WhenAll(waybillsTask, statementsTask, updsTask, kontragentTask, ndsRatePeriodsTask);
            
            if (kontragentTask.Result == null)
            {
                throw new ArgumentNullException($"Kontragent = null. KontragentId = {request.KontragentId} Похоже удален!");
            }

            var inventoryCardsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.InventoryCard)
                ? inventoryCardReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.InventoryCard])
                : Task.FromResult(Array.Empty<InventoryCard>());
            var receiptStatementsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.ReceiptStatement)
                ? receiptStatementReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.ReceiptStatement])
                : Task.FromResult(Array.Empty<Estate.Models.ReceiptStatement>());

            await Task.WhenAll(inventoryCardsTask, receiptStatementsTask);

            var waybillStockProductIds = waybillsTask.Result
                .SelectMany(x => x.Items)
                .Where(x => x.StockProductId.HasValue)
                .Select(x => x.StockProductId.Value)
                .ToArray();
            var updStockProductIds = updsTask.Result
                .SelectMany(x => x.Items)
                .Where(x => x.StockProductId.HasValue)
                .Select(x => x.StockProductId.Value)
                .ToArray();
            var stockProductIds = waybillStockProductIds.Concat(updStockProductIds)
                .Distinct()
                .ToArray();
            var stockProductsTask = stockProductIds.Length > 0
                ? stockProductReader.GetByIdsAsync(stockProductIds)
                : Task.FromResult(Array.Empty<StockProduct>());

            var waybillsFromFixedAssetInvestment = waybillsTask.Result
                .Where(x => x.IsFromFixedAssetInvestment)
                .Select(x => x.DocumentBaseId)
                .ToArray();
            var statementsFromFixedAssetInvestment = statementsTask.Result
                .Where(x => x.IsFromFixedAssetInvestment)
                .Select(x => x.DocumentBaseId)
                .ToArray();
            var primaryDocumentFromFixedAssetInvestmentBaseIds = waybillsFromFixedAssetInvestment.Concat(statementsFromFixedAssetInvestment)
                .Distinct()
                .ToArray();
            var inventoryCardsFromFixedAssetInvestmentTask = primaryDocumentFromFixedAssetInvestmentBaseIds.Length > 0
                ? inventoryCardReader.GetByPrimaryDocumentBaseIdsAsync(primaryDocumentFromFixedAssetInvestmentBaseIds)
                : Task.FromResult(new Dictionary<long, InventoryCard>());

            await Task.WhenAll(stockProductsTask, inventoryCardsFromFixedAssetInvestmentTask);

            var taxPostingsGenerateRequest = new PaymentToSupplierUsnPostingsBusinessModel
            {
                DocumentBaseId = request.DocumentBaseId,
                TaxationSystem = taxSystem.TaxationSystemType,
                NdsRatePeriods = ndsRatePeriodsTask.Result,
                IsUsnProfitAndOutgo = taxSystem.IsUsnProfitAndOutgo,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Kontragent = kontragentTask.Result,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                Waybills = waybillsTask.Result,
                Statements = statementsTask.Result,
                Upds = updsTask.Result,
                InventoryCards = inventoryCardsTask.Result,
                ReceiptStatements = receiptStatementsTask.Result,
                DocumentLinks = request.DocumentLinks,
                StockProducts = stockProductsTask.Result,
                InventoryCardsFromFixedAssetInvestment = inventoryCardsFromFixedAssetInvestmentTask.Result
            };
            return PaymentToSupplierUsnPostingsGenerator.Generate(taxPostingsGenerateRequest);
        }

        private static void SetLinkType(DocumentLink link, IDictionary<long, BaseDocument> baseDocumentDict)
        {
            if (baseDocumentDict.TryGetValue(link.DocumentBaseId, out var baseDocument))
            {
                link.Type = baseDocument.Type;
            }
        }
    }
}