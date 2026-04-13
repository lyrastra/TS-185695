using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee;

[InjectAsSingleton(typeof(ICurrencyBankFeeOutsourceProcessor))]
internal class CurrencyBankFeeOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyBankFeeSaveRequest>, ICurrencyBankFeeOutsourceProcessor
{
    private readonly ICurrencyBankFeeValidator validator;
    private readonly ICurrencyBankFeeReader reader;
    private readonly ICurrencyBankFeeUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public CurrencyBankFeeOutsourceProcessor(
        ICurrencyBankFeeValidator validator,
        ICurrencyBankFeeReader reader,
        ICurrencyBankFeeUpdater updater,
        ILogger<CurrencyBankFeeOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyBankFeeSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyBankFeeSaveRequest> MapToExistentAsync(CurrencyBankFeeSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    protected override async Task<CurrencyBankFeeSaveRequest> OnChangeTypeAsync(CurrencyBankFeeSaveRequest request)
    {
        var exchangeRate = await exchangeRatesReader.GetByDateAndSettlementAccountIdAsync(request.Date, request.SettlementAccountId);
        if (exchangeRate == null)
        {
            request.OperationState = OperationState.MissingExchangeRate;
        }
        else
        {
            request.TotalSum = exchangeRate.Value * request.Sum; // в импорте не округляется
        }

        return request;
    }

    private static CurrencyBankFeeSaveRequest MapToExistent(
        CurrencyBankFeeResponse existent,
        CurrencyBankFeeSaveRequest newValues)
    {
        var result = CurrencyBankFeeMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        // считаем, что сумма в рублях заполнена импортом result.TotalSum = newValues.TotalSum;

        return result;
    }

    protected override Task UpdateAsync(CurrencyBankFeeSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(CurrencyBankFeeSaveRequest request) => validator.ValidateAsync(request);
}