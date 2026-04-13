using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase;

[InjectAsSingleton]
internal sealed class OutgoingCurrencyPurchaseOutsourceProcessor : PaymentOrderOutsourceProcessor<OutgoingCurrencyPurchaseSaveRequest>, IOutgoingCurrencyPurchaseOutsourceProcessor
{
    private readonly IOutgoingCurrencyPurchaseValidator validator;
    private readonly IOutgoingCurrencyPurchaseReader reader;
    private readonly IOutgoingCurrencyPurchaseUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public OutgoingCurrencyPurchaseOutsourceProcessor(
        IOutgoingCurrencyPurchaseValidator validator,
        IOutgoingCurrencyPurchaseReader reader,
        IOutgoingCurrencyPurchaseUpdater updater,
        ILogger<OutgoingCurrencyPurchaseOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(OutgoingCurrencyPurchaseSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<OutgoingCurrencyPurchaseSaveRequest> MapToExistentAsync(OutgoingCurrencyPurchaseSaveRequest request)
    {
        await EnrichModelAsync(request);
        
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private async Task EnrichModelAsync(OutgoingCurrencyPurchaseSaveRequest request)
    {
        if (request.ExchangeRate <= 0)
        {
            request.OperationState = OperationState.MissingExchangeRate;
            return;
        }

        if (request.ToSettlementAccountId is not > 0)
        {
            request.OperationState = OperationState.MissingCurrencySettlementAccount;
            return;
        }

        decimal? cbExchangeRate;

        try
        {
            cbExchangeRate = await exchangeRatesReader.GetByDateAndSettlementAccountIdAsync(
                request.Date,
                request.ToSettlementAccountId.Value);
        }
        catch (NotSupportedException)
        {
            request.OperationState = OperationState.MissingCurrencySettlementAccount;
            return;
        }

        if (cbExchangeRate == null)
        {
            request.OperationState = OperationState.MissingExchangeRate;
            return;
        }

        // расчеты синхронизированы с импортом:
        // https://gitlab.mdtest.org/development/md-paymentImportNetCore/blob/8526c3e9a4e120df34f4a5c19a22fba43b7cbcd8/src/apps/Moedelo.PaymentImport.Business/PaymentImport/MoneyOperations/Factories/Outgoing/OutgoingCurrencyPurchaseFactory.cs#L49
        request.TotalSum = Math.Round(request.Sum / request.ExchangeRate, 2);
        request.ExchangeRateDiff = Math.Round(request.TotalSum * cbExchangeRate.Value - request.Sum, 2);
    }

    private static OutgoingCurrencyPurchaseSaveRequest MapToExistent(
        OutgoingCurrencyPurchaseResponse existent,
        OutgoingCurrencyPurchaseSaveRequest newValues)
    {
        var result = OutgoingCurrencyPurchaseMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.TaxPostings = newValues.TaxPostings;
        result.ToSettlementAccountId = newValues.ToSettlementAccountId;
        result.ExchangeRate = newValues.ExchangeRate;
        result.TotalSum = newValues.TotalSum;
        result.ExchangeRateDiff = newValues.ExchangeRateDiff;

        return result;
    }

    protected override Task UpdateAsync(OutgoingCurrencyPurchaseSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(OutgoingCurrencyPurchaseSaveRequest request) => validator.ValidateAsync(request);
}