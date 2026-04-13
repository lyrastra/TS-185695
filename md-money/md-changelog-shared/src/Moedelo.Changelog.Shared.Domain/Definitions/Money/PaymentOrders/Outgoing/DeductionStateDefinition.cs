using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing
{
    public class DeductionStateDefinition
        : OutgoingPaymentOrderStateDefinition<
            DeductionStateDefinition,
            DeductionStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Outgoing_Deduction;

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

            [Display(Name = "Получатель")]
            public ContractorInfo Contractor { get; set; }

            [Display(Name = "Договор")]
            public string ContractName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? ContractBaseId { get; set; }

            [Display(Name = "Очередность платежа")]
            public int PaymentPriority { get; set; }

            [Display(Name = "Взыскивается долг перед бюджетом")]
            public bool IsBudgetaryDebt { get; set; }
            
            [Display(Name = "Сотрудник - плательщик удержаний")]
            public string DeductionWorkerName { get; set; }

            public long? DeductionWorkerId { get; set; }

            [Display(Name = "КБК")]
            public string Kbk { get; set; }
            
            [Display(Name = "OKTMO")]
            public string Oktmo { get; set; }
            
            [Display(Name = "Код (22)")]
            public string Uin { get; set; }
            
            [Display(Name = "Номер документа (108)")]
            public string DeductionWorkerDocumentNumber { get; set; }

            [Display(Name = "Сумма (7)")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

            [Display(Name = "Оплачено")]
            public bool IsPaid { get; set; }

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
