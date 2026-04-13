using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Money.Reconciliation;
using Moedelo.Finances.Business.Services.Money.Reconciliation.ForUser;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.KontragentsV2.Dto;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using Moq;
using NUnit.Framework;

namespace Moedelo.Finances.Tests
{
    [TestFixture]
    public class ReconciliationComparerTests
    {
        private readonly Mock<IUserContext> userContextMock = new Mock<IUserContext>();
        private readonly Mock<IKontragentsClient> kontragentsClientMock = new Mock<IKontragentsClient>();
        private readonly Mock<ISettlementAccountClient> settlementAccountClientMock = new Mock<ISettlementAccountClient>();
        private readonly Mock<IPaymentOrderOperationDao> paymentOrderOperationDaoMock = new Mock<IPaymentOrderOperationDao>();
        private readonly Mock<IMoneyTransferOperationDao> moneyTransferOperationDaoMock = new Mock<IMoneyTransferOperationDao>();
        private readonly Mock<IDocumentTypeApiClient> documentTypeApiClientMock = new Mock<IDocumentTypeApiClient>();
        private readonly Mock<ILogger> logger = new Mock<ILogger>();

        private IReconciliationComparer reconciliationComparer;

        private readonly Dictionary<string, string> movementLists = new Dictionary<string, string>
        {
            {  "empty", Resources.empty },
            {  "one_operation", Resources.one_operation },
            {  "one_operation_commission", Resources.one_operation_commission },
        };

        private List<KontragentDto> kontragents = new List<KontragentDto>();
        private List<SettlementAccountDto> settlementAccounts = new List<SettlementAccountDto>();
        private List<PaymentOrderOperation> paymentOrderOperations = new List<PaymentOrderOperation>();
        private List<MoneyTransferOperation> moneyTransferOperations = new List<MoneyTransferOperation>();
        private IDictionary<string, TransferType> documentTypes = new Dictionary<string, TransferType>();

