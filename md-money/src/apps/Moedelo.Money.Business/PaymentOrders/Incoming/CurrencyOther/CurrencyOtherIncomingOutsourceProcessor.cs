using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther;

[InjectAsSingleton]
internal sealed class CurrencyOtherIncomingOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyOtherIncomingSaveRequest>, ICurrencyOtherIncomingOutsourceProcessor
{
    private readonly ICurrencyOtherIncomingValidator validator;
    private readonly ICurrencyOtherIncomingReader reader;
    private readonly ICurrencyOtherIncomingUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public CurrencyOtherIncomingOutsourceProcessor(
        ICurrencyOtherIncomingValidator validator,
        ICurrencyOtherIncomingReader reader,
        ICurrencyOtherIncomingUpdater updater,
        ILogger<CurrencyOtherIncomingOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader)
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyOtherIncomingSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyOtherIncomingSaveRequest> MapToExistentAsync(CurrencyOtherIncomingSaveRequest request)
    {
        await EnrichModelAsync(request);

        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        
        // На момент реализации (09.01.2024) из импорта не может прийти операция с типом "прочее валютное поступление".
        // Т. о. операцию можно получить только сменой типа.
        // Не стал добавлять недостижимый код: протестировать никак нельзя. Лучше доработать, когда станет актуально.
        throw new NotImplementedException("Ветка кода недостижима на момент запуска функционала. Релизовать аналогично OtherIncomingOutsourceProcessor");
    }
    
    private async Task EnrichModelAsync(CurrencyOtherIncomingSaveRequest request)
    {
        var exchangeRate = await exchangeRatesReader.GetByDateAndSettlementAccountIdAsync(request.Date, request.SettlementAccountId);
        if (exchangeRate == null)
        {
            request.OperationState = OperationState.MissingExchangeRate;
            return;
        }

        request.TotalSum = exchangeRate.Value * request.Sum; // в импорте (Оплата от покупателя) не округляется
    }

    protected override Task ValidateAsync(CurrencyOtherIncomingSaveRequest request) => validator.ValidateAsync(request);
    protected override Task UpdateAsync(CurrencyOtherIncomingSaveRequest request) => updater.UpdateAsync(request);
}