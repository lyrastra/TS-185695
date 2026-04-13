using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing
{
    public class BankFeeStateDefinition
        : OutgoingPaymentOrderStateDefinition<
            BankFeeStateDefinition,
            BankFeeStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Outgoing_BankFee;

        public sealed class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Расчетный счет")]
            public string SettlementAccountNumber { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int SettlementAccountId { get; set; }

            [Display(Name = "К оплате")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

            [Display(Name = "Учесть в")]
            public string TaxationSystemType { get; set; }

            [Display(Name = "Патент")]
            public string PatentName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? PatentId { get; set; }

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
