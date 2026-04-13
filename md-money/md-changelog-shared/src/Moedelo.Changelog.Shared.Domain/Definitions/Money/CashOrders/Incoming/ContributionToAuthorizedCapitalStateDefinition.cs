using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Enums;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming
{
    public sealed class ContributionToAuthorizedCapitalStateDefinition
        : IncomingCashOrderStateDefinition<
            ContributionToAuthorizedCapitalStateDefinition,
            ContributionToAuthorizedCapitalStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_CashOrders_Incoming_ContributionToAuthorizedCapital;

        public class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Касса")]
            public string CashName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long CashId { get; set; }

            [Display(Name = "Сумма")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Плательщик")]
            public string ContractorName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int ContractorId { get; set; }

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
