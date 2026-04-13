using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment;

[InjectAsSingleton]
internal sealed class RentPaymentOutsourceProcessor : PaymentOrderOutsourceProcessor<RentPaymentSaveRequest>, IRentPaymentOutsourceProcessor
{
    private readonly IRentPaymentValidator validator;
    private readonly IRentPaymentReader reader;
    private readonly IRentPaymentUpdater updater;

    public RentPaymentOutsourceProcessor(
        IRentPaymentValidator validator,
        IRentPaymentReader reader,
        IRentPaymentUpdater updater,
        ILogger<RentPaymentOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(RentPaymentSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }
    
    protected override async Task<RentPaymentSaveRequest> MapToExistentAsync(RentPaymentSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }
    
    private static RentPaymentSaveRequest MapToExistent(
        RentPaymentResponse existent,
        RentPaymentSaveRequest newValues)
    {
        var result = RentPaymentMapper.Map(existent);

        result.Kontragent = newValues.Kontragent;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }
    
    protected override Task UpdateAsync(RentPaymentSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(RentPaymentSaveRequest request) => validator.ValidateAsync(request);
}