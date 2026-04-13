using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.ExchangeRates.ApiClient;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Requisites.Enums.SettlementAccounts;

namespace Moedelo.Money.Business.ExchangeRates;

[InjectAsSingleton(typeof(IExchangeRatesReader))]
internal sealed class ExchangeRatesReader : IExchangeRatesReader
{
    private readonly ConcurrentDictionary<DateTime, ExchangeRateValueDto[]> exchangeRateCache = new ConcurrentDictionary<DateTime, ExchangeRateValueDto[]>();
    private readonly ExchangeRatesApiClient exchangeRatesClient;
    private readonly ISettlementAccountsReader settlementAccountsReader;
        
    public ExchangeRatesReader(
        ExchangeRatesApiClient exchangeRatesClient,
        ISettlementAccountsReader settlementAccountsReader)
    {
        this.exchangeRatesClient = exchangeRatesClient;
        this.settlementAccountsReader = settlementAccountsReader;
    }

    public async Task<decimal> GetByDateAndCurrencyAsync(DateTime date, Currency currency)
    {
        if (exchangeRateCache.TryGetValue(date, out var rates) == false)
        {
            var exchangeRates = await exchangeRatesClient.GetAsync(new[] { date });
            rates = exchangeRateCache.GetOrAdd(date, exchangeRates.SelectMany(x => x.Values).ToArray());
        }

        if (rates == null)
        {
            throw new CurrencyRateNotFoundException(date);
        }

        var byCurrency = rates.FirstOrDefault(x => x.Currency == currency.ToString())?.Value;
        if (byCurrency == null)
        {
            throw new CurrencyRateNotFoundException(currency, date);
        }

        return byCurrency.Value;
    }

    public async Task<bool> ExistsRateAsync(DateTime date, Currency currency)
    {
        try
        {
            await GetByDateAndCurrencyAsync(date, currency);
            return true;
        }
        catch (CurrencyRateNotFoundException)
        {
            return false;
        }
    }

    public async Task<decimal?> GetByDateAndSettlementAccountIdAsync(DateTime date, int settlementAccountId)
    {
        var settlementAccount = await settlementAccountsReader.GetByIdAsync(settlementAccountId);
        if (settlementAccount == null)
        {
            throw new NotSupportedException("Попытка получить курс ЦБ для несуществующего счета");
        }

        var currency = settlementAccount.Currency;
        if (currency is Currency.RUB or Currency.RUR)
        {
            throw new NotSupportedException("Попытка получить курс ЦБ для рублевого счета");
        }

        try
        {
            return await GetByDateAndCurrencyAsync(date, settlementAccount.Currency);
        }
        catch (CurrencyRateNotFoundException)
        {
            return null;
        }
    }
}