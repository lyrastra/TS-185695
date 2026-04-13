using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing
{
    public class AgencyContractStateDefinition
        : OutgoingCashOrderStateDefinition<
            AgencyContractStateDefinition,
            AgencyContractStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_CashOrders_Outgoing_AgencyContract;

        public sealed class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Касса")]
            public string CashName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long CashId { get; set; }

            [Display(Name = "Всего к оплате")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Получатель")]
            public string ContractorName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int ContractorId { get; set; }

            [Display(Name = "По договору")]
            public string ContractName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? ContractBaseId { get; set; }

            [Display(Name = "Основание")]
            public string Destination { get; set; }

            [Display(Name = "Приложение")]
            public string Comment { get; set; }

            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Предыдущий тип платежа")]
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
