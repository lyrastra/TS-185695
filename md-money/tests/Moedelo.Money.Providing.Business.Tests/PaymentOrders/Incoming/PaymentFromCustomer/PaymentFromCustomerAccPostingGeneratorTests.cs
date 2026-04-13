using System;
using FluentAssertions;
using Moedelo.AccPostings.Enums;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.AccPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;
using NUnit.Framework;

namespace Moedelo.Money.Providing.Business.Tests.PaymentOrders.Incoming.PaymentFromCustomer
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class PaymentFromCustomerAccPostingGeneratorTests
    {
        private PaymentFromCustomerAccPostingGenerateRequest businessModel;

        [SetUp]
        public void Setup()
        {
            businessModel = CreteBusinessModel();
        }

        [Test]
        public void Generate_ReturnExpectedPosting()
        {
            //businessModel.DocumentBaseId = IdGenerator.NextId();
            businessModel.Date = new DateTime(2019, 04, 29);
            businessModel.Sum = 55.22m;
            businessModel.IsMainKontragent = true;

            var actual = PaymentFromCustomerAccPostingsGenerator.Generate(businessModel);
            var expected = new AccountingPosting
            {
                Date = businessModel.Date,
                Sum = businessModel.Sum,
                DebitCode = SyntheticAccountCode._51_01,
                DebitSubcontos = new[]
                {
                    new Subconto
                    {
                        Id = businessModel.SettlementAccount.SubcontoId,
                        Type = SubcontoType.SettlementAccount
                    }
                },
                CreditCode = SyntheticAccountCode._62_02,
                CreditSubcontos = new[]
                {
                    new Subconto
                    {
                        Id = businessModel.Kontragent.SubcontoId,
                        Type = SubcontoType.Kontragent
                    },
                    new Subconto
                    {
                        Id = businessModel.Contract.SubcontoId,
                        Type = SubcontoType.Contract
                    }
                },
                OperationType = OperationType.PaymentOrderIncomingPaymentForGoods,
                Description = "Оплата",
                IsManual = false
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Generate_ReturnExpectedPosting_IfKontragentAccountCodeIsOther()
        {
            businessModel.IsMainKontragent = false;
            businessModel.Documents = new[]
            {
                CreteDocument(LinkedDocumentType.Waybill, "5к", new DateTime(2019, 04, 30)),
                CreteDocument(LinkedDocumentType.Waybill, "5у", new DateTime(2019, 04, 29)),
                CreteDocument(LinkedDocumentType.SalesUpd, "3", new DateTime(2019, 04, 12)),
                CreteDocument(LinkedDocumentType.Statement, "1", new DateTime(2019, 04, 02)),
            };

            var actual = PaymentFromCustomerAccPostingsGenerator.Generate(businessModel);

            actual.Description.Should().Be("Оплата товаров по накладным: №5к от 30.04.2019, №5у от 29.04.2019; по УПД: №3 от 12.04.2019; работ по актам: №1 от 02.04.2019");
        }

        [Test]
        public void Generate_ExpectedDescription_IfHasMultipleLinkedDocs()
        {
            businessModel.Documents = new[]
            {
                CreteDocument(LinkedDocumentType.Waybill, "5к", new DateTime(2019, 04, 30)),
                CreteDocument(LinkedDocumentType.Waybill, "5у", new DateTime(2019, 04, 29)),
                CreteDocument(LinkedDocumentType.SalesUpd, "3", new DateTime(2019, 04, 12)),
                CreteDocument(LinkedDocumentType.Statement, "1", new DateTime(2019, 04, 02)),
            };

            var actual = PaymentFromCustomerAccPostingsGenerator.Generate(businessModel);

            actual.Description.Should().Be("Оплата товаров по накладным: №5к от 30.04.2019, №5у от 29.04.2019; по УПД: №3 от 12.04.2019; работ по актам: №1 от 02.04.2019");
        }

        [Test]
        [TestCase(LinkedDocumentType.Waybill, "Оплата товаров по накладной №5к от 29.04.2019")]
        [TestCase(LinkedDocumentType.Statement, "Оплата работ по акту №5к от 29.04.2019")]
        [TestCase(LinkedDocumentType.SalesUpd, "Оплата по УПД №5к от 29.04.2019")]
        public void Generate_ExpectedDescription_IfHasOneLinkedDoc(LinkedDocumentType docType, string expectedPayment)
        {
            businessModel.Documents = new[]
            {
                CreteDocument(docType, "5к", new DateTime(2019, 04, 29))
            };

            var actual = PaymentFromCustomerAccPostingsGenerator.Generate(businessModel);

            actual.Description.Should().Be(expectedPayment);
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

        private static PaymentFromCustomerAccPostingGenerateRequest CreteBusinessModel()
        {
            return new PaymentFromCustomerAccPostingGenerateRequest
            {
                Date = new DateTime(2010, 04, 29),
                IsMainKontragent = true,
                Sum = 500,
                Kontragent = new Kontragent
                {
                    SubcontoId = 112233
                },
                Contract = new Contract
                {
                    SubcontoId = 223344,
                },
                Documents = Array.Empty<BaseDocument>(),
                SettlementAccount = new SettlementAccount
                {
                    SubcontoId = 334455
                }
            };
        }
    }
}