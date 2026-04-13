using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperationLegacy;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;
using Moedelo.Finances.Business.Services.Integrations;
using Moedelo.Finances.Business.Services.Integrations.PaymentOrderCreators;
using Moedelo.Finances.Domain.Enums.Integrations;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Moedelo.Finances.Tests.Business.Integration
{
    [TestFixture]
    public class IntegrationPaymentOrderSenderTests
    {
        private Fixture randomizer;
        private Mock<IUserContext> userContextMock;
        private Mock<IIntegrationPaymentOrderCreator> integrationPaymentOrderCreatorMock;
        private Mock<ILogger> loggerMock;
        private Mock<IBanksApiClient> banksApiClientMock;
        private Mock<ISettlementAccountClient> settlementAccountClientMock;
        private Mock<IBankOperationApiClient> bankOperationApiClientMock;
        private Mock<IPaymentOrderOperationReader> paymentOrderOperationReaderMock;
        private Mock<IIntegrationAccPaymentOrderCreator> integrationAccPaymentOrderCreatorMock;
        private Mock<IIntegrationBizPaymentOrderCreator> integrationBizPaymentOrderCreatorMock;
        private Mock<IBankIntegrationsDataInformationClient> integrationsDataInformationClientMock;
        private Mock<IBankOperationsApiClient> bankOperationsApiClientMock;
        private IntegrationPaymentOrderSender integrationPaymentOrderSender;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            randomizer = new Fixture();
            userContextMock = new Mock<IUserContext>();
            integrationPaymentOrderCreatorMock = new Mock<IIntegrationPaymentOrderCreator>();
        }

        [SetUp]
        public void Setup()
        {
            loggerMock = new Mock<ILogger>();
            banksApiClientMock = new Mock<IBanksApiClient>();
            settlementAccountClientMock = new Mock<ISettlementAccountClient>();
            bankOperationApiClientMock = new Mock<IBankOperationApiClient>();
            paymentOrderOperationReaderMock = new Mock<IPaymentOrderOperationReader>();
            integrationAccPaymentOrderCreatorMock = new Mock<IIntegrationAccPaymentOrderCreator>();
            integrationBizPaymentOrderCreatorMock = new Mock<IIntegrationBizPaymentOrderCreator>();
            integrationsDataInformationClientMock = new Mock<IBankIntegrationsDataInformationClient>();
            bankOperationsApiClientMock = new Mock<IBankOperationsApiClient>();

            integrationPaymentOrderSender = new IntegrationPaymentOrderSender(
                loggerMock.Object,
                banksApiClientMock.Object,
                settlementAccountClientMock.Object,
                bankOperationApiClientMock.Object,
                paymentOrderOperationReaderMock.Object,
                integrationAccPaymentOrderCreatorMock.Object,
                integrationBizPaymentOrderCreatorMock.Object,
                integrationsDataInformationClientMock.Object,
                bankOperationsApiClientMock.Object
            );
        }

        [Test]
        public async Task SendBankInvoiceAsync_WithOutgoingOperations_ReturnsExpectedResponse()
        {
            // Arrange
            var request = new SendBankInvoiceRequest
            {
                OperationId = 1,
                BackUrl = "https://backurl.example",
                SourceType = SendBankInvoiceSourceType.Web
            };
            
            userContextMock
                .Setup(x => x.HasAllRuleAsync(AccessRule.BizPlatform))
                .ReturnsAsync(false);

            var accounts = randomizer.Create<List<SettlementAccountDto>>();
            var bankId = 1;
            var settlementAccountId = 1;
            accounts.ForEach(account => account.BankId = bankId++);
            accounts.ForEach(account => account.Id = settlementAccountId++);
            
            settlementAccountClientMock.Setup(x => x.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IReadOnlyCollection<int>>()))
                .ReturnsAsync(accounts);
            
            var banks = randomizer.Create<List<BankDto>>();
            bankId = 1;
            banks.ForEach(bank => bank.Id = bankId++);
            banksApiClientMock.Setup(x => x.GetByIdsAsync(It.IsAny<IReadOnlyCollection<int>>()))
                .ReturnsAsync(banks);

            var intSummaryBySettlementsResponseDto = randomizer.Create<IntSummaryBySettlementsResponseDto>();
            integrationsDataInformationClientMock.Setup(x => x.GetIntSummaryBySettlementsAsync(It.IsAny<IntSummaryBySettlementsRequestDto>())).ReturnsAsync(intSummaryBySettlementsResponseDto);
            
            var operations = randomizer.Create<List<PaymentOrderOperation>>();
            paymentOrderOperationReaderMock.Setup(x => x.GetByBaseIdsAsync(It.IsAny<int>(), It.IsAny<IReadOnlyCollection<long>>())).ReturnsAsync(operations);
            settlementAccountId = 1;
            operations.ForEach(operation => operation.SettlementAccountId = settlementAccountId++);

            var contextExtraDataMock = new Mock<IUserContextExtraData>();
            var contextExtraData = contextExtraDataMock.Object;

            userContextMock.Setup(x => x.GetContextExtraDataAsync()).ReturnsAsync(contextExtraData);
            userContextMock
                .Setup(x => x.HasAllRuleAsync(AccessRule.UsnAccountantTariff))
                .ReturnsAsync(true);
            
            var paymentOrderDto = randomizer.Create<PaymentOrderDto>();
            integrationAccPaymentOrderCreatorMock
                .Setup(x => x.CreateAsync(It.IsAny<Guid>(), It.IsAny<object>(), It.IsAny<IntegrationPartners>()))
                .ReturnsAsync(paymentOrderDto);
            
            var integrationResponse = new IntegrationResponseDto<SendBankInvoiceResponseDto>(new SendBankInvoiceResponseDto())
                {
                    StatusCode = IntegrationResponseStatusCode.Ok,
                    Data = new SendBankInvoiceResponseDto
                    {
                        InvoiceResponseStatusCode = InvoiceResponseStatusCode.Ok,
                        ErrorMessageForUser = "Success",
                        InvoiceUrl = "https://invoice.url"
                    }
                };

            bankOperationsApiClientMock
                .Setup(x => x.SendInvoiceAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SendBankInvoiceRequestDto>()))
                .ReturnsAsync(integrationResponse);
            
            // Act
            var result = await integrationPaymentOrderSender.SendBankInvoiceAsync(userContextMock.Object, request);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(IntegrationResponseStatusCode.Ok, result.StatusCode);
            ClassicAssert.AreEqual(InvoiceResponseStatusCode.Ok, result.InvoiceStatusCode);
            ClassicAssert.AreEqual("Success", result.Message);
            ClassicAssert.AreEqual("https://invoice.url", result.InvoiceUrl);
        }
    }
}