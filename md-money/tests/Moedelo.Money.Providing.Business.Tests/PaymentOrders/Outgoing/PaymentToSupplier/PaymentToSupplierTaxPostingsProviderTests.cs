using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Estate;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.NdsRatePeriods;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.TaxPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.Stock;
using Moedelo.Money.Providing.Business.TaxationSystems;
using Moedelo.Money.Providing.Business.TaxPostings;
using Moedelo.ReceiptStatement.ApiClient.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Clients;
using Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Stock.Enums;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using Moq;
using NUnit.Framework;

namespace Moedelo.Money.Providing.Business.Tests.PaymentOrders.Outgoing.PaymentToSupplier;

[TestFixture]
public class PaymentToSupplierTaxPostingsProviderTests
{
    private const long PaymentBaseId = 12;
    private const string PaymentNumber = "12/2";
    private static readonly DateTime PaymentDate = new DateTime(2018, 12, 15);

    private const long WaybillBaseId = 21;
    private const string WaybillNumber = "243";
    private const long MaterialStockId = 47;

    [Test]
    public async Task ProvideAsync_ManualTax_WithLinkedDocuments_CallsDeleteByRelatedDocumentExcludingOwnerBeforeSave()
    {
        var fixture = CreateConfiguredFixture();
        var taxUsn = fixture.Freeze<Mock<ITaxPostingsUsnClient>>();

        var callOrder = new List<string>();
        taxUsn
            .Setup(c => c.DeleteByRelatedDocumentIdNotInDocumentIdAsync(
                It.IsAny<FirmId>(),
                It.IsAny<UserId>(),
                It.IsAny<long>()))
            .Callback(() => callOrder.Add("delete"))
            .Returns(Task.CompletedTask);
        taxUsn
            .Setup(c => c.SaveAsync(
                It.IsAny<FirmId>(),
                It.IsAny<UserId>(),
                It.IsAny<IReadOnlyCollection<TaxPostingUsnDto>>()))
            .Callback(() => callOrder.Add("save"))
            .Returns(Task.CompletedTask);

        var provider = CreateProvider(fixture);
        var request = CreateProvideRequest(PaymentDate.AddDays(5), isManualTaxPostings: true);

        await provider.ProvideAsync(request);

        callOrder.Should().Equal("delete", "save");
        taxUsn.Verify(
            c => c.DeleteByRelatedDocumentIdNotInDocumentIdAsync(
                It.IsAny<FirmId>(),
                It.IsAny<UserId>(),
                PaymentBaseId),
            Times.Once);
        taxUsn.Verify(
            c => c.SaveAsync(
                It.IsAny<FirmId>(),
                It.IsAny<UserId>(),
                It.Is<IReadOnlyCollection<TaxPostingUsnDto>>(a => a.Count == 1)),
            Times.Once);
    }

    [Test]
    public async Task ProvideAsync_AutoTax_WithLinkedDocuments_DoesNotCallDeleteByRelatedDocumentExcludingOwner()
    {
        var fixture = CreateConfiguredFixture();
        var taxUsn = fixture.Freeze<Mock<ITaxPostingsUsnClient>>();
        taxUsn
            .Setup(c => c.DeleteByRelatedDocumentIdAsync(It.IsAny<FirmId>(), It.IsAny<UserId>(), It.IsAny<long>()))
            .Returns(Task.CompletedTask);
        taxUsn
            .Setup(c => c.SaveAsync(
                It.IsAny<FirmId>(),
                It.IsAny<UserId>(),
                It.IsAny<IReadOnlyCollection<TaxPostingUsnDto>>()))
            .Returns(Task.CompletedTask);

        var provider = CreateProvider(fixture);
        var request = CreateProvideRequest(PaymentDate.AddDays(5), isManualTaxPostings: false);

        await provider.ProvideAsync(request);

        taxUsn.Verify(
            c => c.DeleteByRelatedDocumentIdNotInDocumentIdAsync(
                It.IsAny<FirmId>(),
                It.IsAny<UserId>(),
                It.IsAny<long>()),
            Times.Never);
    }

