using System;
using System.Diagnostics;
using System.Text;
using FluentAssertions;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Exceptions;
using Moedelo.Parsers.Klto1CParser.Tests.Resources;
using NUnit.Framework;

namespace Moedelo.Parsers.Klto1CParser.Tests.Tests
{
    [TestFixture]
    public class Klto1CParserTests
    {
        public Klto1CParserTests()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void ShouldParse()
        {
            var movementList = Business.Klto1CParser.Parse(TestResources.kl_to_1c);

            movementList.SettlementAccount.Should().Be("40802810727100015991");
            movementList.StartDate.Should().Be(DateTime.Parse("01.10.2014"));
            movementList.EndDate.Should().Be(DateTime.Parse("26.12.2014"));
            movementList.StartBalance.Should().Be(884.42m);
            movementList.IncomingBalance.Should().Be(187541m);
            movementList.OutgoingBalance.Should().Be(177331.65m);
            movementList.EndBalance.Should().Be(11093.77m);
            movementList.Documents.Count.Should().Be(1);
            movementList.Documents[0].SectionName.Should().NotBeEmpty();
            movementList.Documents[0].ContractorBankName.Should().BeEquivalentTo("Œ¿Œ ¿ ¡ \"¿¬¿Õ√¿–ƒ\", „. ÃŒ— ¬¿");
            movementList.Documents[0].ContractorBankCorrespondentAccount.Should().BeEquivalentTo("30101810000000000201");
            movementList.Documents[0].PayerBankName.Should().BeEquivalentTo("Œ¿Œ ¿ ¡ \"¿¬¿Õ√¿–ƒ\", „. ÃŒ— ¬¿");
            movementList.Documents[0].PayerBankCorrespondentAccount.Should().BeEquivalentTo("30101810000000000201");
            movementList.Documents[0].RawPosition.Should().Be(431);
        }

        [Test]
        public void ShouldParseBigFiles()
        {
            var stopwatch = Stopwatch.StartNew();
            var movementList = Business.Klto1CParser.Parse(TestResources.big_one);
            var elapsed = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            elapsed.Should().BeLessThan(250);

            movementList.SettlementAccount.Should().Be("40702810228070100206");
            movementList.StartDate.Should().Be(DateTime.Parse("01.02.2017"));
            movementList.EndDate.Should().Be(DateTime.Parse("01.03.2017"));
            movementList.StartBalance.Should().Be(11236.67m);
            movementList.IncomingBalance.Should().Be(890096.93m);
            movementList.OutgoingBalance.Should().Be(893346.51m);
            movementList.EndBalance.Should().Be(7987.09m);
            movementList.Balances.Count.Should().Be(19);
            movementList.Documents.Count.Should().Be(220);
        }

        [Test]
        public void ShouldParseHugeFiles()
        {
            var stopwatch = Stopwatch.StartNew();
            var movementList = Business.Klto1CParser.Parse(TestResources.huge_one);
            var elapsed = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            elapsed.Should().BeLessThan(1300);

            movementList.SettlementAccount.Should().Be("40702810326170002291");
            movementList.StartDate.Should().Be(DateTime.Parse("01.03.2018"));
            movementList.EndDate.Should().Be(DateTime.Parse("26.04.2018"));
            movementList.StartBalance.Should().Be(6962765m);
            movementList.IncomingBalance.Should().Be(-290399814m);
            movementList.OutgoingBalance.Should().Be(288034063m);
            movementList.EndBalance.Should().Be(4597014m);
            movementList.Balances.Count.Should().Be(1);
            movementList.Documents.Count.Should().Be(4371);
        }

        [Test]
        public void ShouldParseGiantFiles()
        {
            var stopwatch = Stopwatch.StartNew();
            var movementList = Business.Klto1CParser.Parse(TestResources.giant_one);
            var elapsed = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            elapsed.Should().BeLessThan(3000);

            movementList.SettlementAccount.Should().Be("40802810070050003798");
            movementList.StartDate.Should().Be(DateTime.Parse("01.01.2017"));
            movementList.EndDate.Should().Be(DateTime.Parse("20.04.2020"));
            movementList.StartBalance.Should().Be(177931.8m);
            movementList.IncomingBalance.Should().Be(45811258.12m);
            movementList.OutgoingBalance.Should().Be(45675290.78m);
            movementList.EndBalance.Should().Be(313899.14m);
            movementList.Balances.Count.Should().Be(39);
            movementList.Documents.Count.Should().Be(9999);
        }

