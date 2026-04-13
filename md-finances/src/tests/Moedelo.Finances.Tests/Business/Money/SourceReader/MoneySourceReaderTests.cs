using AutoFixture;
using FluentAssertions;
using Moedelo.AccountingV2.Client.Cash;
using Moedelo.AccountingV2.Dto.Cash;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Money.Sources;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.KontragentsV2.Dto;
using Moedelo.RequisitesV2.Client.Purses;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.Purses;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Finances.Tests.Business.Money.SourceReader
{
    [TestFixture]
    public class MoneySourceReaderTests
    {
        private Fixture randomizer;
        private Mock<IUserContext> userContext;
        private Mock<ISettlementAccountClient> settlementAccountClient;
        private Mock<ICashApiClient> cashApiClient;
        private Mock<IPurseClient> purseClient;
        private Mock<IKontragentsClient> kontragentsApiClient;
        private Mock<IMoneyBalancesReader> moneyBalancesReader;
        private Mock<IBankIntegrationDataReader> integrationDataReader;
        private Mock<IReconciliationDataReader> reconciliationDataReader;
        private Mock<IBankDataReader> bankDataReader;
        private Mock<IYandexKassaIntegrationDataReader> yandexKassaIntegrationDataReader;
        private MoneySourceReader reader;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            randomizer = new Fixture();
            userContext = new Mock<IUserContext>();
        }

        [SetUp]
        public void Setup()
        {
            settlementAccountClient = new Mock<ISettlementAccountClient>();
            cashApiClient = new Mock<ICashApiClient>();
            purseClient = new Mock<IPurseClient>();
            kontragentsApiClient = new Mock<IKontragentsClient>();
            moneyBalancesReader = new Mock<IMoneyBalancesReader>();
            integrationDataReader = new Mock<IBankIntegrationDataReader>();
            reconciliationDataReader = new Mock<IReconciliationDataReader>();
            bankDataReader = new Mock<IBankDataReader>();
            yandexKassaIntegrationDataReader = new Mock<IYandexKassaIntegrationDataReader>();

            reader = CreateReaderWithMocks();

            SetupKontragentsApiClient();
            SetupPurseClient();
            SetupSettlementAccountClient();
            SetupCashApiClient();
            SetupBankApiClient();
            SetupBalanceReader();
            SetupReconciliationDataReader();
            SetupIntegrationReader();
        }

        [Test]
        public async Task GetAsync_ReturnsSourceWithProperReconciliationData_IfProperReconciliationDataLoaded(
            CancellationToken ctx)
        {
            // arrage
            var settlementSource = randomizer
                .Build<SettlementAccountDto>()
                .With(p => p.Type, SettlementAccountType.Default)
                .Create();
            var reconciliationData = randomizer.Create<ReconciliationData>();
            SetupSettlementAccountClient(new List<SettlementAccountDto> { settlementSource });
            SetupReconciliationDataReader(new Dictionary<long, ReconciliationData>
            {
                { settlementSource.Id, reconciliationData }
            });

            // act
            var result = await reader
                .GetAsync(userContext.Object, ctx)
                .ConfigureAwait(false);
            var source = result.Find(x => x.Id == settlementSource.Id);

            // assert
            ClassicAssert.AreEqual(reconciliationData.IsReconciliationInProcess, source.IsReconciliationProcessing);
            ClassicAssert.AreEqual(reconciliationData.HasEmployees, source.HasEmployees);
        }

        [Test]
        public async Task GetAsync_ReturnsSourceWithProperIntegrationData_IfProperIntegrationDataLoaded(
            CancellationToken ctx)
        {
            // arrage
            var settlementSource = randomizer
                .Build<SettlementAccountDto>()
                .With(p => p.Type, SettlementAccountType.Default)
                .Create();
            var integrationData = randomizer.Create<IntegrationData>();
            SetupSettlementAccountClient(new List<SettlementAccountDto> { settlementSource });
            SetupIntegrationReader(new Dictionary<long, IntegrationData>
            {
                { settlementSource.Id, integrationData }
            });

            // act
            var result = await reader.GetAsync(userContext.Object, ctx).ConfigureAwait(false);
            var source = result.Find(x => x.Id == settlementSource.Id);

            // assert
            ClassicAssert.AreEqual(integrationData.IntegrationPartner, source.IntegrationPartner);
            ClassicAssert.AreEqual(integrationData.CanRequestMovementList, source.CanRequestMovementList);
            ClassicAssert.AreEqual(integrationData.CanSendPaymentOrder, source.CanSendPaymentOrder);
            ClassicAssert.AreEqual(integrationData.IntegrationImage, source.IntegrationImage);
            ClassicAssert.AreEqual(integrationData.HasActiveIntegration, source.HasActiveIntegration);
            ClassicAssert.AreEqual(integrationData.HasUnprocessedRequests, source.HasUnprocessedRequests);
        }

        [Test]
        public async Task GetAsync_ReturnsSourcesWithFilledBalance_IfProperItemHasBalance(
            CancellationToken ctx)
        {
            // arrage
            var balance = randomizer.Create<decimal>();
            var settlementSource = randomizer
                .Build<SettlementAccountDto>()
                .With(p => p.Type, SettlementAccountType.Default)
                .Create();
            SetupSettlementAccountClient(new List<SettlementAccountDto> { settlementSource });
            SetupBalanceReader(new List<MoneySourceBalance>
            {
                new MoneySourceBalance
                {
                    Id = settlementSource.Id,
                    Type = MoneySourceType.SettlementAccount,
                    Balance = balance
                }
            });

            // act
            var result = await reader.GetAsync(userContext.Object, ctx).ConfigureAwait(false);
            var allSourcesItem = result.Find(x => x.Type == MoneySourceType.All);

            // assert
            ClassicAssert.AreEqual(balance, result.Find(x => x.Id == settlementSource.Id).Balance);
            ClassicAssert.AreEqual(balance, allSourcesItem.Balance);
        }

        [Test]
        [Repeat(5)]
        public async Task GetAsync_ReturnsAllSourcesItemWithIntegrationFields_IfThereIsAtLeastOneSourceWithIntegration(
            CancellationToken ctx)
        {
            // arrage
            var settlementSource = randomizer
                .Build<SettlementAccountDto>()
                .With(p => p.Type, SettlementAccountType.Default)
                .Create();
            var integrationData = randomizer.Create<IntegrationData>();
            SetupSettlementAccountClient(new List<SettlementAccountDto> { settlementSource });
            SetupIntegrationReader(new Dictionary<long, IntegrationData>
            {
                { settlementSource.Id, integrationData }
            });

            // act
            var result = await reader.GetAsync(userContext.Object, ctx).ConfigureAwait(false);
            var allSourcesItem = result.Find(x => x.Type == MoneySourceType.All);

            // assert
            ClassicAssert.AreEqual(integrationData.HasActiveIntegration, allSourcesItem.HasActiveIntegration);
            ClassicAssert.AreEqual(integrationData.HasUnprocessedRequests, allSourcesItem.HasUnprocessedRequests);
        }

        [Test]
        public async Task GetAsync_ReturnsAllSourcesItem_EvenIfThereAreNoOtherSources(
            CancellationToken ctx)
        {
            // arrage
            // act
            var result = await reader.GetAsync(userContext.Object, ctx).ConfigureAwait(false);

            // assert
            ClassicAssert.AreEqual(1, result.Count);

            result[0].Should().BeEquivalentTo(
                new MoneySource
                {
                    Id = 0,
                    Name = "Все счета и кассы",
                    Type = MoneySourceType.All,
                    IsActive = true,
                    Balance = 0,
                    HasActiveIntegration = false,
                    HasUnprocessedRequests = false
                }
            );
        }

        private void SetupKontragentsApiClient(List<KontragentDto> list = null)
        {
            kontragentsApiClient
                .Setup(m => m.GetPopulationAndPurseAgentsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list ?? new List<KontragentDto>());
        }

        private void SetupPurseClient(List<PurseDto> list = null)
        {
            purseClient
                .Setup(m => m.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<int>>()))
                .ReturnsAsync(list ?? new List<PurseDto>());
        }

        private void SetupSettlementAccountClient(List<SettlementAccountDto> list = null)
        {
            settlementAccountClient
                .Setup(m => m.GetWithDeletedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list ?? new List<SettlementAccountDto>());
        }

        private void SetupCashApiClient(List<CashDto> list = null)
        {
            cashApiClient
                .Setup(m => m.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list ?? new List<CashDto>());
        }

        private void SetupBankApiClient(Dictionary<int, SourceBankData> dic = null)
        {
            bankDataReader
                .Setup(m => m.GetBanksBySourcesAsync(
                    It.IsAny<IUserContext>(), 
                    It.IsAny<IReadOnlyCollection<MoneySource>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(dic ?? new Dictionary<int, SourceBankData>());
        }

        private void SetupBalanceReader(List<MoneySourceBalance> list = null)
        {
            moneyBalancesReader
                .Setup(m => m.GetAsync(
                    It.IsAny<IUserContext>(),
                    It.IsAny<IReadOnlyCollection<MoneySourceBase>>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list ?? new List<MoneySourceBalance>());
        }

        private void SetupReconciliationDataReader(Dictionary<long, ReconciliationData> dic = null)
        {
            reconciliationDataReader
                .Setup(m => m.GetReconciliationDataAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IReadOnlyCollection<MoneySource>>()))
                .ReturnsAsync(dic ?? new Dictionary<long, ReconciliationData>());
        }

        private void SetupIntegrationReader(Dictionary<long, IntegrationData> dic = null)
        {
            integrationDataReader
                .Setup(m => m.GetBankIntegrationDataAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IReadOnlyCollection<MoneySource>>(), It.IsAny<Dictionary<int, SourceBankData>>()))
                .ReturnsAsync(dic ?? new Dictionary<long, IntegrationData>());
        }

        private MoneySourceReader CreateReaderWithMocks()
        {
            return new MoneySourceReader(
                settlementAccountClient.Object,
                cashApiClient.Object,
                purseClient.Object,
                kontragentsApiClient.Object,
                moneyBalancesReader.Object,
                integrationDataReader.Object,
                reconciliationDataReader.Object,
                bankDataReader.Object,
                yandexKassaIntegrationDataReader.Object
            );
        }
    }
}
