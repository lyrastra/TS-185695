using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing
{
    public class BudgetaryPaymentStateDefinition
        : OutgoingCashOrderStateDefinition<
            BudgetaryPaymentStateDefinition,
            BudgetaryPaymentStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_CashOrders_Outgoing_BudgetaryPayment;

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

            [Display(Name = "Сумма")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Тип бюджетного платежа")]
            public BudgetaryAccountType AccountType { get; set; }

            [Display(Name = "Тип налога/взноса")]
            public string AccountCode { get; set; }

            [Display(Name = "Период")]
            public string Period { get; set; }

            // на всякий случай сохраняем технический идентификатор
            public int? KbkId { get; set; }

            [Display(Name = "КБК")]
            public string KbkNumber { get; set; }

            [Display(Name = "Тип платежа (КБК)")]
            public KbkPaymentTypeEnum KbkPaymentType { get; set; }

            [Display(Name = "Получатель")]
            public string Recipient { get; set; }

            [Display(Name = "Назначение")]
            public string Destination { get; set; }

            [Display(Name = "Предыдущий тип платежа")]
            public string OldOperationType { get; set; }

            /// <summary>
            /// Тип платежа (КБК)
            /// </summary>
            public enum KbkPaymentTypeEnum
            {
                /// <summary>
                /// Налоги
                /// </summary>
                [Display(Name = "Налоги")]
                PaymentTaxes = 100,

                /// <summary>
                /// Взносы
                /// </summary>
                [Display(Name = "Взносы")]
                PaymentFees = 101,

                /// <summary>
                /// Пени
                /// </summary>
                [Display(Name = "Пени")]
                Surcharge = 2,

                /// <summary>
                /// Штрафы
                /// </summary>
                [Display(Name = "Штрафы")]
                Forfeit = 3
            }

            public enum BudgetaryAccountType
            {
                [Display(Name = "Налоги")]
                Taxes = 0,
                [Display(Name = "Взносы")]
                Fees = 1
            }
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
