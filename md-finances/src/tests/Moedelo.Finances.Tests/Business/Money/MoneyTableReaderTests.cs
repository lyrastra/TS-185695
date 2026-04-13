using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations;
using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moq;
using NUnit.Framework;
using Moedelo.Finances.Business.Services.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;
using Moedelo.Finances.Business.Services.Money.Operations;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.BalanceMaster;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.Money.Client.BankBalanceHistory;
using Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment.SubPayment;
using Moedelo.Money.Client.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.PaymentImport.Client;
using Moedelo.PaymentImport.Dto;
using Moedelo.RequisitesV2.Client.FirmRequisites;
using Moedelo.RequisitesV2.Client.FirmTaxationSystem;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.FirmTaxationSystem;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using OperationType = Moedelo.Common.Enums.Enums.PostingEngine.OperationType;

namespace Moedelo.Finances.Tests.Business.Money
{
    [TestFixture]
    public class MoneyTableReaderTests
    {
        private Fixture randomizer;
        private Mock<IBalanceMasterService> balanceMasterServiceMock;
        private Mock<IMoneyBalancesReader> balancesReaderMock;
        private Mock<IMoneyOperationLinkedDocumentsReader> linkedDocumentsReaderMock;
        private Mock<IMoneyOperationSalaryChargePaymentsReader> salaryChargePaymentsReaderMock;
        private Mock<IMoneyOperationTaxPostingsReader> taxPostingsReaderMock;
        private Mock<IPaymentOrderOperationReader> paymentOrderOperationReaderMock;
        private Mock<IMoneyTableDao> moneyTableDaoMock;
        private Mock<IFirmTaxationSystemClient> taxationSystemClientMock;
        private Mock<IMoneyBankBalanceApiClient> moneyBankBalanceApiClientMock;
        private Mock<IPaymentImportRulesClient> paymentImportRulesClientMock;
        private Mock<IPaymentToNaturalPersonsClient> paymentToNaturalPersonsClientMock;
        private Mock<IBankOperationsApiClient> bankOperationsApiClientMock;
        private Mock<IFirmRequisitesClient> firmRequisitesClientMock;
        private Mock<IUnifiedBudgetarySubPaymentClient> unifiedBudgetarySubPaymentClientMock;
        private Mock<IOutsourceImportRulesClient> outsourceImportRulesClientMock;
        private Mock<ILogger> loggerMock;

        private MoneyTableReader reader;
        private Mock<IUserContext> userContextMock;
        
        private Mock<CanSendToBankReader> canSendToBankReaderMock;
        private Mock<IBankIntegrationDataReader> integrationDataReaderMock;
        private Mock<IBankDataReader> bankDataReaderMock;
        private Mock<ISettlementAccountClient> settlementAccountClientMock;
        private Mock<IPaymentOrderOperationDao> paymentOrderOperationDaoMock;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            randomizer = new Fixture();
            userContextMock = new Mock<IUserContext>();
        }
        