        [OneTimeSetUp]
        public void Initialize()
        {
            userContextMock.Setup(x => x.HasAllRuleAsync(AccessRule.UsnAccountantTariff)).ReturnsAsync(true);
            kontragentsClientMock.Setup(x => x.GetByInnAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(kontragents));
            settlementAccountClientMock
                .Setup(x => x.GetByNumbersAsync(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<IReadOnlyCollection<string>>()))
                .Returns(() => Task.FromResult(settlementAccounts));
            paymentOrderOperationDaoMock
                .Setup(x => x.GetForReconciliationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .Returns(() => Task.FromResult(paymentOrderOperations));
            moneyTransferOperationDaoMock
                .Setup(x => x.GetForReconciliationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .Returns(() => Task.FromResult(moneyTransferOperations));
            documentTypeApiClientMock
                .Setup(x => x.DetermineAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<Document>>()))
                .Returns(() => Task.FromResult(documentTypes));
            
            reconciliationComparer = new ReconciliationComparer(
                kontragentsClientMock.Object,
                settlementAccountClientMock.Object, 
                paymentOrderOperationDaoMock.Object,
                moneyTransferOperationDaoMock.Object, 
                documentTypeApiClientMock.Object,
                logger.Object);
        }

        [SetUp]
        public void SetUp()
        {
            kontragents = new List<KontragentDto>();
            settlementAccounts = new List<SettlementAccountDto>();
            paymentOrderOperations = new List<PaymentOrderOperation>();
            moneyTransferOperations = new List<MoneyTransferOperation>();
    }

        [Test(Description = "Пустая выписка, нет операций в сервисе")]
        public async Task Test1()
        {
            var startDate = DateTime.Parse("01.10.2017");
            var endDate = DateTime.Parse("02.10.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["empty"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Should().BeEmpty();
        }

        [Test(Description = "Есть операция в выписке, нет операций в сервисе")]
        public async Task Test2()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Count.Should().Be(1);
            result.ExcessOperations.Should().BeEmpty();
        }

        [Test(Description = "Есть операция в выписке, есть операция в сервисе, но не совпадает по направлению")]
        public async Task Test3()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation> { new PaymentOrderOperation { Direction = MoneyDirection.Incoming, Date = new DateTime(2017,10,16) } };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Count.Should().Be(1);
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "Есть операция в выписке, есть операция в сервисе, но не совпадает по сумме")]
        public async Task Test4()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation> { new PaymentOrderOperation { Sum = 100500, Date = new DateTime(2017,10,16) } };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Count.Should().Be(1);
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "Есть операция в выписке, есть операция в сервисе, но не совпадает по дате")]
        public async Task Test5()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation> { new PaymentOrderOperation { Date = new DateTime(2017, 10, 26) } };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Count.Should().Be(1);
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "Есть операция в выписке и есть операция в сервисе")]
        public async Task Test6()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017")
                }
            };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Should().BeEmpty();
        }

        [Test(Description = "Есть операция в выписке, есть две операции в сервисе с разным описанием")]
        public async Task Test7()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    DocumentBaseId = 1,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Description = "Взнос на счет ип. Сумма 10356.00, НДС не облагается"
                },
                new PaymentOrderOperation
                {
                    DocumentBaseId = 2,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Description = "ололо, трололо"
                }
            };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "Есть операция в выписке, есть две операции в сервисе с разными номерами")]
        public async Task Test8()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    DocumentBaseId = 1,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Number = "1"
                },
                new PaymentOrderOperation
                {
                    DocumentBaseId = 2,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Number = "2"
                }
            };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "Есть операция в выписке, есть две операции в сервисе с разными контрагентами")]
        public async Task Test9()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            kontragents = new List<KontragentDto> { new KontragentDto { Id = 2 } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    DocumentBaseId = 1,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Description = "Взнос на счет ип. Сумма 10356.00, НДС не облагается",
                    KontragentId = 1
                },
                new PaymentOrderOperation
                {
                    DocumentBaseId = 2,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Description = "Взнос на счет ип. Сумма 10356.00, НДС не облагается",
                    KontragentId = 2
                }
            };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["one_operation"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "Нет операции в выписке, есть операция в сервисе")]
        public async Task Test10()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    Direction = MoneyDirection.Incoming,
                    Sum = 100500,
                    Date = new DateTime(2017,10,2)
                }
            };

            var startDate = DateTime.Parse("01.10.2017");
            var endDate = DateTime.Parse("02.10.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(userContextMock.Object, movementLists["empty"], startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Count.Should().Be(1);
        }

        [Test(Description = "2 одинаковые операции (коммисия банка), с разными датами есть в сервисе и есть такая же операция с другой датой в выписке")]
        public async Task Test11()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            kontragents = new List<KontragentDto> { new KontragentDto { Id = 2 } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    DocumentBaseId = 1,
                    Number = "866",
                    Direction = MoneyDirection.Incoming,
                    Sum = 99,
                    Date = DateTime.Parse("16.10.2017"),
                    Description = "Плата за предоставление услуги SMS-банк"
                },
                new PaymentOrderOperation
                {
                    DocumentBaseId = 2,
                    Number = "866",
                    Direction = MoneyDirection.Incoming,
                    Sum = 99,
                    Date = DateTime.Parse("17.10.2017"),
                    Description = "Плата за предоставление услуги SMS-банк"
                }
            };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(
                userContextMock.Object, 
                movementLists["one_operation_commission"], 
                startDate, endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Count.Should().Be(1);
            result.ExcessOperations[0].Date.Should().Be(DateTime.Parse("17.10.2017"));
        }

        [Test(Description = "Есть две  одинаковых операций в сервисе с датами, и есть такая же операция в выписке но с другой датой")]
        public async Task Test12()
        {
            settlementAccounts = new List<SettlementAccountDto>() { new SettlementAccountDto { Number = "40802810727100015991" } };
            kontragents = new List<KontragentDto> { new KontragentDto { Id = 2 } };
            paymentOrderOperations = new List<PaymentOrderOperation>
            {
                new PaymentOrderOperation
                {
                    DocumentBaseId = 1,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("15.10.2017"),
                    Description = "Взнос на счет ип. Сумма 10356.00, НДС не облагается",
                    KontragentId = 2
                },
                new PaymentOrderOperation
                {
                    DocumentBaseId = 2,
                    Direction = MoneyDirection.Incoming,
                    Sum = 10356,
                    Date = DateTime.Parse("16.10.2017"),
                    Description = "Взнос на счет ип. Сумма 10356.00, НДС не облагается",
                    KontragentId = 2
                }
            };

            var startDate = DateTime.Parse("15.10.2017");
            var endDate = DateTime.Parse("15.11.2017");
            var result = await reconciliationComparer.CompareWithBankStatementAsync(
                userContextMock.Object, 
                movementLists["one_operation"], 
                startDate, 
                endDate).ConfigureAwait(false);

            result.MissingOperations.Should().BeEmpty();
            result.ExcessOperations.Count.Should().Be(1);
            result.ExcessOperations[0].Date.Should().Be(DateTime.Parse("16.10.2017"));
        }
    }
}
