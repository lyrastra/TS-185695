using FluentAssertions;
using Moedelo.AccPostings.Enums;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.AccountingPostings.PaymentOrders.Outgoing;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;
using NUnit.Framework;
using System;
using System.Linq;

namespace Moedelo.Money.Providing.Business.Tests.PaymentOrders.Outgoing.PaymentToSupplier
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class PaymentToSupplierPostingGeneratorTests
    {
        private PaymentToSupplierAccPostingGenerateRequest request;

        [SetUp]
        public void Setup()
        {
            request = CreteBusinessModel();
        }

        [Test]
        public void Generate_ReturnExpectedPosting()
        {
            request.PaymentDate = new DateTime(2019, 04, 29);
            request.PaymentSum = 55.22m;
            request.IsMainKontragent = true;

            var actual = PaymentToSupplierAccPostingsGenerator.Generate(request).First();
            var expected = new AccountingPosting
            {
                Date = request.PaymentDate,
                Sum = request.PaymentSum,
                DebitCode = SyntheticAccountCode._60_02,
                DebitSubcontos = new[]
                {
                    new Subconto
                    {
                        Id = request.Kontragent.SubcontoId,
                        Type = SubcontoType.Kontragent
                    },
                    new Subconto
                    {
                        Id = request.Contract.SubcontoId,
                        Type = SubcontoType.Contract
                    }
                },
                CreditCode = SyntheticAccountCode._51_01,
                CreditSubcontos = new[]
                {
                    new Subconto
                    {
                        Id = request.SettlementAccount.SubcontoId,
                        Type = SubcontoType.SettlementAccount
                    }
                },
                OperationType = OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods,
                Description = "Оплата",
                IsManual = false
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Generate_ReturnExpectedPosting_IfKontragentAccountCodeIsOther()
        {
            request.IsMainKontragent = false;
            request.BaseDocuments = new[]
            {
                CreteDocument(LinkedDocumentType.Waybill, "5к", new DateTime(2019, 04, 30)),
                CreteDocument(LinkedDocumentType.Waybill, "5у", new DateTime(2019, 04, 29)),
                CreteDocument(LinkedDocumentType.Upd, "3", new DateTime(2019, 04, 12)),
                CreteDocument(LinkedDocumentType.Statement, "1", new DateTime(2019, 04, 02)),
                CreteDocument(LinkedDocumentType.InventoryCard, "6", new DateTime(2019, 04, 04)),
                CreteDocument(LinkedDocumentType.ReceiptStatement, "11", new DateTime(2019, 04, 04))
            };
            request.Contract = new Contract
            {
                Number = "22",
                Date = new DateTime(2019, 02, 11)
            };

            var actual = PaymentToSupplierAccPostingsGenerator.Generate(request).First();

            actual.Description.Should().Be("Оплата товаров по накладным: №5к от 30.04.2019, №5у от 29.04.2019; по УПД: №3 от 12.04.2019; работ по актам: №1 от 02.04.2019; аванса по договору аренды (лизинга) №22 от 11.02.2019");
        }

        [Test]
        public void Generate_ExpectedDescription_IfHasMultipleLinkedDocs()
        {
            request.BaseDocuments = new[]
            {
                CreteDocument(LinkedDocumentType.Waybill, "5к", new DateTime(2019, 04, 30)),
                CreteDocument(LinkedDocumentType.Waybill, "5у", new DateTime(2019, 04, 29)),
                CreteDocument(LinkedDocumentType.Upd, "3", new DateTime(2019, 04, 12)),
                CreteDocument(LinkedDocumentType.Statement, "1", new DateTime(2019, 04, 02)),
                CreteDocument(LinkedDocumentType.InventoryCard, "6", new DateTime(2019, 04, 04)),
                CreteDocument(LinkedDocumentType.ReceiptStatement, "11", new DateTime(2019, 04, 04))
            };
            request.Contract = new Contract
            {
                Number = "22",
                Date = new DateTime(2019, 02, 11)
            };

            var actual = PaymentToSupplierAccPostingsGenerator.Generate(request).First();

            actual.Description.Should().Be("Оплата товаров по накладным: №5к от 30.04.2019, №5у от 29.04.2019; по УПД: №3 от 12.04.2019; работ по актам: №1 от 02.04.2019; аванса по договору аренды (лизинга) №22 от 11.02.2019");
            //actual.LinkedDocuments.ElementAt(0).Postings.First().Description.Should().Be("Признание предоплаты оплатой товаров по накладной №5к от 30.04.2019");
            //actual.LinkedDocuments.ElementAt(1).Postings.First().Description.Should().Be("Признание предоплаты оплатой товаров по накладной №5у от 29.04.2019");
            //actual.LinkedDocuments.ElementAt(2).Postings.First().Description.Should().Be("Признание предоплаты оплатой по УПД №3 от 12.04.2019");
            //actual.LinkedDocuments.ElementAt(3).Postings.First().Description.Should().Be("Признание предоплаты оплатой работ по акту №1 от 02.04.2019");
        }

        [Test]
        [TestCase(LinkedDocumentType.Waybill, "Оплата товаров по накладной №5к от 29.04.2019", "Признание предоплаты оплатой товаров по накладной №5к от 29.04.2019")]
        [TestCase(LinkedDocumentType.Statement, "Оплата работ по акту №5к от 29.04.2019", "Признание предоплаты оплатой работ по акту №5к от 29.04.2019")]
        [TestCase(LinkedDocumentType.Upd, "Оплата по УПД №5к от 29.04.2019", "Признание предоплаты оплатой по УПД №5к от 29.04.2019")]
        public void Generate_ExpectedDescription_IfHasOneLinkedDoc(LinkedDocumentType docType, string expectedPayment, string expectedPrePayment)
        {
            request.BaseDocuments = new[]
            {
                CreteDocument(docType, "5к", new DateTime(2019, 04, 29))
            };

            var actual = PaymentToSupplierAccPostingsGenerator.Generate(request).First();

            actual.Description.Should().Be(expectedPayment);
            //if (expectedPrePayment != null)
            //{
            //    actual.LinkedDocuments.First().Postings.First().Description.Should().Be(expectedPrePayment);
            //}
        }

        [Test]
        public void Generate_ExpectedPosting_IfHasOneInventoryCard()
        {
            request.BaseDocuments = new[]
            {
                CreteDocument(LinkedDocumentType.InventoryCard, "6", new DateTime(2019, 04, 04))
            };

            var actual = PaymentToSupplierAccPostingsGenerator.Generate(request).First();

            actual.Description.Should().Be("Оплата");
            //actual.LinkedDocuments.Count().Should().Be(0);
        }

        [Test]
        public void Generate_ExpectedPosting_IfHasOneReceiptStatement()
        {
            request.BaseDocuments = new[]
            {
                CreteDocument(LinkedDocumentType.ReceiptStatement, "11", new DateTime(2019, 04, 30))
            };
            request.Contract = new Contract
            {
                Number = "22",
                Date = new DateTime(2019, 02, 11)
            };

            var actual = PaymentToSupplierAccPostingsGenerator.Generate(request).First();

            actual.Description.Should().Be("Оплата аванса по договору аренды (лизинга) №22 от 11.02.2019");
        }

        private static BaseDocument CreteDocument(LinkedDocumentType docType, string number, DateTime date)
        {
            return new BaseDocument
            {
                Date = date,
                Number = number,
                Type = docType,
                Sum = 500
            };
        }

        private static PaymentToSupplierAccPostingGenerateRequest CreteBusinessModel()
        {
            return new PaymentToSupplierAccPostingGenerateRequest
            {
                PaymentDate = new DateTime(2010, 04, 29),
                PaymentSum = 500,
                IsMainKontragent = true,
                Kontragent = new Kontragent
                {
                     SubcontoId = 112233
                },
                Contract = new Contract
                {
                    SubcontoId = 223344
                },
                BaseDocuments = Array.Empty<BaseDocument>(),
                SettlementAccount = new SettlementAccount
                {
                    SubcontoId = 334455
                }
            };
        }
    }
}