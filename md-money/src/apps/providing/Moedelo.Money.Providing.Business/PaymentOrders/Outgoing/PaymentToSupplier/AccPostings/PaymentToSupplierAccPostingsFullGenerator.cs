using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Outgoing;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Documents;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.StockVisibility;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.Subcontos;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.AccountingPostings.PaymentOrders.Outgoing
{
    [InjectAsSingleton(typeof(IPaymentToSupplierAccPostingsFullGenerator))]
    class PaymentToSupplierAccPostingsFullGenerator : IPaymentToSupplierAccPostingsFullGenerator
    {
        private readonly BaseDocumentReader baseDocumentReader;
        private readonly PurchasesWaybillReader waybillReader;
        private readonly PurchasesStatementReader statementReader;
        private readonly PurchasesUpdReader updReader;
        private readonly SettlementAccountReader settlementAccountReader;
        private readonly KontragentReader kontragentReader;
        private readonly ContractReader contractReader;
        private readonly PaymentToSupplierSubcontoReader subcontoReader;
        private readonly StockVisibilityInspector stockVisibilityInspector;

        public PaymentToSupplierAccPostingsFullGenerator(
            BaseDocumentReader baseDocumentReader,
            PurchasesWaybillReader waybillReader,
            PurchasesStatementReader statementReader,
            PurchasesUpdReader updReader,
            SettlementAccountReader settlementAccountReader,
            KontragentReader kontragentReader,
            ContractReader contractReader,
            PaymentToSupplierSubcontoReader subcontoReader,
            StockVisibilityInspector stockVisibilityInspector)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.waybillReader = waybillReader;
            this.statementReader = statementReader;
            this.updReader = updReader;
            this.settlementAccountReader = settlementAccountReader;
            this.kontragentReader = kontragentReader;
            this.contractReader = contractReader;
            this.subcontoReader = subcontoReader;
            this.stockVisibilityInspector = stockVisibilityInspector;
        }

        public async Task<AccountingPostingsResponse> GenerateAsync(PaymentToSupplierAccPostingsFullGenerateRequest request)
        {
            var docBaseIds = request.DocumentLinks.Select(x => x.DocumentBaseId).ToArray();
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(docBaseIds);

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
            var settlementAccountTask = settlementAccountReader.GetByIdAsync(request.SettlementAccountId);
            var kontragentTask = kontragentReader.GetByIdAsync(request.KontragentId);
            var contract = request.ContractBaseId.HasValue
                ? await contractReader.GetByBaseIdAsync(request.ContractBaseId.Value) ?? await contractReader.GetMainAsync(request.KontragentId)
                : await contractReader.GetMainAsync(request.KontragentId);

            await Task.WhenAll(waybillsTask, statementsTask, updsTask, settlementAccountTask, kontragentTask);
            
            if (kontragentTask.Result == null)
            {
                throw new ArgumentNullException($"Kontragent = null. KontragentId = {request.KontragentId} Похоже удален!");
            }

            var accPostings = await GenerateAccPostings(request, baseDocuments, settlementAccountTask, kontragentTask, contract);
            var accStatements = GenerateAccStatements(request, waybillsTask, statementsTask, updsTask, kontragentTask, contract);

            return new AccountingPostingsResponse
            {
                LinkedDocuments = accStatements.Select(x => new LinkedDocumentAccountingPostings
                {
                    Date = x.AccountingStatement.Date,
                    Number = x.AccountingStatement.Number,
                    Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.AccountingStatement,
                    Postings = new[] { x.AccoutingPosting }
                }).ToArray(),
                Postings = accPostings
            };
        }

        private async Task<IReadOnlyCollection<AccountingPosting>> GenerateAccPostings(PaymentToSupplierAccPostingsFullGenerateRequest request,
            BaseDocument[] baseDocuments, Task<SettlementAccount> settlementAccountTask,
            Task<Kontragent> kontragentTask, Contract contract)
        {
            var accPostingGenerateRequest = new PaymentToSupplierAccPostingGenerateRequest
            {
                PaymentBaseId = request.DocumentBaseId,
                PaymentDate = request.Date,
                PaymentSum = request.Sum,
                IsMainKontragent = request.IsMainKontragent,
                SettlementAccount = settlementAccountTask.Result,
                Kontragent = kontragentTask.Result,
                Contract = contract,
                BaseDocuments = baseDocuments,
                IsStockInvisible = await stockVisibilityInspector.IsStockInVisible(request.Date.Year)
            };
            await FillSubcontoAsync(accPostingGenerateRequest);
            return PaymentToSupplierAccPostingsGenerator.Generate(accPostingGenerateRequest);
        }

        private static PaymentForDocumentsGenerateResult[] GenerateAccStatements(
            PaymentToSupplierAccPostingsFullGenerateRequest request, Task<PurchasesWaybill[]> waybillsTask,
            Task<PurchasesStatement[]> statementsTask, Task<PurchasesUpd[]> updsTask, Task<Kontragent> kontragentTask,
            Contract contract)
        {
            var accStatementsGenerateRequest = new OutgoingPaymentForDocumentsGenerateRequest
            {
                PaymentDate = request.Date,
                IsMainKontragent = request.IsMainKontragent,
                Links = request.DocumentLinks,
                Waybills = waybillsTask.Result,
                Statements = statementsTask.Result,
                Upds = updsTask.Result,
                Kontragent = kontragentTask.Result,
                Contract = contract,
            };
            return OutgoingPaymentForDocumentsGenerator.Generate(accStatementsGenerateRequest);
        }

        private async Task FillSubcontoAsync(PaymentToSupplierAccPostingGenerateRequest request)
        {
            if (request.IsStockInvisible)
            {
                var costItemsSubcontoTask = subcontoReader.GetCostItemsSubcontoAsync();
                var divisionSubcontoTask = subcontoReader.GetDivisionSubcontoAsync();

                await Task.WhenAll(costItemsSubcontoTask, divisionSubcontoTask);
                request.CostItemsSubconto = costItemsSubcontoTask.Result;
                request.DivisionSubconto = divisionSubcontoTask.Result;
            }
        }
    }
}
