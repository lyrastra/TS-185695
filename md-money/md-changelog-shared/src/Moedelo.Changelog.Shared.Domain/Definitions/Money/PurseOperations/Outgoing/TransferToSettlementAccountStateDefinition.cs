using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Incoming;
using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing
{
    public class TransferToSettlementAccountStateDefinition
        : IncomingPurseOperationStateDefinition<
            TransferToSettlementAccountStateDefinition,
            TransferToSettlementAccountStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PurseOperations_Outgoing_TransferToSettlementAccount;

        public class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Платежная система")]
            public string PurseName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int PurseId { get; set; }

            [Display(Name = "Расчетный счет")]
            public string SettlementAccountNumber { get; set; }

            public int? SettlementAccountId { get; set; }

            [Display(Name = "К оплате")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Комментарий")]
            public string Comment { get; set; }

            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Предыдущий тип списания")]
            public string OldOperationType { get; set; }
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