        [SetUp]
        public void Setup()
        {
            balanceMasterServiceMock = new Mock<IBalanceMasterService>();
            balancesReaderMock = new Mock<IMoneyBalancesReader>();
            linkedDocumentsReaderMock = new Mock<IMoneyOperationLinkedDocumentsReader>();
            salaryChargePaymentsReaderMock = new Mock<IMoneyOperationSalaryChargePaymentsReader>();
            taxPostingsReaderMock = new Mock<IMoneyOperationTaxPostingsReader>();
            paymentOrderOperationReaderMock = new Mock<IPaymentOrderOperationReader>();
            moneyTableDaoMock = new Mock<IMoneyTableDao>();
            taxationSystemClientMock = new Mock<IFirmTaxationSystemClient>();
            moneyBankBalanceApiClientMock = new Mock<IMoneyBankBalanceApiClient>();
            paymentImportRulesClientMock = new Mock<IPaymentImportRulesClient>();
            paymentToNaturalPersonsClientMock = new Mock<IPaymentToNaturalPersonsClient>();
            bankOperationsApiClientMock = new Mock<IBankOperationsApiClient>();
            firmRequisitesClientMock = new Mock<IFirmRequisitesClient>();
            unifiedBudgetarySubPaymentClientMock = new Mock<IUnifiedBudgetarySubPaymentClient>();
            outsourceImportRulesClientMock = new Mock<IOutsourceImportRulesClient>();
            loggerMock = new Mock<ILogger>();
            
            integrationDataReaderMock = new Mock<IBankIntegrationDataReader>();
            bankDataReaderMock = new Mock<IBankDataReader>();
            settlementAccountClientMock = new Mock<ISettlementAccountClient>();
            paymentOrderOperationDaoMock = new Mock<IPaymentOrderOperationDao>();
            
            canSendToBankReaderMock = new Mock<CanSendToBankReader>(
                integrationDataReaderMock.Object,
                bankDataReaderMock.Object,
                settlementAccountClientMock.Object,
                paymentOrderOperationDaoMock.Object);

            reader = new MoneyTableReader(
                balanceMasterServiceMock.Object,
                balancesReaderMock.Object,
                linkedDocumentsReaderMock.Object,
                salaryChargePaymentsReaderMock.Object,
                taxPostingsReaderMock.Object,
                paymentOrderOperationReaderMock.Object,
                moneyTableDaoMock.Object,
                taxationSystemClientMock.Object,
                moneyBankBalanceApiClientMock.Object,
                paymentImportRulesClientMock.Object,
                paymentToNaturalPersonsClientMock.Object,
                canSendToBankReaderMock.Object,
                bankOperationsApiClientMock.Object,
                firmRequisitesClientMock.Object,
                unifiedBudgetarySubPaymentClientMock.Object,
                outsourceImportRulesClientMock.Object,
                loggerMock.Object);
        }

