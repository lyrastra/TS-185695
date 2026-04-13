using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Incoming;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.AccPostings
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerAccPostingsFullGenerator))]
    internal class PaymentFromCustomerAccPostingsFullGenerator : IPaymentFromCustomerAccPostingsFullGenerator
    {
        private readonly BaseDocumentReader baseDocumentReader;
        private readonly SalesWaybillReader waybillReader;
        private readonly SalesStatementReader statementReader;
        private readonly SalesUpdReader updReader;
        private readonly SettlementAccountReader settlementAccountReader;
        private readonly KontragentReader kontragentReader;
        private readonly ContractReader contractReader;

        public PaymentFromCustomerAccPostingsFullGenerator(
            BaseDocumentReader baseDocumentReader,
            SalesWaybillReader waybillReader,
            SalesStatementReader statementReader,
            SalesUpdReader updReader,
            SettlementAccountReader settlementAccountReader,
            KontragentReader kontragentReader,
            ContractReader contractReader)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.waybillReader = waybillReader;
            this.statementReader = statementReader;
            this.updReader = updReader;
            this.settlementAccountReader = settlementAccountReader;
            this.kontragentReader = kontragentReader;
            this.contractReader = contractReader;
        }

        public async Task<AccountingPostingsResponse> GenerateAsync(PaymentFromCustomerAccPostingsFullGenerateRequest request)
        {
            var docBaseIds = request.DocumentLinks.Select(x => x.DocumentBaseId).ToArray();
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(docBaseIds);

            var baseDocumentsByType = baseDocuments.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, v => v.Select(x => x.Id).ToArray());
            var waybillsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.Waybill)
                ? waybillReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.Waybill])
                : Task.FromResult(Array.Empty<SalesWaybill>());
            var statementsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.Statement)
                ? statementReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.Statement])
                : Task.FromResult(Array.Empty<SalesStatement>());
            var updsTask = baseDocumentsByType.ContainsKey(LinkedDocumentType.SalesUpd)
                ? updReader.GetByBaseIdsAsync(baseDocumentsByType[LinkedDocumentType.SalesUpd])
                : Task.FromResult(Array.Empty<SalesUpd>());
            var settlementAccountTask = settlementAccountReader.GetByIdAsync(request.SettlementAccountId);
            var kontragentTask = kontragentReader.GetByIdAsync(request.KontragentId);
            var contract = request.ContractBaseId.HasValue
                ? await contractReader.GetByBaseIdAsync(request.ContractBaseId.Value) ?? await contractReader.GetMainAsync(request.KontragentId)
                : await contractReader.GetMainAsync(request.KontragentId);

            await Task.WhenAll(waybillsTask, statementsTask, updsTask, settlementAccountTask, kontragentTask);

            var accPosting = GenerateAccPostings(request, baseDocuments, settlementAccountTask.Result, kontragentTask.Result, contract);
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
                Postings = new[] { accPosting }
            };
        }

        private static AccountingPosting GenerateAccPostings(
            PaymentFromCustomerAccPostingsFullGenerateRequest request,
            BaseDocument[] baseDocuments,
            SettlementAccount settlementAccount,
            Kontragent kontragent,
            Contract contract)
        {
            var accPostingGenerateRequest = new PaymentFromCustomerAccPostingGenerateRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Sum = request.Sum,
                IsMediation = request.IsMediation,
                IsMainKontragent = request.IsMainKontragent,
                SettlementAccount = settlementAccount,
                Kontragent = kontragent,
                Contract = contract,
                Documents = baseDocuments,

            };
            return PaymentFromCustomerAccPostingsGenerator.Generate(accPostingGenerateRequest);
        }

        private static PaymentForDocumentsGenerateResult[] GenerateAccStatements(PaymentFromCustomerAccPostingsFullGenerateRequest request, Task<SalesWaybill[]> waybillsTask, Task<SalesStatement[]> statementsTask, Task<SalesUpd[]> updsTask, Task<Kontragent> kontragentTask, Contract contract)
        {
            var accStatementsGenerateRequest = new IncomingPaymentForDocumentsGenerateRequest
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
            return IncomingPaymentForDocumentsGenerator.Generate(accStatementsGenerateRequest);
        }
    }
}
