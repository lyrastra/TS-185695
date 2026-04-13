using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Enums;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing
{
    public class OutgoingCurrencyPurchaseStateDefinition
        : OutgoingPaymentOrderStateDefinition<
             OutgoingCurrencyPurchaseStateDefinition,
             OutgoingCurrencyPurchaseStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Outgoing_CurrencyPurchase;

        public class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Сумма")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Курс банка")]
            public MoneySum ExchangeRate { get; set; }

            [Display(Name = "Курсовая разница")]
            public MoneySum ExchangeRateDiff { get; set; }

            [Display(Name = "Итого")]
            public MoneySum TotalSum { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

            [Display(Name = "Расчетный счет")]
            public string SettlementAccountNumber { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int SettlementAccountId { get; set; }

            [Display(Name = "Зачислить на счет")]
            public string ToSettlementAccountNumber { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int ToSettlementAccountId { get; set; }

            [Display(Name = "Проведено вручную в налоговом учете")]
            public bool IsManualTaxPostings { get; set; }

            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }
        }

        public override long GetEntityId(State state)
        {
            return state.DocumentBaseId;
        }

        protected override string GetEntityName(State state)
        {
            return $"{EntityTypeName} №{state.Number} от {state.Date:dd.MM.yyyy}";
        }
    }
}