        [Test]
        public async Task LoadPassThruPaymentStateAsync_ProperlySetsPaymentState_AccordingToStatusPriorityRules()
        {
            // Arrange
            var docIdExample1 = 100;  // Пример 1: Нет статуса банка + Не оплачено в МД
            var docIdExample2 = 101;  // Пример 2: Успех банка + Оплачено в МД
            var docIdExample3 = 102;  // Пример 3: Ошибка банка + Не оплачено в МД
            var docIdExample4 = 103;  // Пример 4: Ручное "Оплачено" + Банк отклонил
            var docIdExample5 = 104;  // Пример 5: Множественные попытки

            var firmTaxationSystemDto = randomizer
                .Build<FirmTaxationSystemDto>()
                .With(x => x.IsUSN, true)
                .Create();
          
            // Операции из MSSQL (Accounting_PaymentOrder)
            var operations = new List<MainMoneyTableOperation>
            {
                // Пример 1: Нет статуса банка + Не оплачено в МД
                new MainMoneyTableOperation
                {
                    DocumentBaseId = docIdExample1,
                    PaidStatus = DocumentStatus.NotPayed,
                    OperationType = OperationType.PaymentOrderIncomingOther,
                    SettlementType = SettlementAccountType.Default,
                    Direction = MoneyDirection.Outgoing
                },
                
                // Пример 2: Успех банка + Оплачено в МД
                new MainMoneyTableOperation
                {
                    DocumentBaseId = docIdExample2,
                    PaidStatus = DocumentStatus.Payed,
                    OperationType = OperationType.PaymentOrderIncomingOther,
                    SettlementType = SettlementAccountType.Default,
                    Direction = MoneyDirection.Outgoing
                },
                
                // Пример 3: Ошибка банка + Не оплачено в МД
                new MainMoneyTableOperation
                {
                    DocumentBaseId = docIdExample3,
                    PaidStatus = DocumentStatus.NotPayed,
                    OperationType = OperationType.PaymentOrderIncomingOther,
                    SettlementType = SettlementAccountType.Default,
                    Direction = MoneyDirection.Outgoing
                },
                
                // Пример 4: Изначально пользователь вручную проставил "Оплачено", но банк отклонил
                // В итоге в MSSQL статус "Не оплачено", в банке - "Ошибка"
                new MainMoneyTableOperation
                {
                    DocumentBaseId = docIdExample4,
                    PaidStatus = DocumentStatus.NotPayed,
                    OperationType = OperationType.PaymentOrderIncomingOther,
                    SettlementType = SettlementAccountType.Default,
                    Direction = MoneyDirection.Outgoing
                },
                
                // Пример 5: Множественные попытки (последняя успешная)
                new MainMoneyTableOperation
                {
                    DocumentBaseId = docIdExample5,
                    PaidStatus = DocumentStatus.Payed,
                    OperationType = OperationType.PaymentOrderIncomingOther,
                    SettlementType = SettlementAccountType.Default,
                    Direction = MoneyDirection.Outgoing
                }
            };

            // Статусы из банка (PostgreSQL - invoice_status)
            var invoiceDetails = new List<InvoiceDetailResponseDto>
            {
                // Пример 2: Успех в банке
                new InvoiceDetailResponseDto
                {
                    DocumentBaseId = docIdExample2,
                    Status = InvoiceStatus.Processed,
                    DescriptionStatus = "Успешная оплата",
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    CreateDate = DateTime.Now.AddHours(-1)
                },
                
                // Пример 3: Ошибка в банке
                new InvoiceDetailResponseDto
                {
                    DocumentBaseId = docIdExample3,
                    Status = InvoiceStatus.Failed,
                    DescriptionStatus = "Ошибка оплаты",
                    IntegrationPartnerId = IntegrationPartners.PointBank,
                    CreateDate = DateTime.Now.AddHours(-2)
                },
                
                // Пример 4: Банк отклонил платеж
                new InvoiceDetailResponseDto
                {
                    DocumentBaseId = docIdExample4,
                    Status = InvoiceStatus.Failed,
                    DescriptionStatus = "Платеж отклонен банком",
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    CreateDate = DateTime.Now.AddHours(-1)
                },
                
                // Пример 5: Первая попытка (неудачная)
                new InvoiceDetailResponseDto
                {
                    DocumentBaseId = docIdExample5,
                    Status = InvoiceStatus.Failed,
                    DescriptionStatus = "Первая попытка не удалась",
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    CreateDate = DateTime.Now.AddHours(-3)
                },
                
                // Пример 5: Вторая попытка (успешная)
                new InvoiceDetailResponseDto
                {
                    DocumentBaseId = docIdExample5,
                    Status = InvoiceStatus.Processed,
                    DescriptionStatus = "Оплата прошла успешно",
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    CreateDate = DateTime.Now.AddHours(-1)
                }
            };
            
            var contextExtraDataMock = new Mock<IUserContextExtraData>();

            contextExtraDataMock.Setup(x => x.FirmRegistrationDate).Returns(new DateTime(2025, 06, 20));
            contextExtraDataMock.Setup(x => x.IsOoo).Returns(true);
            userContextMock.Setup(x => x.GetContextExtraDataAsync()).ReturnsAsync(contextExtraDataMock.Object);
            userContextMock
                .Setup(x => x.HasAllRuleAsync(AccessRule.UsnAccountantTariff))
                .ReturnsAsync(true);

            balanceMasterServiceMock.Setup(x => x.GetStatusAsync(
                It.IsAny<IUserContext>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(new BalanceMasterStatus{ Date = new DateTime(2025, 06, 26), IsCompleted = false});
            
            taxationSystemClientMock.Setup(x => x.GetTaxationSystemAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    CancellationToken.None))
                .ReturnsAsync(firmTaxationSystemDto);
            
            bankOperationsApiClientMock
                .Setup(x => x.GetListInvoiceDetailByDocumentBaseIdsAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<IReadOnlyCollection<long>>()))
                .ReturnsAsync(new IntegrationResponseDto<List<InvoiceDetailResponseDto>> ( invoiceDetails));
            
            moneyTableDaoMock
                .Setup(x => x.GetMultiCurrencyTableAsync(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<MainMoneyTableRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MainMoneyMultiCurrencyTableResponse {Operations = operations, Summaries = randomizer.Create<List<MainMoneyMultiCurrencyTableSummary>>(), BankBalance = randomizer.Create<MainMoneyTableBankBalance>()});
            
            taxPostingsReaderMock
                .Setup(x => x.GetSumsByBaseIdsAsync(It.IsAny<IUserContext>(), It.IsAny<List<long>>()))
                .ReturnsAsync(new Dictionary<long, List<TaxSumRec>>
                {
                    {docIdExample1, new List<TaxSumRec> 
                        {
                            new TaxSumRec
                            {
                            
                                TaxType = TaxationSystemType.Usn
                            }
                        }
                    },
                    {docIdExample2, new List<TaxSumRec> 
                        {
                            new TaxSumRec
                            {
                            
                                TaxType = TaxationSystemType.Usn
                            }
                        }
                    },
                    {docIdExample3, new List<TaxSumRec> 
                        {
                            new TaxSumRec
                            {
                            
                                TaxType = TaxationSystemType.Usn
                            }
                        }
                    },
                    {docIdExample4, new List<TaxSumRec> 
                        {
                            new TaxSumRec
                            {
                            
                                TaxType = TaxationSystemType.Usn
                            }
                        }
                    },
                    {docIdExample5, new List<TaxSumRec> 
                        {
                            new TaxSumRec
                            {
                            
                                TaxType = TaxationSystemType.Usn
                            }
                        }
                    }
                });

            paymentImportRulesClientMock.Setup(x =>x.GetAppliedRulesAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<long[]>()
            )).ReturnsAsync(new List<AppliedImportRuleDto>()
            {
                new AppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample1,
                    Id = 1,
                    Name = "Первый"
                },
                new AppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample2,
                    Id = 2,
                    Name = "Второй"
                },
                new AppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample3,
                    Id = 3,
                    Name = "Третий"
                },
                new AppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample4,
                    Id = 4,
                    Name = "Четвертый"
                },
                new AppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample5,
                    Id = 5,
                    Name = "Пятый"
                }
            });
            
            outsourceImportRulesClientMock.Setup(x =>x.GetAppliedRulesAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<AppliedRulesRequestDto>()
            )).ReturnsAsync(new List<OutsourceAppliedImportRuleDto>()
            {
                new OutsourceAppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample1,
                    RuleId = 1,
                    RuleName = "Первый"
                },
                new OutsourceAppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample2,
                    RuleId = 2,
                    RuleName = "Второй"
                },
                new OutsourceAppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample3,
                    RuleId = 3,
                    RuleName = "Третий"
                },
                new OutsourceAppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample4,
                    RuleId = 4,
                    RuleName = "Четвертый"
                }, new OutsourceAppliedImportRuleDto()
                {
                    DocumentBaseId = docIdExample5,
                    RuleId = 5,
                    RuleName = "Пятый"
                }
            });
            
            var paymentOrderOperations = randomizer.Create<List<PaymentOrderOperation>>();

            foreach (var paymentOrderOperation in paymentOrderOperations)
            {
                paymentOrderOperation.SettlementAccountId = 1;
            }
            
            var settlementAccountDtos = randomizer.Create<List<SettlementAccountDto>>();

            foreach (var settlementAccountDto in settlementAccountDtos)
            {
                settlementAccountDto.Id = 1;
            }
            
            var sourceBankDatas = randomizer.Create<Dictionary<int, SourceBankData>>();
            var integrationDatas = randomizer.Create<Dictionary<long, IntegrationData>>();
            
            paymentOrderOperationDaoMock.Setup(x => x.GetByBaseIdsAsync(It.IsAny<int>(), It.IsAny<long[]>()))
                .ReturnsAsync(paymentOrderOperations);
            settlementAccountClientMock
                .Setup(x => x.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int[]>()))
                .ReturnsAsync(settlementAccountDtos);
            bankDataReaderMock
                .Setup(x => x.GetBanksBySourcesAsync(It.IsAny<IUserContext>(),
                    It.IsAny<MoneySource[]>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(sourceBankDatas);
            integrationDataReaderMock.Setup(x =>
                    x.GetBankIntegrationDataAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MoneySource[]>(), It.IsAny<Dictionary<int, SourceBankData>>()))
                .ReturnsAsync(integrationDatas);
            
            var dictionaryLinkedDocument = randomizer.Create<Dictionary<long, List<LinkedDocument>>>();
            linkedDocumentsReaderMock
                .Setup(x => x.GetMapByBaseIdsAsync(It.IsAny<IUserContext>(), It.IsAny<List<long>>()))
                .ReturnsAsync(dictionaryLinkedDocument);
                
            // Act
            var result = await reader.GetMultiCurrencyTableAsync(userContextMock.Object, new MainMoneyTableRequest(), CancellationToken.None);
            
            // Assert
            Assert.That(result.Operations, Is.Not.Null);
            Assert.That(result.Operations.Count, Is.EqualTo(5));

            // Пример 1: Нет статуса банка + Не оплачено в МД
            var opExample1 = result.Operations.First(x => x.DocumentBaseId == docIdExample1);
            Assert.That(opExample1.PassThruPaymentState, Is.Null, "Пример 1: Нет статуса банка - PassThruPaymentState должен быть null");
            Assert.That(opExample1.PaidStatus, Is.EqualTo(DocumentStatus.NotPayed));

            // Пример 2: Успех банка + Оплачено в МД
            var opExample2 = result.Operations.First(x => x.DocumentBaseId == docIdExample2);
            Assert.That(opExample2.PassThruPaymentState, Is.Null, "Пример 2: Оплачено в МД - PassThruPaymentState должен быть null");
            Assert.That(opExample2.PaidStatus, Is.EqualTo(DocumentStatus.Payed));

            // Пример 3: Ошибка банка + Не оплачено в МД
            var opExample3 = result.Operations.First(x => x.DocumentBaseId == docIdExample3);
            Assert.That(opExample3.PassThruPaymentState, Is.Not.Null, "Пример 3: Ошибка банка - должен быть PassThruPaymentState");
            Assert.That(opExample3.PassThruPaymentState.Status, Is.EqualTo(InvoiceStatus.Failed));
            Assert.That(opExample3.PassThruPaymentState.Message, Is.EqualTo("Ошибка оплаты"));
            Assert.That(opExample3.PaidStatus, Is.EqualTo(DocumentStatus.NotPayed));

            // Пример 4: Ручное "Оплачено" + Банк отклонил → В МД "Не оплачено" + Банк "Ошибка"
            var opExample4 = result.Operations.First(x => x.DocumentBaseId == docIdExample4);
            Assert.That(opExample4.PassThruPaymentState, Is.Not.Null, "Пример 4: Банк отклонил - должен быть PassThruPaymentState");
            Assert.That(opExample4.PassThruPaymentState.Status, Is.EqualTo(InvoiceStatus.Failed));
            Assert.That(opExample4.PassThruPaymentState.Message, Is.EqualTo("Платеж отклонен банком"));
            Assert.That(opExample4.PaidStatus, Is.EqualTo(DocumentStatus.NotPayed));

            // Пример 5: Множественные попытки (берется последний статус)
            var opExample5 = result.Operations.First(x => x.DocumentBaseId == docIdExample5);
            Assert.That(opExample5.PassThruPaymentState, Is.Null, "Пример 5: Последняя попытка успешна - PassThruPaymentState должен быть null");
            Assert.That(opExample5.PaidStatus, Is.EqualTo(DocumentStatus.Payed));
            
            // Проверяем, что для docIdExample5 берется последний статус из банка
            var lastBankStatus = invoiceDetails
                .Where(x => x.DocumentBaseId == docIdExample5)
                .OrderByDescending(x => x.CreateDate)
                .First();
            Assert.That(lastBankStatus.Status, Is.EqualTo(InvoiceStatus.Processed));
        }
    }
}
