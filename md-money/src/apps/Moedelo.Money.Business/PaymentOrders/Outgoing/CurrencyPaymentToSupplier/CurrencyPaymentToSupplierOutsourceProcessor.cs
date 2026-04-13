using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

[InjectAsSingleton]
internal sealed class CurrencyPaymentToSupplierOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyPaymentToSupplierSaveRequest>, ICurrencyPaymentToSupplierOutsourceProcessor
{
    private readonly ICurrencyPaymentToSupplierValidator validator;
    private readonly ICurrencyPaymentToSupplierReader reader;
    private readonly ICurrencyPaymentToSupplierUpdater updater;
    private readonly IExchangeRatesReader exchangeRatesReader;

    public CurrencyPaymentToSupplierOutsourceProcessor(
        ICurrencyPaymentToSupplierValidator validator,
        ICurrencyPaymentToSupplierReader reader,
        ICurrencyPaymentToSupplierUpdater updater,
        ILogger<CurrencyPaymentToSupplierOutsourceProcessor> logger,
        IExchangeRatesReader exchangeRatesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.exchangeRatesReader = exchangeRatesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyPaymentToSupplierSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyPaymentToSupplierSaveRequest> MapToExistentAsync(CurrencyPaymentToSupplierSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    protected override async Task<CurrencyPaymentToSupplierSaveRequest> OnChangeTypeAsync(CurrencyPaymentToSupplierSaveRequest request)
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

    private static CurrencyPaymentToSupplierSaveRequest MapToExistent(
        CurrencyPaymentToSupplierResponse existent,
        CurrencyPaymentToSupplierSaveRequest newValues)
    {
        var result = CurrencyPaymentToSupplierMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings;
        result.Kontragent = newValues.Kontragent;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        
        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = null;
            result.DocumentLinks = Array.Empty<DocumentLinkSaveRequest>();
        }

        return result;
    }

    protected override Task UpdateAsync(CurrencyPaymentToSupplierSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(CurrencyPaymentToSupplierSaveRequest request) => validator.ValidateAsync(request);
}