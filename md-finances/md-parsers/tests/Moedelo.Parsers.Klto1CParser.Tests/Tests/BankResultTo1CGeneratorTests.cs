using System;
using System.Collections.Generic;
using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser;
using Moedelo.Parsers.Klto1CParser.Tests.Resources;
using NUnit.Framework;

namespace Moedelo.Parsers.Klto1CParser.Tests.Tests
{
    [TestFixture]
    public class BankResultTo1CGeneratorTests
    {
        private readonly DateTime testDate = new DateTime(2018, 1, 1, 12, 43, 25);

        [Test]
        public void ShouldGenerate()
        {
            const string settlementAccount = "12345678901234567890";

            var doc1 = CreateDoc(settlementAccount, "1");
            var doc2 = CreateDoc(settlementAccount, "2");
            var doc3 = CreateDoc(settlementAccount, "3");

            var model1C = new Model1C
            {
                Header = new Model1CHeader
                {
                    BeginDate = testDate,
                    EndDate = testDate,
                    BankName = "имя_банка",
                    IncomingBalance = 12578.73m,
                    OutgoingBalance = 16589.98m,
                    IncomingSumm = 547.97m,
                    OutgoingSumm = 689.65m,
                    SettlementAccount = settlementAccount
                },
                Docs = new List<Model1CDoc> { doc1, doc2, doc3 }
            };

            var result = BankResultTo1CGenerator.GenerateDocument(model1C, testDate);

            Assert.AreEqual(TestResources.bank_result_1c, result);
        }

        private Model1CDoc CreateDoc(string settlementAccount, string number)
        {
            return new Model1CDoc
            {
                Date = testDate,
                Number = number,
                IncomingDate = testDate,
                Purpose = "Тестовая п/п без комисии",
                Summ = 547.97m,
                Priority = 0,
                BudgetDetails = new BudgetDetails
                {
                    BudgetaryDocDate = testDate,
                    BudgetaryDocNumber = "тест",
                    BudgetaryPaymentBase = "тест",
                    BudgetaryPaymentType = "тест",
                    BudgetaryPayerStatus = "тест_статус",
                    BudgetaryPeriod = "тест",
                    Kbk = "18210101011011000110",
                    Okato = "87845415054"
                },
                Payer = new ContactDetails
                {
                    SettlementNumber = settlementAccount,
                    Name = "Плательщик 1",
                    Kpp = "567484564",
                    Inn = "787874944941",
                    BankName = "Тинькофф",
                    BankBik = "046546446",
                    BankCorrespondentAccount = settlementAccount
                },
                Recipient = new ContactDetails
                {
                    SettlementNumber = settlementAccount,
                    Name = "Получатель 2",
                    Kpp = "567484564",
                    Inn = "787874944941",
                    BankName = "Тинькофф",
                    BankBik = "046546446",
                    BankCorrespondentAccount = settlementAccount
                }
            };
        }
    }
}