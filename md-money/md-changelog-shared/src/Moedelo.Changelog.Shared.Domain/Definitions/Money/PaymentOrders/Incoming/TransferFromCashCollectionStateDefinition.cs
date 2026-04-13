using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming
{
    public class TransferFromCashCollectionStateDefinition
        : IncomingPaymentOrderStateDefinition<
            TransferFromCashCollectionStateDefinition,
            TransferFromCashCollectionStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Incoming_TransferFromCashCollection;

        public class State
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

            [Display(Name = "Поступило")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

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
