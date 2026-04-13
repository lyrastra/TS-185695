using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue;

[InjectAsSingleton(typeof(IRetailRevenueOutsourceProcessor))]
internal class RetailRevenueOutsourceProcessor : PaymentOrderOutsourceProcessor<RetailRevenueSaveRequest>, IRetailRevenueOutsourceProcessor
{
    private readonly IRetailRevenueValidator validator;
    private readonly IRetailRevenueReader reader;
    private readonly IRetailRevenueUpdater updater;

    public RetailRevenueOutsourceProcessor(
        IRetailRevenueValidator validator,
        IRetailRevenueReader reader,
        IRetailRevenueUpdater updater,
        ILogger<RetailRevenueOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(RetailRevenueSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<RetailRevenueSaveRequest> MapToExistentAsync(RetailRevenueSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static RetailRevenueSaveRequest MapToExistent(
        RetailRevenueResponse existent,
        RetailRevenueSaveRequest newValues)
    {
        var result = RetailRevenueMapper.MapToSaveRequest(existent);

        result.TaxPostings ??= new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };
        result.IsMediation = newValues.IsMediation;
        result.TaxationSystemType = newValues.TaxationSystemType;
        result.PatentId = newValues.PatentId;
        result.SaleDate = newValues.SaleDate;
        result.AcquiringCommissionDate = newValues.AcquiringCommissionDate;
        result.AcquiringCommissionSum = newValues.AcquiringCommissionSum;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(RetailRevenueSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(RetailRevenueSaveRequest request) => validator.ValidateAsync(request);
}