using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.Parsers.Klto1CParser.Tests.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Parsers.Klto1CParser.Tests.Tests
{
    [TestFixture]
    public class KlTo1CGeneratorTests
    {
        private readonly DateTime testDate = new DateTime(2018, 1, 1, 12, 43, 25);
        private const string SettlementAccount = "12345678901234567890";

        public KlTo1CGeneratorTests()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void ShouldGenerate()
        {
            var doc1 = CreateDoc("1");
            var doc2 = CreateDoc("2");
            var doc3 = CreateDoc("3");

            var movementList = new MovementList
            {
                Balances = new List<Balance> {new Balance
                {
                    StartDate = testDate,
                    EndDate = testDate,
                    StartBalance = 12578.73m,
                    EndBalance = 16589.98m,
                    IncomingBalance = 547.97m,
                    OutgoingBalance = 689.65m,
                    SettlementAccount = SettlementAccount
                } },
                Documents = new List<Document> { doc1, doc2, doc3 },
                SettlementAccount = SettlementAccount,
                StartDate = testDate,
                EndDate = testDate
            };
            var result = KlTo1CGenerator.GenerateDocument(movementList, testDate);

            Assert.AreEqual(TestResources.KlTo1CGenerator_result_1c, result);
        }

        private Document CreateDoc(string number)
        {
            return new Document
            {
                DocDate = testDate,
                DocumentNumber = number,
                IncomingDate = testDate,
                PaymentPurpose = "Тестовая п/п без комисии",
                Summa = 547.97m,
                CodeUin = "0",

                Contractor = "Получатель 2",
                ContractorAccount = SettlementAccount,
                ContractorBankName = "Тинькофф",
                ContractorBik = "046546446",
                ContractorInn = "787874944941",
                ContractorKpp = "567484564",

                Payer = "Плательщик 1",
                PayerAccount = SettlementAccount,
                PayerBankName = "Тинькофф",
                PayerBik = "046546446",
                PayerInn = "787874944941",
                PayerKpp = "567484564",
                PayerStatus = "статус",

                IndicatorKbk = "18210101011011000110",
                Okato = "87845415054",
                OutgoingDate = testDate,
                PaymentDate = testDate,
                PaymentFoundation = "тест",
                PaymentKind = "01",
                PaymentNumber = "тест",
                PaymentTurn = null,
                PaymentType = "тест",
                Period = "тест",
                RawSection = null,
                ReservedString = null,
                SectionName = "Платежное поручение",
                Type = TransferType.NotDefined
            };
        }
    }
}