    private static IFixture CreateConfiguredFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());

        fixture.Freeze<Mock<IExecutionInfoContextAccessor>>()
            .Setup(a => a.ExecutionInfoContext)
            .Returns(new ExecutionInfoContext
            {
                FirmId = new FirmId(1),
                UserId = new UserId(2)
            });

        fixture.Freeze<Mock<ITaxationSystemApiClient>>()
            .Setup(t => t.GetByYearAsync(It.IsAny<FirmId>(), It.IsAny<UserId>(), It.IsAny<int>()))
            .ReturnsAsync(new TaxationSystemDto
            {
                Id = 1,
                StartYear = 2018,
                IsUsn = true,
                UsnType = UsnType.ProfitAndOutgo,
                IsOsno = false,
                IsEnvd = false
            });

        fixture.Freeze<Mock<IInventoryCardApiClient>>()
            .Setup(a => a.GetByBaseIdsAsync(It.IsAny<FirmId>(), It.IsAny<UserId>(), It.IsAny<IReadOnlyCollection<long>>()))
            .ReturnsAsync(new List<InventoryCardDto>());

        fixture.Freeze<Mock<IReceiptStatementApiClient>>();

        fixture.Freeze<Mock<IProductApiClient>>()
            .Setup(a => a.GetByIdsAsync(It.IsAny<FirmId>(), It.IsAny<UserId>(), It.IsAny<IReadOnlyCollection<long>>()))
            .ReturnsAsync([new StockProductDto { Id = MaterialStockId, Type = StockProductTypeEnum.Material }]);

        fixture.Freeze<Mock<INdsRatePeriodsApiClient>>()
            .Setup(a => a.GetAsync(It.IsAny<GetNdsRatePeriodsFilterDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        fixture.Freeze<Mock<IBaseDocumentsCommandWriter>>()
            .Setup(w => w.WriteAsync(It.IsAny<SetTaxStatusCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return fixture;
    }

    private static PaymentToSupplierTaxPostingsProvider CreateProvider(IFixture fixture)
    {
        var contextAccessor = fixture.Create<IExecutionInfoContextAccessor>();
        var taxUsn = fixture.Create<ITaxPostingsUsnClient>();

        return new PaymentToSupplierTaxPostingsProvider(
            new TaxationSystemReader(contextAccessor, fixture.Create<ITaxationSystemApiClient>()),
            new TaxPostingsRemover(
                contextAccessor,
                taxUsn,
                fixture.Create<ITaxPostingsOsnoClient>(),
                fixture.Create<ITaxPostingsPsnClient>(),
                fixture.Create<IBaseDocumentsCommandWriter>()),
            new UsnPostingsSaver(contextAccessor, taxUsn),
            fixture.Create<IBaseDocumentsCommandWriter>(),
            new InventoryCardReader(contextAccessor, fixture.Create<IInventoryCardApiClient>()),
            new ReceiptStatementReader(fixture.Create<IReceiptStatementApiClient>()),
            new StockProductReader(contextAccessor, fixture.Create<IProductApiClient>()),
            new NdsRatePeriodsReader(fixture.Create<INdsRatePeriodsApiClient>()));
    }

    private static PaymentToSupplierTaxPostingsProvideRequest CreateProvideRequest(
        DateTime waybillDate,
        bool isManualTaxPostings)
    {
        var sum = 500m;
        var waybill = new PurchasesWaybill
        {
            DocumentBaseId = WaybillBaseId,
            Date = waybillDate,
            Number = WaybillNumber,
            Sum = sum,
            TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType.Usn,
            Items = new[]
            {
                new WaybillItem
                {
                    StockProductId = MaterialStockId,
                    SumWithNds = sum,
                    SumWithoutNds = sum
                }
            }
        };

        return new PaymentToSupplierTaxPostingsProvideRequest
        {
            DocumentBaseId = PaymentBaseId,
            Date = PaymentDate,
            Number = PaymentNumber,
            Sum = sum,
            Kontragent = new Kontragent { Name = "Иванов" },
            BaseDocuments = new[]
            {
                new BaseDocument { Id = WaybillBaseId, Type = LinkedDocumentType.Waybill }
            },
            Waybills = new[] { waybill },
            Statements = Array.Empty<PurchasesStatement>(),
            Upds = Array.Empty<PurchasesUpd>(),
            DocumentLinks = new[]
            {
                new DocumentLink
                {
                    DocumentBaseId = WaybillBaseId,
                    Type = LinkedDocumentType.Waybill,
                    LinkSum = sum
                }
            },
            IsPaid = true,
            IsManualTaxPostings = isManualTaxPostings,
            IsBadOperationState = false
        };
    }
}