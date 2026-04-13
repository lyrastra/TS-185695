using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.Estate;
using Moedelo.Money.Providing.Business.Estate.Models;
using Moedelo.Money.Providing.Business.NdsRatePeriods;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings;
using Moedelo.Money.Providing.Business.Stock;
using Moedelo.Money.Providing.Business.Stock.Models;
using Moedelo.Money.Providing.Business.TaxationSystems;
using Moedelo.Money.Providing.Business.TaxPostings;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.TaxPostings
{
    [InjectAsSingleton(typeof(PaymentToSupplierTaxPostingsProvider))]
    class PaymentToSupplierTaxPostingsProvider
    {
        private readonly TaxationSystemReader taxationSystemReader;
        private readonly TaxPostingsRemover taxPostingsRemover;
        private readonly UsnPostingsSaver usnPostingsSaver;
        private readonly IBaseDocumentsCommandWriter commandWriter;
        private readonly InventoryCardReader inventoryCardReader;
        private readonly ReceiptStatementReader receiptStatementReader;
        private readonly StockProductReader stockProductReader;
        private readonly NdsRatePeriodsReader ndsRatePeriodsReader;

        public PaymentToSupplierTaxPostingsProvider(
            TaxationSystemReader taxationSystemReader,
            TaxPostingsRemover taxPostingsRemover,
            UsnPostingsSaver usnPostingsSaver,
            IBaseDocumentsCommandWriter commandWriter,
            InventoryCardReader inventoryCardReader,
            ReceiptStatementReader receiptStatementReader,
            StockProductReader stockProductReader,
            NdsRatePeriodsReader ndsRatePeriodsReader)
        {
            this.taxationSystemReader = taxationSystemReader;
            this.taxPostingsRemover = taxPostingsRemover;
            this.usnPostingsSaver = usnPostingsSaver;
            this.commandWriter = commandWriter;
            this.inventoryCardReader = inventoryCardReader;
            this.receiptStatementReader = receiptStatementReader;
            this.stockProductReader = stockProductReader;
            this.ndsRatePeriodsReader = ndsRatePeriodsReader;
        }

        public async Task ProvideAsync(PaymentToSupplierTaxPostingsProvideRequest request)
        {
            if (request.IsBadOperationState)
            {
                return;
            }

            if (request.IsPaid == false)
            {
                await taxPostingsRemover.DeleteAndUnsetTaxStatusAsync(request.DocumentBaseId);
                return;
            }

            var taxSystem = await taxationSystemReader.GetByYearAsync(request.Date.Year);
            if (taxSystem == null)
            {
                return;
            }

            if (taxSystem.IsUsn && taxSystem.IsUsnProfitAndOutgo)
            {
                var usnPostingsResponse = await GenerateUsnPostingsAsync(request, taxSystem);
                
                // Можно упростить, объединив команду на создание "ручного" НУ с обрабатываемым событием

                if (!request.IsManualTaxPostings) // автоматический НУ
                {
                    // 1. удаляем ВСЕ проводки с участием ПП (свои и "по связанным")
                    await usnPostingsSaver.DeleteRelatedAsync(request.DocumentBaseId);
                    // 2. создаем проводки, где ПП основной документ
                    await usnPostingsSaver.CreateAsync(usnPostingsResponse.Postings);
                } 
                else // ручной НУ
                {
                    // 1'. удаляем только проводки "по связанным" (где ПП не является главным)
                    await usnPostingsSaver.DeleteRelatedExcludingOwnAsync(request.DocumentBaseId);
                    // 2'. "ручная" проводка для ПП пересоздается по событию в отдельной очереди
                }

                // 3. создаем проводки "по связанным" (где ПП не является главным)
                if (usnPostingsResponse.LinkedDocuments.Count > 0)
                {
                    var postings = usnPostingsResponse
                        .LinkedDocuments.Select(ldp => ldp)
                        .SelectMany(x => x.Postings)
                        .ToArray();
                    await usnPostingsSaver.CreateAsync(postings);
                }

                // устанавливаем НУ-статусы платежа и связанных док-тов
                await SetTaxStatusesAsync(request, usnPostingsResponse);
                
                return;
            }

            await taxPostingsRemover.DeleteAndUnsetTaxStatusAsync(request.DocumentBaseId);
        }

        private async Task SetTaxStatusesAsync(PaymentToSupplierTaxPostingsProvideRequest request, UsnTaxPostingsResponse usnPostingsResponse)
        {
            // НУ статус платежа
            await commandWriter.WriteAsync(new SetTaxStatusCommand
            {
                Id = request.DocumentBaseId,
                TaxStatus = (TaxPostingStatus) usnPostingsResponse.TaxStatus
            });

            // НУ-статус для связанных документов, являющихся в проводках "основными"
            var linkedDocsWithSelfPostings = usnPostingsResponse.LinkedDocuments
                .Where(ld => ld.Postings.Any(p => p.DocumentId == ld.DocumentBaseId));
            foreach (var linkedDocPostings in linkedDocsWithSelfPostings)
            {
                await commandWriter.WriteAsync(new SetTaxStatusCommand
                {
                    Id = linkedDocPostings.DocumentBaseId,
                    TaxStatus = TaxPostingStatus.Yes
                });
            }
        }

        private async Task<UsnTaxPostingsResponse> GenerateUsnPostingsAsync(PaymentToSupplierTaxPostingsProvideRequest request, TaxationSystem taxSystem)
        {
            var baseDocumentsByType = request.BaseDocuments
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, v => v.Select(x => x.Id).ToArray());

            var inventoryCardsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.InventoryCard, out var inventoryCardBaseIds)
                ? inventoryCardReader.GetByBaseIdsAsync(inventoryCardBaseIds)
                : Task.FromResult(Array.Empty<InventoryCard>());
            var receiptStatementsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.ReceiptStatement, out var receiptStatementBaseIds)
                ? receiptStatementReader.GetByBaseIdsAsync(receiptStatementBaseIds)
                : Task.FromResult(Array.Empty<Estate.Models.ReceiptStatement>());
            var ndsRatePeriodsTask = ndsRatePeriodsReader.GetAsync();

            await Task.WhenAll(inventoryCardsTask, receiptStatementsTask, ndsRatePeriodsTask);

            var waybillStockProductIds = request.Waybills
                .SelectMany(x => x.Items)
                .Where(x => x.StockProductId.HasValue)
                .Select(x => x.StockProductId.Value)
                .ToArray();
            var updStockProductIds = request.Upds
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

            var waybillsFromFixedAssetInvestment = request.Waybills
                .Where(x => x.IsFromFixedAssetInvestment)
                .Select(x => x.DocumentBaseId)
                .ToArray();
            var statementsFromFixedAssetInvestment = request.Statements
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
                TaxationSystem = taxSystem.TaxationSystemType,
                NdsRatePeriods = ndsRatePeriodsTask.Result,
                IsUsnProfitAndOutgo = taxSystem.IsUsnProfitAndOutgo,
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Kontragent = request.Kontragent,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                Waybills = request.Waybills,
                Statements = request.Statements,
                Upds = request.Upds,
                InventoryCards = inventoryCardsTask.Result,
                ReceiptStatements = receiptStatementsTask.Result,
                DocumentLinks = request.DocumentLinks,
                StockProducts = stockProductsTask.Result,
                InventoryCardsFromFixedAssetInvestment = inventoryCardsFromFixedAssetInvestmentTask.Result
            };
            return PaymentToSupplierUsnPostingsGenerator.Generate(taxPostingsGenerateRequest);
        }
    }
}
