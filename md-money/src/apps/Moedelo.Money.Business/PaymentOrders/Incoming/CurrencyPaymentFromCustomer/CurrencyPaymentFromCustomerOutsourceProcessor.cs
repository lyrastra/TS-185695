using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;

[InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerOutsourceProcessor))]
internal sealed class CurrencyPaymentFromCustomerOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyPaymentFromCustomerSaveRequest>, ICurrencyPaymentFromCustomerOutsourceProcessor
{
    private readonly ICurrencyPaymentFromCustomerValidator validator;
    private readonly ICurrencyPaymentFromCustomerReader reader;
    private readonly ICurrencyPaymentFromCustomerUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public CurrencyPaymentFromCustomerOutsourceProcessor(
        ICurrencyPaymentFromCustomerValidator validator,
        ICurrencyPaymentFromCustomerReader reader,
        ICurrencyPaymentFromCustomerUpdater updater,
        ILogger<CurrencyPaymentFromCustomerOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyPaymentFromCustomerSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyPaymentFromCustomerSaveRequest> MapToExistentAsync(CurrencyPaymentFromCustomerSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    protected override async Task<CurrencyPaymentFromCustomerSaveRequest> OnChangeTypeAsync(CurrencyPaymentFromCustomerSaveRequest request)
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

    private static CurrencyPaymentFromCustomerSaveRequest MapToExistent(
        CurrencyPaymentFromCustomerResponse existent,
        CurrencyPaymentFromCustomerSaveRequest newValues)
    {
        var result = CurrencyPaymentFromCustomerMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings;
        result.Kontragent = newValues.Kontragent;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = null;
            result.LinkedDocuments = Array.Empty<DocumentLinkSaveRequest>();
        }

        return result;
    }

    protected override Task UpdateAsync(CurrencyPaymentFromCustomerSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(CurrencyPaymentFromCustomerSaveRequest request) => validator.ValidateAsync(request);
}