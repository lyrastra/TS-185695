using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.SettlementAccounts;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(CurrencyTransferFromAccountToDtoMapper))]
    internal sealed class CurrencyTransferFromAccountToDtoMapper
    {
        private readonly IExchangeRatesReader exchangeRatesReader;
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public CurrencyTransferFromAccountToDtoMapper(
            IExchangeRatesReader exchangeRatesReader,
            ISettlementAccountsReader settlementAccountsReader)
        {
            this.exchangeRatesReader = exchangeRatesReader;
            this.settlementAccountsReader = settlementAccountsReader;
        }

        /// <summary>
        /// Преобразует saveRequest в dto-модель для сохранения через API
        /// </summary>
        public async Task<CurrencyTransferFromAccountDto> MapAsync(CurrencyTransferFromAccountSaveRequest request)
        {
            // р/сч уже загружается в рамках одного web-запроса для валидации
            // подумать, как избавиться от этого (возможно, завести некую бизнес-модель)
            var settlementAccount = await settlementAccountsReader.GetByIdAsync(
                request.SettlementAccountId).ConfigureAwait(false);

            var exchangeRate = await exchangeRatesReader.GetByDateAndCurrencyAsync(
                request.Date,
                settlementAccount.Currency).ConfigureAwait(false);

            return new CurrencyTransferFromAccountDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                TotalSum = Math.Round(request.Sum * exchangeRate, 2),
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }
    }
}