using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale;

[InjectAsSingleton]
internal sealed class OutgoingCurrencySaleOutsourceProcessor : PaymentOrderOutsourceProcessor<OutgoingCurrencySaleSaveRequest>, IOutgoingCurrencySaleOutsourceProcessor
{
    private readonly IOutgoingCurrencySaleValidator validator;
    private readonly IOutgoingCurrencySaleReader reader;
    private readonly IOutgoingCurrencySaleUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public OutgoingCurrencySaleOutsourceProcessor(
        IOutgoingCurrencySaleValidator validator,
        IOutgoingCurrencySaleReader reader,
        IOutgoingCurrencySaleUpdater updater,
        ILogger<OutgoingCurrencySaleOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(OutgoingCurrencySaleSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<OutgoingCurrencySaleSaveRequest> MapToExistentAsync(OutgoingCurrencySaleSaveRequest request)
    {
        await EnrichModelAsync(request);
        
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private async Task EnrichModelAsync(OutgoingCurrencySaleSaveRequest request)
    {
        if (request.ExchangeRate <= 0)
        {
            request.OperationState = OperationState.MissingExchangeRate;
            return;
        }
        
        var cbExchangeRate = await exchangeRatesReader.GetByDateAndSettlementAccountIdAsync(request.Date, request.SettlementAccountId);
        if (cbExchangeRate == null)
        {
            request.OperationState = OperationState.MissingExchangeRate;
            return;
        }

        // в импорте не округляются:
        request.TotalSum = request.ExchangeRate * request.Sum;
        request.ExchangeRateDiff = request.Sum * (request.ExchangeRate - cbExchangeRate.Value);
    }

    private static OutgoingCurrencySaleSaveRequest MapToExistent(
        OutgoingCurrencySaleResponse existent,
        OutgoingCurrencySaleSaveRequest newValues)
    {
        var result = OutgoingCurrencySaleMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.TaxPostings = newValues.TaxPostings;
        result.ToSettlementAccountId = newValues.ToSettlementAccountId;
        result.ExchangeRate = newValues.ExchangeRate;
        result.TotalSum = newValues.TotalSum;
        result.ExchangeRateDiff = newValues.ExchangeRateDiff;
        

        return result;
    }

    protected override Task UpdateAsync(OutgoingCurrencySaleSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(OutgoingCurrencySaleSaveRequest request) => validator.ValidateAsync(request);
}