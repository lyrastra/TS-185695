using FluentAssertions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using NUnit.Framework;
using System;
using TaxationSystemType = Moedelo.Requisites.Enums.TaxationSystems.TaxationSystemType;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;

namespace Moedelo.Money.Providing.Business.Tests.PaymentOrders.Outgoing.PaymentToSupplier
{
    [TestFixture]
    public class PaymentToSupplierUsnProfitGeneratorTests
    {
        private const long DefaultOperationBaseId = 12;
        private const string DefaultOperationNumber = "12/2";
        private static readonly DateTime DefaultOperationDate = new DateTime(2018, 12, 15);

        private const long DefaultDocumentBaseId = 21;
        private const string DefaultDocumentNumber = "243";
        private static readonly DateTime DefaultDocumentDate = new DateTime(2018, 12, 14);

        [Test]
        [TestCase(TaxationSystemType.Usn, TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.UsnAndEnvd, TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.Osno, TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, Moedelo.TaxPostings.Enums.TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.Envd, TaxPostingStatus.NotTax)]
        public void Generate_ShouldReturnNoTax_IfNoDocuments(TaxationSystemType taxSystem, TaxPostingStatus status)
        {
            var model = new PaymentToSupplierUsnPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(status);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "за услуги по акту")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "за услуги по акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной")]
        public void Generate_ShouldReturnNoTax_IfSumIsPresentAndExistDocument(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = new PaymentToSupplierUsnPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m
            };
            CreateDocument(model, documentType, (Enums.TaxationSystemType)taxSystem, 500m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        private static void CreateDocument(PaymentToSupplierUsnPostingsBusinessModel model, LinkedDocumentType documentType, Enums.TaxationSystemType taxSystem, decimal sum)
        {
            model.DocumentLinks = new[]
            {
                new DocumentLink
                {
                    DocumentBaseId = DefaultDocumentBaseId,
                    Type = documentType
                }
            };
            switch (documentType)
            {
                case LinkedDocumentType.Waybill:
                    model.Waybills = new[]
                    {
                        new PurchasesWaybill
                        {
                            DocumentBaseId = DefaultDocumentBaseId,
                            Date = DefaultDocumentDate,
                            Number = DefaultDocumentNumber,
                            Sum = sum,
                            TaxationSystemType = taxSystem
                        }
                    };
                    break;
                case LinkedDocumentType.Statement:
                    model.Statements = new[]
                    {
                        new PurchasesStatement
                        {
                            DocumentBaseId = DefaultDocumentBaseId,
                            Date = DefaultDocumentDate,
                            Number = DefaultDocumentNumber,
                            Sum = sum,
                            TaxationSystemType = taxSystem
                        }
                    };
                    break;
                case LinkedDocumentType.Upd:
                    model.Upds = new[]
                    {
                        new PurchasesUpd
                        {
                            DocumentBaseId = DefaultDocumentBaseId,
                            Date = DefaultDocumentDate,
                            Number = DefaultDocumentNumber,
                            Sum = sum,
                            TaxationSystemType = taxSystem
                        }
                    };
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
