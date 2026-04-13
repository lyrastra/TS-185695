using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationError;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.Finances.Business.Services.Integrations;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moq;
using NUnit.Framework;
using IntegrationErrorType = Moedelo.BankIntegrations.Enums.IntegrationErrorType;

namespace Moedelo.Finances.Tests.Business
{
    [TestFixture]
    public class IntegrationErrorsServiceTests
    {
        private Fixture randomizer;
        private Mock<ILogger> logger;
        private Mock<IIntegrationErrorApiClient> integrationErrorClient;
        private IntegrationErrorsService service;

        [SetUp]
        public void Setup()
        {
            randomizer = new Fixture();
            logger = new Mock<ILogger>();
            integrationErrorClient = new Mock<IIntegrationErrorApiClient>();
            service = new IntegrationErrorsService(logger.Object, integrationErrorClient.Object);
        }

        [Test]
        public async Task GetIntegrationErrorsAsync_ShouldReturnEmptyListOfError_IfExceptionRaisesWhenCallingIntegrationClient(
            CancellationToken ctx)
        {
            //arrange
            var firmId = randomizer.Create<int>();
            integrationErrorClient.Setup(p => p.GetListAsync(
                It.IsAny<GetListIntegrationErrorRequestDto>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            //act
            var result = await service.GetIntegrationErrorsAsync(firmId, ctx);

            //assert
            result.Should().BeEquivalentTo(new List<IntegrationErrorResponse>());
        }

        [Test]
        public async Task GetIntegrationErrorsAsync_ShouldReturnListOfError_IfIntegrationSberBankClient(CancellationToken ctx)
        {
            //arrange
            var firmId = randomizer.Create<int>();
            var errors = randomizer
                .Build<IntegrationErrorDto>()
                .With(x=> x.FirmId, firmId)
                .With(x=> x.IntegrationPartnerId, IntegrationPartners.SberBank)
                .CreateMany(3).ToList();
            errors[0].ErrorType = IntegrationErrorType.SberbankSettlementWorkflowFault;
            errors[1].ErrorType = IntegrationErrorType.SberbankOfferNotSigning;
            errors[2].ErrorType = IntegrationErrorType.SberbankSettlementUnavailableError;
            var clientResponse = randomizer.Create<IntegrationErrorListDto>();
            clientResponse.Items = errors;
            integrationErrorClient.Setup(p => p.GetListAsync(
                It.IsAny<GetListIntegrationErrorRequestDto>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(clientResponse);

            //act
            var result = await service.GetIntegrationErrorsAsync(firmId, ctx).ConfigureAwait(false);

            //assert
            result.Should().BeEquivalentTo(new[] 
            {
                new IntegrationErrorResponse
                {
                    Message = errors[0].Message,
                    ErrorIds = new[] { errors[0].Id }.ToList(),
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    ErrorType = IntegrationErrorType.SberbankSettlementWorkflowFault,
                    IntegrationNotificationErrorType = IntegrationNotificationErrorType.Warning,
                    NeedLink = false
                },
                new IntegrationErrorResponse
                {
                    Message = errors[1].Message,
                    ErrorIds = new[] { errors[1].Id }.ToList(),
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    ErrorType = IntegrationErrorType.SberbankOfferNotSigning,
                    IntegrationNotificationErrorType = IntegrationNotificationErrorType.Warning,
                    NeedLink = true
                },
                new IntegrationErrorResponse
                {
                    Message = errors[2].Message,
                    ErrorIds = new[] { errors[2].Id }.ToList(),
                    IntegrationPartnerId = IntegrationPartners.SberBank,
                    ErrorType = IntegrationErrorType.SberbankSettlementUnavailableError,
                    IntegrationNotificationErrorType = IntegrationNotificationErrorType.Warning,
                    NeedLink = true
                }
            }.ToList());
        }

        [Test]
        public async Task GetIntegrationErrorsAsync_ShouldReturnListOfError_IfNeedLinkIsTrue(CancellationToken ctx)
        {
            //arrange
            var firmId = randomizer.Create<int>();
            var errors = randomizer.CreateMany<IntegrationErrorDto>(4).ToList();
            errors[0].ErrorType = IntegrationErrorType.SberbankOfferNotSigning;
            errors[1].ErrorType = IntegrationErrorType.SberbankSettlementUnavailableError;
            errors[2].ErrorType = IntegrationErrorType.SberbankSettlementNotExistsInClientInfo;
            errors[3].ErrorType = IntegrationErrorType.AlfaConsentReSigning;
            var clientResponse = randomizer.Create<IntegrationErrorListDto>();
            clientResponse.Items = errors;
            integrationErrorClient.Setup(p => p.GetListAsync(
                It.IsAny<GetListIntegrationErrorRequestDto>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(clientResponse);

            //act
            var result = await service
                .GetIntegrationErrorsAsync(firmId, ctx)
                .ConfigureAwait(false);

            //assert
            result.Count(x => x.NeedLink).Should().Be(errors.Count);
        }
        
        [Test]
        public async Task GetIntegrationErrorsAsync_ShouldReturnListOfError_DifferentIntegrationNotificationErrorTypes(
            CancellationToken ctx)
        {
            //arrange
            var firmId = randomizer.Create<int>();
            var errors = randomizer
                .Build<IntegrationErrorDto>()
                .With(x=> x.FirmId, firmId)
                .With(x=> x.IntegrationPartnerId, IntegrationPartners.Alfa)
                .CreateMany(2).ToList();
          
            errors[0].ErrorType = IntegrationErrorType.AlfaConsentReSigning;
            errors[1].ErrorType = IntegrationErrorType.AlfaConsentExpired;
            
            var clientResponse = randomizer.Create<IntegrationErrorListDto>();
            clientResponse.Items = errors;
            integrationErrorClient.Setup(p => p.GetListAsync(
                It.IsAny<GetListIntegrationErrorRequestDto>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(clientResponse);

            //act
            var result = await service
                .GetIntegrationErrorsAsync(firmId, ctx)
                .ConfigureAwait(false);
            
            //assert
            result.Should().BeEquivalentTo(new[] 
            {
                new IntegrationErrorResponse
                {
                    Message = errors[0].Message,
                    ErrorIds = new[] { errors[0].Id }.ToList(),
                    IntegrationPartnerId = IntegrationPartners.Alfa,
                    ErrorType = IntegrationErrorType.AlfaConsentReSigning,
                    IntegrationNotificationErrorType = IntegrationNotificationErrorType.Warning,
                    NeedLink = true
                },
                new IntegrationErrorResponse
                {
                    Message = errors[1].Message,
                    ErrorIds = new[] { errors[1].Id }.ToList(),
                    IntegrationPartnerId = IntegrationPartners.Alfa,
                    ErrorType = IntegrationErrorType.AlfaConsentExpired,
                    IntegrationNotificationErrorType = IntegrationNotificationErrorType.Error,
                    NeedLink = false
                }
            }.ToList());
        }
    }
}
