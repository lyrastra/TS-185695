using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Business.Services.Money.Sources.Readers;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.PayrollV2.Client.Employees;
using Moedelo.PayrollV2.Dto.Employees;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Moedelo.Finances.Tests.Business.Money.SourceReader
{
    [TestFixture]
    public class ReconciliationDataReaderTests
    {
        private Mock<IEmployeesApiClient> employeesApiClient;
        private Mock<IBalanceReconcilationDao> balanceReconcilationDao;
        private Fixture randomizer;
        private int firmId;
        private int userId;


        [SetUp]
        public void Setup()
        {
            randomizer = new Fixture();
            randomizer.Customize<MoneySource>(c => 
                    c.Without(p => p.IsReconciliationProcessing)
                    .Without(p => p.HasEmployees)
                    .Without(p => p.Type)
            );

            employeesApiClient = new Mock<IEmployeesApiClient>();
            balanceReconcilationDao = new Mock<IBalanceReconcilationDao>();

            firmId = randomizer.Create<int>();
            userId = randomizer.Create<int>();
        }

        [Test]
        public async Task GetReconciliationDataAsync_ReturnsValues_IfSourceTypeIsSettlement()
        {
            // arrange
            var sources = new List<MoneySource>();
            var source1 = randomizer.Create<MoneySource>();
            source1.Type = MoneySourceType.Cash;
            var source2 = randomizer.Create<MoneySource>();
            source2.Type = MoneySourceType.SettlementAccount;
            sources.Add(source1);
            sources.Add(source2);

            balanceReconcilationDao
                .Setup(m => m.GetSettlementAccountsInProgressAsync(It.IsAny<int>(), It.IsAny<IReadOnlyCollection<int>>()))
                .ReturnsAsync(new HashSet<int>{ (int)source1.Id, (int)source2.Id });
            var reconcicliationDataReader =
                new ReconciliationDataReader(employeesApiClient.Object, balanceReconcilationDao.Object);

            // act
            var result = await reconcicliationDataReader.GetReconciliationDataAsync(firmId, userId, sources);

            // assert
            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual(source2.Id, result.First().Key);
                ClassicAssert.AreEqual(1, result.Count);
            });
        }

        [Test]
        public async Task GetReconciliationDataAsync_FillsHasEmployees_IfHaveNotFiredWorkers()
        {
            // arrange
            var sources = new List<MoneySource>();
            var source1 = randomizer.Create<MoneySource>();
            source1.Type = MoneySourceType.SettlementAccount;
            sources.Add(source1);

            employeesApiClient
                .Setup(m => m.GetNotFiredWorkersAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomizer.CreateMany<NotFiredWorkerDto>().ToList());
            balanceReconcilationDao
                .Setup(m => m.GetSettlementAccountsInProgressAsync(It.IsAny<int>(), It.IsAny<IReadOnlyCollection<int>>()))
                .ReturnsAsync(new HashSet<int> { (int)source1.Id });
            var reconcicliationDataReader =
                new ReconciliationDataReader(employeesApiClient.Object, balanceReconcilationDao.Object);

            // act
            var result = await reconcicliationDataReader.GetReconciliationDataAsync(firmId, userId, sources);

            // assert
            ClassicAssert.AreEqual(true, result.First().Value.HasEmployees);
        }

        
        [Test]
        public async Task GetReconciliationDataAsync_FillsIsReconciliationInProcess_IfReconciliationIsInProcess()
        {
            // arrange
            var sources = new List<MoneySource>();
            var source1 = randomizer.Create<MoneySource>();
            var source2 = randomizer.Create<MoneySource>();
            source1.Type = MoneySourceType.SettlementAccount;
            source2.Type = MoneySourceType.SettlementAccount;
            sources.Add(source1);
            sources.Add(source2);

            balanceReconcilationDao
                .Setup(m => m.GetSettlementAccountsInProgressAsync(It.IsAny<int>(), It.IsAny<IReadOnlyCollection<int>>()))
                .ReturnsAsync(new HashSet<int> { (int)source1.Id });
            var reconcicliationDataReader =
                new ReconciliationDataReader(employeesApiClient.Object, balanceReconcilationDao.Object);

            // act
            var result = await reconcicliationDataReader.GetReconciliationDataAsync(firmId, userId, sources);

            // assert
            ClassicAssert.AreEqual(2, result.Count);
            ClassicAssert.AreEqual(1, result.Count(x => x.Value.IsReconciliationInProcess));
            ClassicAssert.AreEqual(source1.Id, result.First(x => x.Value.IsReconciliationInProcess).Key);
        }
    }
}
