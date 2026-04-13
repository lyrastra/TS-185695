using FluentAssertions;
using Moedelo.Money.BankBalanceHistory.Business.Balances;
using Moedelo.Money.BankBalanceHistory.Domain;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.BankBalanceHistory.Business.Tests
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.Children)]
    public class BalanceUpdateGeneratorTests
    {
        private const string SettlementAccountNumber = "12345678901234567890";

        [Test]
        public void Create_GenersatesOneRequest_IfOneDayMovementWithoutDocuments()
        {
            // arrange
            var movementList = new MovementList
            {
                StartDate = new DateTime(2020, 09, 15),
                EndDate = new DateTime(2020, 09, 15),
                Documents = new List<Document>(),
                Balances =
                [
                    new Balance
                    {
                        StartBalance = 1,
                        EndBalance = 2,
                        IncomingBalance = 0,
                        OutgoingBalance = 0
                    }
                ]
            };

            // act
            var requests = BalanceRequestCreator.Create(0, movementList);

            // assert
            requests.Should().HaveCount(1);
            requests.ElementAt(0).BalanceDate.Should().Be(movementList.StartDate);
            requests.ElementAt(0).StartBalance.Should().Be(movementList.StartBalance);
            requests.ElementAt(0).EndBalance.Should().Be(movementList.EndBalance);
            requests.ElementAt(0).IncomingBalance.Should().Be(movementList.IncomingBalance);
            requests.ElementAt(0).OutgoingBalance.Should().Be(movementList.OutgoingBalance);
        }

        [Test]
        public void Create_GeneratesNothing_IfMultipleDaysMovementWithoutDocuments()
        {
            // arrange
            var movementList = new MovementList
            {
                StartDate = new DateTime(2020, 09, 15),
                EndDate = new DateTime(2020, 09, 17),
                Documents = new List<Document>(),
                Balances =
                [
                    new Balance
                    {
                        StartBalance = 1,
                        EndBalance = 2,
                        IncomingBalance = 0,
                        OutgoingBalance = 0
                    }
                ]
            };

            // act
            var requests = BalanceRequestCreator.Create(0, movementList);

            // assert
            requests.Should().BeEmpty();
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        public void Create_GeneratesNothing_IfBalanceHasMovingsWithoutDocuments(decimal incomingBalance, decimal outgoingBalance)
        {
            // arrange
            var movementList = new MovementList
            {
                StartDate = new DateTime(2020, 09, 15),
                EndDate = new DateTime(2020, 09, 15),
                Documents = new List<Document>(),
                Balances =
                [
                    new Balance
                    {
                        StartBalance = 1,
                        EndBalance = 2,
                        IncomingBalance = incomingBalance,
                        OutgoingBalance = outgoingBalance
                    }
                ]
            };

            // act
            var requests = BalanceRequestCreator.Create(0, movementList);

            // assert
            requests.Should().BeEmpty();
        }

        [Test]
        public void Create_GeneratesRequestsForEachDay_IfDocumentsPassed()
        {
            // arrange
            var startDate = new DateTime(2020, 09, 16);
            var endDate = new DateTime(2020, 09, 17);
            var movementList = new MovementList
            {
                SettlementAccount = SettlementAccountNumber,
                StartDate = startDate,
                EndDate = endDate,
                Balances =
                [
                    new Balance
                    {
                        StartBalance = 20,
                        EndBalance = 50
                    }
                ],
                Documents =
                [
                    new Document { Summa = 20, OutgoingDate = startDate, PayerAccount = SettlementAccountNumber },
                    new Document { Summa = 50, IncomingDate = endDate, ContractorAccount = SettlementAccountNumber }
                ],
            };

            // act
            var expected = new[]
            {
                new BankBalanceUpdateRequest
                {
                    BalanceDate = startDate,
                    StartBalance = 20,
                    IncomingBalance = 0,
                    OutgoingBalance = 20,
                    EndBalance = 0
                },
                new BankBalanceUpdateRequest
                {
                    BalanceDate = endDate,
                    StartBalance = 0,
                    IncomingBalance = 50,
                    OutgoingBalance = 0,
                    EndBalance = 50
                }
            };
            var actual = BalanceRequestCreator.Create(0, movementList);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Create_GeneratesOneRequest_IfDocumentsAreForOneDay()
        {
            // arrange
            var date = new DateTime(2020, 09, 15);
            var movementList = new MovementList
            {
                SettlementAccount = SettlementAccountNumber,
                StartDate = date,
                EndDate = date,
                Balances =
                [
                    new Balance
                    {
                        StartBalance = 0,
                        EndBalance = 20
                    }
                ],
                Documents =
                [
                    new Document { Summa = 10, IncomingDate = date, ContractorAccount = SettlementAccountNumber },
                    new Document { Summa = 20, OutgoingDate = date, PayerAccount = SettlementAccountNumber },
                    new Document { Summa = 20, OutgoingDate = date, PayerAccount = SettlementAccountNumber },
                    new Document { Summa = 50, IncomingDate = date, ContractorAccount = SettlementAccountNumber }
                ],
            };

            // act
            var expected = new[]
            {
                new BankBalanceUpdateRequest
                {
                    BalanceDate = date,
                    StartBalance = 0,
                    IncomingBalance = 60,
                    OutgoingBalance = 40,
                    EndBalance = 20
                }
            };
            var actual = BalanceRequestCreator.Create(0, movementList);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Create_GeneratesRequestsForEachDay_EventIfDocumentsIsAbsentForSomeDays()
        {
            // arrange
            var startDate = new DateTime(2020, 09, 16);
            var endDate = new DateTime(2020, 09, 17);
            var movementList = new MovementList
            {
                SettlementAccount = SettlementAccountNumber,
                StartDate = startDate,
                EndDate = endDate,
                Balances =
                [
                    new Balance
                    {
                        StartBalance = 30,
                        EndBalance = 10
                    }
                ],
                Documents =
                [
                    new Document { Summa = 20, OutgoingDate = startDate, PayerAccount = SettlementAccountNumber }
                ],
            };

            // act
            var expected = new[]
            {
                new BankBalanceUpdateRequest
                {
                    BalanceDate = startDate,
                    StartBalance = 30,
                    IncomingBalance = 0,
                    OutgoingBalance = 20,
                    EndBalance = 10
                },
                new BankBalanceUpdateRequest
                {
                    BalanceDate = endDate,
                    StartBalance = 10,
                    IncomingBalance = 0,
                    OutgoingBalance = 0,
                    EndBalance = 10
                }
            };
            var actual = BalanceRequestCreator.Create(0, movementList);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
