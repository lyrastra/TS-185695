using AutoFixture;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Client.IntegratedUser;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.IntegratedUser;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.Finances.Business.Services.Money.Sources.Readers;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequests;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.Finances.Tests.Business.Money.SourceReader
{
    [TestFixture]
    public class IntegrationDataReaderTests
    {
        private Fixture randomizer;
        private Mock<IBankIntegrationsDataInformationClient> integrationsDataInformationClient;
        private Mock<IIntegratedUserClient> integratedUserClient;
        private Mock<IIntegrationRequestApiClient> integrationRequestApiClient;
        private Mock<ILogger> logger;


        private int firmId;
        private int userId;

        [SetUp]
        public void Setup()
        {
            randomizer = new Fixture();

            firmId = randomizer.Create<int>();
            userId = randomizer.Create<int>();

            logger = new Mock<ILogger>();
            integratedUserClient = new Mock<IIntegratedUserClient>();
            integrationRequestApiClient = new Mock<IIntegrationRequestApiClient>();
            integrationsDataInformationClient = new Mock<IBankIntegrationsDataInformationClient>();
        }

        [Test]
        public async Task GetIntegrationDataAsync_ReturnsIntData_IfProperDataLoaded()
        {
            // arrange
            var integrationReader = new BankIntegrationDataReader(
                integrationsDataInformationClient.Object,
                logger.Object,
                integratedUserClient.Object,
                integrationRequestApiClient.Object
            );
            var sources = randomizer.CreateMany<MoneySource>().ToList();
            var banks = new Dictionary<int, SourceBankData>();
            var intSummary = new IntSummaryBySettlementsResponseDto
            {
                Result = new List<SettlementAccountStatusDto>()
            };
            var activePartners = new List<IntegrationPartnersInfoDto>();
            foreach (var source in sources)
            {
                banks[source.BankId.Value] = randomizer.Build<SourceBankData>().With(p => p.IsActive, true).Create();
                var sourceIntSummary = randomizer
                    .Build<SettlementAccountStatusDto>()
                    .With(p => p.Status, SettlementIntegrationStatus.Enabled)
                    .With(p => p.SettlementNumber, source.Number)
                    .With(p => p.Bik, source.BankBik)
                    .Create();
                intSummary.Result.Add(sourceIntSummary);
                activePartners.Add(randomizer
                    .Build<IntegrationPartnersInfoDto>()
                    .With(p => p.IntegratedPartner, sourceIntSummary.IntegrationPartner)
                    .Create());
            }

            integrationsDataInformationClient
                .Setup(m => m.GetIntSummaryBySettlementsAsync(It.IsAny<IntSummaryBySettlementsRequestDto>()))
                .ReturnsAsync(intSummary);
            integrationsDataInformationClient
                .Setup(m => m.GetRequestQueueStatusAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<IntegrationRequestQueueStatusDto>());
            integratedUserClient
                .Setup(m => m.GetActiveIntegrationsPartnersInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(activePartners);
            integrationRequestApiClient
                .Setup(m => m.HasUnprocessedRequestMovementListAsync(It.IsAny<HasUnprocessedRequestMovementListDto>()))
                .ReturnsAsync(new List<IntegrationPartners>());

            // act
            var result = await integrationReader.GetBankIntegrationDataAsync(firmId, userId, sources, banks).ConfigureAwait(false);

            // assert
            Assert.Multiple(() =>
            {
                foreach (var source in sources)
                {
                    ClassicAssert.IsTrue(result.ContainsKey(source.Id));
                }
            });

        }

        [Test]
        public async Task GetIntegrationDataAsync_ReturnsNothing_IfNoIntSummaryFound()
        {
            // arrange
            var integrationReader = new BankIntegrationDataReader(
                integrationsDataInformationClient.Object,
                logger.Object,
                integratedUserClient.Object,
                integrationRequestApiClient.Object
            );
            var sources = randomizer.CreateMany<MoneySource>().ToList();
            var banks = new Dictionary<int, SourceBankData>();
            foreach (var source in sources)
            {
                banks[source.BankId.Value] = randomizer.Build<SourceBankData>().With(p => p.IsActive, true).Create();
            }

            integrationsDataInformationClient
                .Setup(m => m.GetIntSummaryBySettlementsAsync(It.IsAny<IntSummaryBySettlementsRequestDto>()))
                .ReturnsAsync(new IntSummaryBySettlementsResponseDto());

            // act
            var result = await integrationReader.GetBankIntegrationDataAsync(firmId, userId, sources, banks).ConfigureAwait(false);

            // assert
            ClassicAssert.AreEqual(0, result.Count);
        }

        [Test]
        public async Task GetIntegrationDataAsync_ReturnsNothing_IfNoSourcesWithBankPassed()
        {
            // arrange
            var integrationReader = new BankIntegrationDataReader(
                integrationsDataInformationClient.Object,
                logger.Object,
                integratedUserClient.Object,
                integrationRequestApiClient.Object
            );
            var sources = new List<MoneySource>
            {
                randomizer.Build<MoneySource>().Without(p => p.BankId).Create()
            };

            var banks = new Dictionary<int, SourceBankData>();

            // act
            var result = await integrationReader.GetBankIntegrationDataAsync(firmId, userId, sources, banks).ConfigureAwait(false);

            // assert
            ClassicAssert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetSettlementsWithBank_ReturnSettlements_IfBankExistsAndActive()
        {
            // arrange
            var sources = randomizer.CreateMany<MoneySource>().ToList();
            var banks = new Dictionary<int, SourceBankData>();
            foreach (var source in sources)
            {
                if (!banks.ContainsKey(source.BankId.Value))
                {
                    banks[source.BankId.Value] = randomizer.Create<SourceBankData>();
                    banks[source.BankId.Value].IsActive = true;
                }
            }
            var expected = sources.Count;

            // act
            var result = BankIntegrationDataReader.GetSettlementsWithBank(sources, banks);

            // assert
            ClassicAssert.AreEqual(expected, result.Count);
        }

        [Test]
        public void GetSettlementsWithBank_ReturnNoSettlements_IfBankDoesntExist()
        {
            // arrange
            var sources = randomizer.CreateMany<MoneySource>().ToList();
            var banks = new Dictionary<int, SourceBankData>();

            // act
            var result = BankIntegrationDataReader.GetSettlementsWithBank(sources, banks);

            // assert
            ClassicAssert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetSettlementsWithBank_ReturnOnlySettlements_IfBankIsActive()
        {
            // arrange

            var sources = randomizer.CreateMany<MoneySource>().ToList();
            var banks = new Dictionary<int, SourceBankData>();
            foreach (var source in sources)
            {
                if (!banks.ContainsKey(source.BankId.Value))
                {
                    banks[source.BankId.Value] = randomizer.Create<SourceBankData>();
                }
            }
            var expected = banks.Count(x => x.Value.IsActive);

            // act
            var result = BankIntegrationDataReader.GetSettlementsWithBank(sources, banks);

            // assert
            ClassicAssert.AreEqual(expected, result.Count);
        }
    }
}
