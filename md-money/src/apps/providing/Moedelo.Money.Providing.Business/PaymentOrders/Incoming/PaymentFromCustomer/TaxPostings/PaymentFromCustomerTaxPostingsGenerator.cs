using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Exceptions;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.LinkedDocuments;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.TaxationSystems;
using Moedelo.Requisites.Enums.TaxationSystems;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.TaxPostings
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerTaxPostingsGenerator))]
    internal class PaymentFromCustomerTaxPostingsGenerator : IPaymentFromCustomerTaxPostingsGenerator
    {
        private readonly TaxationSystemReader taxationSystemReader;
        private readonly FirmRequisitesReader requisitesReader;
        private readonly BaseDocumentReader baseDocumentReader;
        private readonly SalesWaybillReader waybillReader;
        private readonly SalesStatementReader statementReader;
        private readonly SalesUpdReader updReader;

        public PaymentFromCustomerTaxPostingsGenerator(
            TaxationSystemReader taxationSystemReader,
            FirmRequisitesReader requisitesReader,
            BaseDocumentReader baseDocumentReader,
            SalesWaybillReader waybillReader,
            SalesStatementReader statementReader,
            SalesUpdReader updReader)
        {
            this.taxationSystemReader = taxationSystemReader;
            this.requisitesReader = requisitesReader;
            this.baseDocumentReader = baseDocumentReader;
            this.waybillReader = waybillReader;
            this.statementReader = statementReader;
            this.updReader = updReader;
        }

        public async Task<ITaxPostingsResponse<ITaxPosting>> GenerateAsync(PaymentFromCustomerTaxPostingsGenerateRequest request)
        {
            var taxSystem = await taxationSystemReader.GetByYearAsync(request.Date.Year);
            if (taxSystem == null)
            {
                throw new TaxationSystemNotFoundException(request.Date.Year);
            }

            if (taxSystem.IsUsn)
            {
                var taxPostingsGenerateResult = await GetGenerationModel(request, taxSystem);
                return PaymentFromCustomerUsnPostingsGenerator.Generate(taxPostingsGenerateResult);
            }

            if (taxSystem.IsOsno && request.TaxationSystemType == TaxationSystemType.Patent)
            {
                var taxPostingsGenerateResult = await GetGenerationModel(request, taxSystem);
                return PaymentFromCustomerOsnoPatentPostingsGenerator.Generate(taxPostingsGenerateResult);
            }

            if (taxSystem.IsOsno)
            {
                if (!await requisitesReader.IsOooAsync())
                {
                    return PaymentFromCustomerIpOsnoPostingsGenerator.Generate(request);
                }

                return new OsnoTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            return new TaxPostingsResponse
            {
                TaxationSystemType = taxSystem.TaxationSystemType
            };
        }

        private async Task<PaymentFromCustomerPostingsBusinessModel> GetGenerationModel(
            PaymentFromCustomerTaxPostingsGenerateRequest request, TaxationSystem taxSystem)
        {
            var taxationSystemType = request.TaxationSystemType ?? taxSystem.DefaultTaxationSystemType;

            var docBaseIds = request.DocumentLinks.Select(x => x.DocumentBaseId).ToArray();
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(docBaseIds);

            var baseDocumentsById = baseDocuments.ToDictionary(x => x.Id);
            foreach (var documentLink in request.DocumentLinks)
            {
                SetLinkType(documentLink, baseDocumentsById);
            }

            var baseDocumentsByType = baseDocuments
                .GroupBy(x => x.Type)
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

            await Task.WhenAll(waybillsTask, statementsTask, updsTask);

            return new PaymentFromCustomerPostingsBusinessModel
            {
                DocumentBaseId = request.DocumentBaseId,
                TaxationSystem = taxationSystemType,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                KontragentId = request.KontragentId,
                KontragentName = request.KontragentName,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                MediationNdsSum = request.MediationNdsSum,
                IsMediation = request.IsMediation,
                MediationCommissionSum = request.MediationCommissionSum,
                DocumentLinks = request.DocumentLinks,
                Waybills = waybillsTask.Result,
                Statements = statementsTask.Result,
                Upds = updsTask.Result,
            };
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
