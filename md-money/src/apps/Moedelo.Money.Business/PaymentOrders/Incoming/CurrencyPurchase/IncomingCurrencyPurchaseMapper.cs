using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.ExchangeRates;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IncomingCurrencyPurchaseToDtoMapper))]
    internal sealed class IncomingCurrencyPurchaseToDtoMapper
    {
        private readonly IExchangeRatesReader exchangeRatesReader;
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public IncomingCurrencyPurchaseToDtoMapper(
            IExchangeRatesReader exchangeRatesReader,
            ISettlementAccountsReader settlementAccountsReader)
        {
            this.exchangeRatesReader = exchangeRatesReader;
            this.settlementAccountsReader = settlementAccountsReader;
        }

        /// <summary>
        /// Преобразует request в dto-модель для сохранения через API
        /// </summary>
        public async Task<IncomingCurrencyPurchaseDto> MapAsync(IncomingCurrencyPurchaseSaveRequest request)
        {
            // р/сч уже загружается в рамках одного web-запроса для валидации
            // подумать, как избавиться от этого (возможно, завести некую бизнес-модель)
            var settlementAccount = await settlementAccountsReader.GetByIdAsync(
                request.SettlementAccountId).ConfigureAwait(false);

            var exchangeRate = await exchangeRatesReader.GetByDateAndCurrencyAsync(
                request.Date,
                settlementAccount.Currency).ConfigureAwait(false);

            return new IncomingCurrencyPurchaseDto
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                DocumentBaseId = request.DocumentBaseId,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId,
                TotalSum = Math.Round(request.Sum * exchangeRate, 2),
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                ProvideInAccounting = request.ProvideInAccounting,
                OutsourceState = request.OutsourceState,
            };
        }
    }

    internal static class IncomingCurrencyPurchaseMapper
    {
        internal static IncomingCurrencyPurchaseSaveRequest MapToSaveRequest(IncomingCurrencyPurchaseResponse response)
        {
            return new IncomingCurrencyPurchaseSaveRequest
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                DocumentBaseId = response.DocumentBaseId,
                SettlementAccountId = response.SettlementAccountId,
                FromSettlementAccountId = response.FromSettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static IncomingCurrencyPurchaseSaveRequest MapToSaveRequest(IncomingCurrencyPurchaseImportRequest request)
        {
            return new IncomingCurrencyPurchaseSaveRequest
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                ProvideInAccounting = true,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }
    }
}