using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing
{
    public class RentPaymentStateDefinition
        : OutgoingPaymentOrderStateDefinition<
            RentPaymentStateDefinition,
            RentPaymentStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Outgoing_RentPayment;

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

            [Display(Name = "Основное средство")]
            public string FixedAssetName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? InventoryCardBaseId { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

            [Display(Name = "К оплате")]
            public MoneySum Sum { get; set; }

            [Display(Name = "В том числе НДС")]
            public bool WithNds { get; set; }

            [Display(Name = "Ставка НДС")]
            public string NdsType { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Оплачено")]
            public bool IsPaid { get; set; }

            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Периоды")]

            public IReadOnlyCollection<RentPeriodInfo> RentPeriods { get; set; }

        }

        public override long GetEntityId(State state)
        {
            return state.DocumentBaseId;
        }

        protected override string GetEntityName(State state)
        {
            return $"{EntityTypeName} №{state.Number} от {state.Date:dd.MM.yyyy}";
        }

        public class RentPeriodInfo
        {
            [Display(Name = "Сумма")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Период")]
            public string Name { get; set; }
        }
    }
}
