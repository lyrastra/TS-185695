using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moedelo.BankIntegrations.Converter1C.Business;
using Moedelo.BankIntegrations.Enums;
using Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser;

namespace Moedelo.BankIntegrations.Converter1C.Tests
{
    [TestFixture]
    public class Model1CToDocumentSectionConverterTests
    {
        [Test]
        public void ConvertToDocumentSections_IncomeOperation_SetsCorrectType()
        {
            // Arrange
            var model = new Model1C
            {
                Header = new Model1CHeader { SettlementAccount = "40702810123450000123" },
                Docs = new List<Model1CDoc> // ← старый синтаксис
                {
                    new Model1CDoc
                    {
                        Number = "INV-001",
                        Summ = 15000.50m,
                        Purpose = "Оплата за товар",
                        Date = new DateTime(2025, 3, 1),
                        IncomingDate = new DateTime(2025, 3, 1),
                        OutgoingDate = DateTime.MinValue,
                        Payer = new ContactDetails
                        {
                            Name = "ООО Поставщик",
                            Inn = "7712345678",
                            Kpp = "771201001",
                            SettlementNumber = "40701810987650000456",
                            BankBik = "044525225",
                            BankName = "Сбербанк"
                        },
                        Recipient = new ContactDetails
                        {
                            Name = "ИП Иванов",
                            Inn = "772345678901",
                            Kpp = "",
                            SettlementNumber = "40702810123450000123", // наш счёт
                            BankBik = "044525555",
                            BankName = "ВТБ"
                        }
                    }
                }
            };

            // Act
            var result = Model1CToDocumentSectionConverter.ConvertToDocumentSections(model);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            var doc = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(doc.OperationType, Is.EqualTo(OperationType.IncomeOperation));
                Assert.That(doc.ContractorAccount, Is.EqualTo("40702810123450000123".Substring(0, 20)));
                Assert.That(doc.Summa, Is.EqualTo(15000.50m));
                Assert.That(doc.DocDate, Is.EqualTo("01.03.2025"));
            });
        }
    }
}