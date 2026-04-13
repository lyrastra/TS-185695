using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest;

[InjectAsSingleton(typeof(IAccrualOfInterestOutsourceProcessor))]
internal class AccrualOfInterestOutsourceProcessor : PaymentOrderOutsourceProcessor<AccrualOfInterestSaveRequest>, IAccrualOfInterestOutsourceProcessor
{
    private readonly IAccrualOfInterestValidator validator;
    private readonly IAccrualOfInterestReader reader;
    private readonly IAccrualOfInterestUpdater updater;

    public AccrualOfInterestOutsourceProcessor(
        IAccrualOfInterestValidator validator,
        IAccrualOfInterestReader reader,
        IAccrualOfInterestUpdater updater,
        ILogger<AccrualOfInterestOutsourceProcessor> logger)
    : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(AccrualOfInterestSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<AccrualOfInterestSaveRequest> MapToExistentAsync(AccrualOfInterestSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static AccrualOfInterestSaveRequest MapToExistent(
        AccrualOfInterestResponse existent,
        AccrualOfInterestSaveRequest newValues)
    {
        var result = AccrualOfInterestMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.TaxationSystemType = newValues.TaxationSystemType;
        result.TaxPostings = newValues.TaxPostings;

        return result;
    }

    protected override Task UpdateAsync(AccrualOfInterestSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(AccrualOfInterestSaveRequest request) => validator.ValidateAsync(request);
}