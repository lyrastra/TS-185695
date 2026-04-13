using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Abstractions.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Documents;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.TaxPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using LinkedDocumentType = Moedelo.LinkedDocuments.Enums.LinkedDocumentType;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierProvider))]
    class PaymentToSupplierProvider : IPaymentToSupplierProvider
    {
        private readonly PaymentToSupplierApiClient paymentToSupplierApiClient;
        private readonly BaseDocumentReader baseDocumentReader;
        private readonly PurchasesWaybillReader waybillReader;
        private readonly PurchasesStatementReader statementReader;
        private readonly PurchasesUpdReader updReader;
        private readonly KontragentReader kontragentReader;
        private readonly ContractReader contractReader;
        private readonly PaymentForDocumentsCreator accountingStatementsCreator;
        private readonly PaymentLinksCreator linksCreator;
        private readonly PaymentToSupplierAccPostingsProvider accPostingsProvider;
        private readonly PaymentToSupplierTaxPostingsProvider taxPostingsProvider;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;
        private readonly IPaymentToSupplierUnprovider unprovider;
        private readonly LinksReader linksReader;

        public PaymentToSupplierProvider(
            PaymentToSupplierApiClient paymentToSupplierApiClient,
            BaseDocumentReader baseDocumentReader,
            PurchasesWaybillReader waybillReader,
            PurchasesStatementReader statementReader,
            PurchasesUpdReader updReader,
            KontragentReader kontragentReader,
            ContractReader contractReader,
            PaymentForDocumentsCreator accountingStatementsCreator,
            PaymentLinksCreator linksCreator,
            PaymentToSupplierAccPostingsProvider accPostingsProvider,
            PaymentToSupplierTaxPostingsProvider taxPostingsProvider,
            PaymentOrderProvidingStateSetter providingStateSetter,
            IPaymentToSupplierUnprovider unprovider,
            LinksReader linksReader)
        {
            this.paymentToSupplierApiClient = paymentToSupplierApiClient;
            this.baseDocumentReader = baseDocumentReader;
            this.waybillReader = waybillReader;
            this.statementReader = statementReader;
            this.updReader = updReader;
            this.kontragentReader = kontragentReader;
            this.contractReader = contractReader;
            this.accountingStatementsCreator = accountingStatementsCreator;
            this.linksCreator = linksCreator;
            this.accPostingsProvider = accPostingsProvider;
            this.taxPostingsProvider = taxPostingsProvider;
            this.providingStateSetter = providingStateSetter;
            this.unprovider = unprovider;
            this.linksReader = linksReader;
        }

        public async Task ProvideAsync(PaymentToSupplierProvideRequest request)
        {
            if (request.IsBadOperationState)
            {
                if (request.EventType == HandleEventType.Updated)
                {
                    await unprovider.UnprovideAsync(request.DocumentBaseId);
                }
                else
                {
                    await providingStateSetter.UnsetStateAsync(request.ProvidingStateId);
                }

                return;
            }

            var isPaymentExists = await paymentToSupplierApiClient.IsExistsAsync(request.DocumentBaseId);
            if (isPaymentExists == false)
            {
                // do nothing
                await providingStateSetter.UnsetStateAsync(request.ProvidingStateId);
                return;
            }

            var documentsBaseIds = request.DocumentLinks.Select(x => x.DocumentBaseId).ToArray();
            var invoicesBaseIds = request.InvoiceLinks.Select(x => x.InvoiceBaseId).ToArray();
            var baseIds = documentsBaseIds.Concat(invoicesBaseIds).Distinct().ToArray();
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(baseIds);
            var baseDocumentsById = baseDocuments.ToDictionary(x => x.Id);

            // fixme
            request.DocumentLinks = request.DocumentLinks
                .Where(x => baseDocumentsById.ContainsKey(x.DocumentBaseId))
                .GroupBy(x => x.DocumentBaseId)
                .Select(x => x.First())
                .ToArray();

            request.InvoiceLinks = request.InvoiceLinks
                .Where(x => baseDocumentsById.ContainsKey(x.InvoiceBaseId))
                .GroupBy(x => x.InvoiceBaseId)
                .Select(x => x.First())
                .ToArray();

            foreach (var documentLink in request.DocumentLinks)
            {
                SetLinkType(documentLink, baseDocumentsById);
            }

            var kontragent = await kontragentReader.GetByIdAsync(request.KontragentId);
            if (kontragent == null)
            {
                // не можем завершить обработку (нужно маркировать операцию некорректной?)
                await providingStateSetter.UnsetStateAsync(request.ProvidingStateId);
                return;
            }
            
            var baseDocumentsByType = baseDocuments.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, v => v.Select(x => x.Id).ToArray());
            var waybillsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.Waybill, out var waybillBaseIds)
                ? waybillReader.GetByBaseIdsAsync(waybillBaseIds)
                : Task.FromResult(Array.Empty<PurchasesWaybill>());
            var statementsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.Statement, out var statementBaseIds)
                ? statementReader.GetByBaseIdsAsync(statementBaseIds)
                : Task.FromResult(Array.Empty<PurchasesStatement>());
            var updsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.Upd, out var updBaseIds)
                ? updReader.GetByBaseIdsAsync(updBaseIds)
                : Task.FromResult(Array.Empty<PurchasesUpd>());

            var contractResult = await contractReader.TryGetAsync(request.ContractBaseId, request.KontragentId);
            if (!contractResult.IsSuccess)
            {
                // Для этой операции считаем ошибку чтения договора неретрабельной
                // и явно снимаем state, чтобы документ не завис в providing.
                await providingStateSetter.UnsetStateAsync(request.ProvidingStateId);
                return;
            }
            var contract = contractResult.Contract;

            await Task.WhenAll(waybillsTask, statementsTask, updsTask);

            await CreateAccStatementsAndLinksAsync(request, waybillsTask.Result, statementsTask.Result, updsTask.Result,
                kontragent, contract, baseDocumentsById);
            await ProvideAccPostingsAsync(request, baseDocuments, kontragent, contract);
            await ProvideTaxPostingsAsync(request, baseDocuments, waybillsTask.Result, statementsTask.Result,
                updsTask.Result, kontragent);

            await providingStateSetter.UnsetStateAsync(request.ProvidingStateId);
        }

        public Task UpdateReserveAsync(SetReserveRequest request)
        {
            return linksCreator.UpdateReserveAsync(request);
        }

        private Task<PaymentForDocumentCreateResponse[]> CreateAccountingStatementsAsync(
            PaymentToSupplierProvideRequest request, PurchasesWaybill[] waybills, PurchasesStatement[] statements,
            PurchasesUpd[] upds, Kontragent kontragent, Contract contract, LinkWithDocument[] existentLinks)
        {
            var createRequest = new OutgoingPaymentForDocumentsCreateRequest
            {
                PaymentBaseId = request.DocumentBaseId,
                PaymentDate = request.Date,
                IsMainKontragent = request.IsMainKontragent,
                Links = request.DocumentLinks,
                Waybills = waybills,
                Statements = statements,
                Upds = upds,
                Kontragent = kontragent,
                Contract = contract,
                IsBadOperationState = request.IsBadOperationState,
                IsPaid = request.IsPaid,
                ExistentLinks = existentLinks
            };
            return accountingStatementsCreator.GenerateForOutgoingPaymentAsync(createRequest);
        }

        private async Task CreateAccStatementsAndLinksAsync(
            PaymentToSupplierProvideRequest request,
            PurchasesWaybill[] waybills,
            PurchasesStatement[] statements,
            PurchasesUpd[] upds,
            Kontragent kontragent,
            Contract contract,
            IDictionary<long, BaseDocument> baseDocumentsById)
        {
            var existentLinks = await linksReader.GetByIdAsync(request.DocumentBaseId);

            var accountingStatements = await CreateAccountingStatementsAsync(
                request,
                waybills,
                statements,
                upds,
                kontragent,
                contract,
                existentLinks);
            
            var createLinksRequest = new PaymentLinksCreateRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                DocumentLinks = request.DocumentLinks,
                InvoiceLinks = request.InvoiceLinks,
                BaseDocuments = baseDocumentsById,
                AccountingStatements = accountingStatements,
                ContractBaseId = request.ContractBaseId,
                ReserveSum = request.ReserveSum,
                CanHaveBills = false,
                EventType = request.EventType,
                ExistentLinks = existentLinks
            };
            
            try
            {
                await linksCreator.OverwriteAsync(createLinksRequest);
            }
            catch (Exception)
            {
                // в случае ошибки при создании связей остаются не связанные ни с чем бухсправки
                await accountingStatementsCreator.UndoAsync(accountingStatements);
                throw;
            }
        }

        private Task ProvideAccPostingsAsync(PaymentToSupplierProvideRequest request, IReadOnlyCollection<BaseDocument> baseDocuments, Kontragent kontragent, Contract contract)
        {
            var accPostingGenerateRequest = new PaymentToSupplierAccPostingsProvideRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                IsMainKontragent = request.IsMainKontragent,
                BaseDocuments = baseDocuments,
                Kontragent = kontragent,
                Contract = contract,
                IsBadOperationState = request.IsBadOperationState,
                ProvideInAccounting = request.ProvideInAccounting
            };
            return accPostingsProvider.ProvideAsync(accPostingGenerateRequest);
        }

        private Task ProvideTaxPostingsAsync(PaymentToSupplierProvideRequest request, IReadOnlyCollection<BaseDocument> baseDocuments, PurchasesWaybill[] waybills, PurchasesStatement[] statements, PurchasesUpd[] upds, Kontragent kontragent)
        {
            var provideTaxPostingsRequest = new PaymentToSupplierTaxPostingsProvideRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Kontragent = kontragent,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                BaseDocuments = baseDocuments,
                Waybills = waybills,
                Statements = statements,
                Upds = upds,
                DocumentLinks = request.DocumentLinks,
                IsBadOperationState = request.IsBadOperationState,
                IsPaid = request.IsPaid,
                IsManualTaxPostings = request.IsManualTaxPostings
            };
            return taxPostingsProvider.ProvideAsync(provideTaxPostingsRequest);
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
