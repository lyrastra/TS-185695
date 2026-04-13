using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale;

[InjectAsSingleton]
internal sealed class IncomingCurrencySaleOutsourceProcessor : PaymentOrderOutsourceProcessor<IncomingCurrencySaleSaveRequest>, IIncomingCurrencySaleOutsourceProcessor
{
    private readonly IIncomingCurrencySaleValidator validator;
    private readonly IIncomingCurrencySaleReader reader;
    private readonly IIncomingCurrencySaleUpdater updater;

    public IncomingCurrencySaleOutsourceProcessor(
        IIncomingCurrencySaleValidator validator,
        IIncomingCurrencySaleReader reader,
        IIncomingCurrencySaleUpdater updater,
        ILogger<IncomingCurrencySaleOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(IncomingCurrencySaleSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<IncomingCurrencySaleSaveRequest> MapToExistentAsync(IncomingCurrencySaleSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static IncomingCurrencySaleSaveRequest MapToExistent(
        IncomingCurrencySaleResponse existent,
        IncomingCurrencySaleSaveRequest newValues)
    {
        var result = IncomingCurrencySaleMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(IncomingCurrencySaleSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(IncomingCurrencySaleSaveRequest request) => validator.ValidateAsync(request);
}