        [Test]
        public void ShouldParseIncrediblyHugeFiles()
        {
            var stopwatch = Stopwatch.StartNew();
            var movementList = Business.Klto1CParser.Parse(TestResources.incredibly_huge);
            var elapsed = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            elapsed.Should().BeLessThan(10000);

            movementList.SettlementAccount.Should().Be("40702810001960000979");
            movementList.StartDate.Should().Be(DateTime.Parse("01.01.2019"));
            movementList.EndDate.Should().Be(DateTime.Parse("13.10.2019"));
            movementList.StartBalance.Should().Be(114053.68m);
            movementList.IncomingBalance.Should().Be(16424742.37m);
            movementList.OutgoingBalance.Should().Be(16538111.05m);
            movementList.EndBalance.Should().Be(685.00m);
            movementList.Balances.Count.Should().Be(1);
            movementList.Documents.Count.Should().Be(31643);
        }

        [Test]
        public void ShouldFallWhenFileHasBadFormat()
        {
            Action parse = () => Business.Klto1CParser.Parse(TestResources.bad_format);
            parse.Should().Throw<BadFormatException>();
        }

        [Test]
        public void ShouldNotFallWhenFileHasNotBadFormat()
        {
            Action parse = () => Business.Klto1CParser.Parse(TestResources.bankInvolvement);
            parse.Should().NotThrow<BadFormatException>();
        }

        [Test]
        public void ShouldFallWhenMissingStartBalance()
        {
            Action parse = () => Business.Klto1CParser.Parse(TestResources.alfa_empty);
            parse.Should().Throw<MissingStartBalanceException>();
        }

        [Test]
        public void ShouldFallWhenMissingStartBalanceAndCheckOptionIsDisabled()
        {
            Action parse = () => Business.Klto1CParser.ParseBalancesSections(TestResources.alfa_empty, ParseOptions.NoCheckStartBalance);
            parse.Should().NotThrow<MissingStartBalanceException>();
        }

        [Test]
        public void ShouldParseStartBalanceWithComma()
        {
            var movementList = Business.Klto1CParser.Parse(TestResources.start_balance_comma);
            const decimal expectedBalance = 12578.73m;
            movementList.StartBalance.Should().Be(expectedBalance);
        }

        [Test]
        public void ShouldParseBalancesWithoutCommonSection()
        {
            var balances = Business.Klto1CParser.ParseBalancesSections(TestResources.reconcilation_tinkoff);
            const decimal expectedBalance = 41751.43m;
            balances[0].StartBalance.Should().Be(expectedBalance);
        }

        [Test]
        public void ShouldParseByDocuments()
        {
            var documents = new[]
            {
                TestResources.reconcilation_document_first,
                TestResources.reconcilation_document_second
            };
            var file = string.Join(Environment.NewLine, documents);
            var documentSections = Business.Klto1CParser.ParseDocumentsSections(file);

            documentSections.Count.Should().Be(2);
            documentSections[0].Summa.Should().Be(25000);
            documentSections[1].ContractorInn.Should().BeEquivalentTo("7735154433");
        }

        [Test]
        public void ShouldParseChargeDate()
        {
            var movementList = Business.Klto1CParser.Parse(TestResources.charge_request);
            var response = Business.KlTo1CGenerator.GenerateDocument(movementList);
            Assert.IsTrue(response.Contains("ƒ‡Ú‡—ÔËÒ‡ÌÓ=03.05.2018"));
        }

        [Test]
        public void ShouldParseAccountsWithMultiLinePayerAndContractor()
        {
            var movementList = Business.Klto1CParser.Parse(TestResources.multiline_payer_contractor);
            movementList.Documents[0].ContractorAccount.Should().BeEquivalentTo("LV82NDEA0000083700958");
            movementList.Documents[0].PayerAccount.Should().BeEquivalentTo("40702978410000003920");
        }

        [Test]
        public void ShouldFixSberbankAccountNumberStartingWithSlash()
        {
            var movementList = Business.Klto1CParser.Parse(TestResources.sber_slash_in_account_number);
            movementList.Documents[0].ContractorAccount.Should().BeEquivalentTo("40702978138000001920");
        }
    }
}
