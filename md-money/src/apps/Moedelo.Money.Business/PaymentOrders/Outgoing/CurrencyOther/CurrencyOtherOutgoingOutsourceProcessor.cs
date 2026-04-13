using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther;

[InjectAsSingleton]
internal sealed class CurrencyOtherOutgoingOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyOtherOutgoingSaveRequest>, ICurrencyOtherOutgoingOutsourceProcessor
{
    private readonly ICurrencyOtherOutgoingValidator validator;
    private readonly ICurrencyOtherOutgoingReader reader;
    private readonly ICurrencyOtherOutgoingUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public CurrencyOtherOutgoingOutsourceProcessor(
        ICurrencyOtherOutgoingValidator validator,
        ICurrencyOtherOutgoingReader reader,
        ICurrencyOtherOutgoingUpdater updater,
        ILogger<CurrencyOtherOutgoingOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader)
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyOtherOutgoingSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyOtherOutgoingSaveRequest> MapToExistentAsync(CurrencyOtherOutgoingSaveRequest request)
    {
        await EnrichModelAsync(request);

        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        
        // На момент реализации (28.12.2023) из импорта не может прийти операция с типом "прочее валютное списание".
        // Т. о. операцию можно получить только сменой типа.
        // Не стал добавлять недостижимый код: протестировать никак нельзя. Лучше доработать, когда станет актуально.
        throw new NotImplementedException("Ветка кода недостижима на момент запуска функционала. Релизовать аналогично OtherOutgoingOutsourceProcessor");
    }
    
    private async Task EnrichModelAsync(CurrencyOtherOutgoingSaveRequest request)
    {
        var exchangeRate = await exchangeRatesReader.GetByDateAndSettlementAccountIdAsync(request.Date, request.SettlementAccountId);
        if (exchangeRate == null)
        {
            request.OperationState = OperationState.MissingExchangeRate;
            return;
        }

        request.TotalSum = exchangeRate.Value * request.Sum; // в импорте (Оплата поставщику) не округляется
    }

    protected override Task ValidateAsync(CurrencyOtherOutgoingSaveRequest request) => validator.ValidateAsync(request);
    protected override Task UpdateAsync(CurrencyOtherOutgoingSaveRequest request) => updater.UpdateAsync(request);
}