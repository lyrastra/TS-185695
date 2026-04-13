using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase;

[InjectAsSingleton]
internal sealed class IncomingCurrencyPurchaseOutsourceProcessor : PaymentOrderOutsourceProcessor<IncomingCurrencyPurchaseSaveRequest>, IIncomingCurrencyPurchaseOutsourceProcessor
{
    private readonly IIncomingCurrencyPurchaseValidator validator;
    private readonly IIncomingCurrencyPurchaseReader reader;
    private readonly IIncomingCurrencyPurchaseUpdater updater;

    public IncomingCurrencyPurchaseOutsourceProcessor(
        IIncomingCurrencyPurchaseValidator validator,
        IIncomingCurrencyPurchaseReader reader,
        IIncomingCurrencyPurchaseUpdater updater,
        ILogger<IncomingCurrencyPurchaseOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(IncomingCurrencyPurchaseSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<IncomingCurrencyPurchaseSaveRequest> MapToExistentAsync(IncomingCurrencyPurchaseSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static IncomingCurrencyPurchaseSaveRequest MapToExistent(
        IncomingCurrencyPurchaseResponse existent,
        IncomingCurrencyPurchaseSaveRequest newValues)
    {
        var result = IncomingCurrencyPurchaseMapper.MapToSaveRequest(existent);

        result.ProvideInAccounting = newValues.ProvideInAccounting;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(IncomingCurrencyPurchaseSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(IncomingCurrencyPurchaseSaveRequest request) => validator.ValidateAsync(request);
}