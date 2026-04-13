using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Abstractions.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.AccPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.TaxPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using LinkedDocumentType = Moedelo.LinkedDocuments.Enums.LinkedDocumentType;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerProvider))]
    internal class PaymentFromCustomerProvider : IPaymentFromCustomerProvider
    {
        private readonly PaymentFromCustomerApiClient paymentFromCustomerApiClient;
        private readonly BaseDocumentReader baseDocumentReader;
        private readonly SalesWaybillReader waybillReader;
        private readonly SalesStatementReader statementReader;
        private readonly SalesUpdReader updReader;
        private readonly KontragentReader kontragentReader;
        private readonly ContractReader contractReader;
        private readonly PaymentForDocumentsCreator accountingStatementsCreator;
        private readonly PaymentLinksCreator linksCreator;
        private readonly BillStatusUpdater billStatusUpdater;
        private readonly PaymentFromCustomerAccPostingsProvider accPostingsProvider;
        private readonly PaymentFromCustomerTaxPostingsProvider taxPostingsProvider;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;
        private readonly IPaymentFromCustomerUnprovider unprovider;
        private readonly LinksReader linksReader;

        public PaymentFromCustomerProvider(
            PaymentFromCustomerApiClient paymentFromCustomerApiClient,
            BaseDocumentReader baseDocumentReader,
            SalesWaybillReader waybillReader,
            SalesStatementReader statementReader,
            SalesUpdReader updReader,
            KontragentReader kontragentReader,
            ContractReader contractReader,
            PaymentForDocumentsCreator accountingStatementsCreator,
            PaymentLinksCreator linksCreator,
            BillStatusUpdater billStatusUpdater,
            PaymentFromCustomerAccPostingsProvider accPostingsProvider,
            PaymentFromCustomerTaxPostingsProvider taxPostingsProvider, 
            PaymentOrderProvidingStateSetter providingStateSetter,
            IPaymentFromCustomerUnprovider unprovider,
            LinksReader linksReader)
        {
            this.paymentFromCustomerApiClient = paymentFromCustomerApiClient;
            this.baseDocumentReader = baseDocumentReader;
            this.waybillReader = waybillReader;
            this.statementReader = statementReader;
            this.updReader = updReader;
            this.kontragentReader = kontragentReader;
            this.contractReader = contractReader;
            this.accountingStatementsCreator = accountingStatementsCreator;
            this.linksCreator = linksCreator;
            this.billStatusUpdater = billStatusUpdater;
            this.accPostingsProvider = accPostingsProvider;
            this.taxPostingsProvider = taxPostingsProvider;
            this.providingStateSetter = providingStateSetter;
            this.unprovider = unprovider;
            this.linksReader = linksReader;
        }

        public async Task ProvideAsync(PaymentFromCustomerProvideRequest request)
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

            var isPaymentExists = await paymentFromCustomerApiClient.IsExistsAsync(request.DocumentBaseId);
            if (isPaymentExists == false)
            {
                // do nothing
                await providingStateSetter.UnsetStateAsync(request.ProvidingStateId);
                return;
            }

            var billsBaseIds = request.BillLinks.Select(x => x.BillBaseId).ToArray();
            var documentsBaseIds = request.DocumentLinks.Select(x => x.DocumentBaseId).ToArray();
            var invoicesBaseIds = request.InvoiceLinks.Select(x => x.InvoiceBaseId).ToArray();
            var baseIds = billsBaseIds.Concat(documentsBaseIds).Concat(invoicesBaseIds).Distinct().ToArray();
            // todo: чтение всех связанных сущностей желательно перенести на реплику 
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(baseIds);
            var baseDocumentsById = baseDocuments.ToDictionary(x => x.Id);

            // fixme
            request.BillLinks = request.BillLinks
                .Where(x => baseDocumentsById.ContainsKey(x.BillBaseId))
                .GroupBy(x => x.BillBaseId)
                .Select(x => x.First())
                .ToArray();

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
                : Task.FromResult(Array.Empty<SalesWaybill>());
            var statementsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.Statement, out var statementBaseIds)
                ? statementReader.GetByBaseIdsAsync(statementBaseIds)
                : Task.FromResult(Array.Empty<SalesStatement>());
            var updsTask = baseDocumentsByType.TryGetValue(LinkedDocumentType.SalesUpd, out var updBaseIds)
                ? updReader.GetByBaseIdsAsync(updBaseIds)
                : Task.FromResult(Array.Empty<SalesUpd>());

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

            var linksCreationResponse = await CreateAccStatementsAndLinksAsync(request, waybillsTask.Result,
                statementsTask.Result, updsTask.Result, kontragent, contract, baseDocumentsById);

            await UpdateBillsStatusesAsync(request.BillLinks, linksCreationResponse.PreviousBillBaseIds);
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
            PaymentFromCustomerProvideRequest request,
            SalesWaybill[] waybills,
            SalesStatement[] statements,
            SalesUpd[] upds,
            Kontragent kontragent,
            Contract contract,
            LinkWithDocument[] existentLinks)
        {
            var createRequest = new IncomingPaymentForDocumentsCreateRequest
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
                ExistentLinks = existentLinks
            };
            return accountingStatementsCreator.GenerateForIncomingPaymentAsync(createRequest);
        }

        private async Task<PaymentLinksCreationResponse> CreateAccStatementsAndLinksAsync(
            PaymentFromCustomerProvideRequest request,
            SalesWaybill[] waybills,
            SalesStatement[] statements,
            SalesUpd[] upds,
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
                BaseDocuments = baseDocumentsById,
                BillLinks = request.BillLinks,
                DocumentLinks = request.DocumentLinks,
                InvoiceLinks = request.InvoiceLinks,
                AccountingStatements = accountingStatements,
                ContractBaseId = request.ContractBaseId,
                ReserveSum = request.ReserveSum,
                EventType = request.EventType,
                CanHaveBills = true,
                ExistentLinks = existentLinks
            };

            return await linksCreator.OverwriteAsync(createLinksRequest);
        }

        private Task UpdateBillsStatusesAsync(IReadOnlyCollection<BillLink> newBillLinks, IReadOnlyCollection<long> previousBillBaseIds)
        {
            var newBillBaseIds = newBillLinks.Select(x => x.BillBaseId).Distinct().ToArray();
            return billStatusUpdater.UpdateStatusesAsync(newBillBaseIds, previousBillBaseIds);
        }

        private Task ProvideAccPostingsAsync(PaymentFromCustomerProvideRequest request, IReadOnlyCollection<BaseDocument> baseDocuments, Kontragent kontragent, Contract contract)
        {
            var accPostingGenerateRequest = new PaymentFromCustomerAccPostingsProvideRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Sum = request.Sum,
                IsMediation = request.IsMediation,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = kontragent,
                Contract = contract,
                BaseDocuments = baseDocuments,
                IsMainKontragent = request.IsMainKontragent,
                IsBadOperationState = request.IsBadOperationState,
                ProvideInAccounting = request.ProvideInAccounting,
            };
            return accPostingsProvider.ProvideAsync(accPostingGenerateRequest);
        }

        private Task ProvideTaxPostingsAsync(
            PaymentFromCustomerProvideRequest request,
            IReadOnlyCollection<BaseDocument> baseDocuments,
            IReadOnlyCollection<SalesWaybill> waybills,
            IReadOnlyCollection<SalesStatement> statements,
            IReadOnlyCollection<SalesUpd> upds,
            Kontragent kontragent)
        {
            var provideTaxPostingsRequest = new PaymentFromCustomerTaxPostingsProvideRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Kontragent = kontragent,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                MediationNdsSum = request.MediationNdsSum,
                IsMediation = request.IsMediation,
                MediationCommissionSum = request.MediationCommissionSum,
                BaseDocuments = baseDocuments,
                Waybills = waybills,
                Statements = statements,
                Upds = upds,
                DocumentLinks = request.DocumentLinks,
                TaxationSystemType = request.TaxationSystemType,
                IsManualTaxPostings = request.IsManualTaxPostings,
                IsBadOperationState = request.IsBadOperationState,
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
