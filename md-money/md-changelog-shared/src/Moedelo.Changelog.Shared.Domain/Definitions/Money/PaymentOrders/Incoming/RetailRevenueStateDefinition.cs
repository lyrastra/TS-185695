using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming
{
    public class RetailRevenueStateDefinition
        : IncomingPaymentOrderStateDefinition<
            RetailRevenueStateDefinition,
            RetailRevenueStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Incoming_RetailRevenue;

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

            [Display(Name = "Посредничество")]
            public bool? IsMediation { get; set; }

            [Display(Name = "Дата отгрузки/реализации услуг")]
            public DateTime? SaleDate { get; set; }

            [Display(Name = "Комиссия (эквайринг)")]
            public MoneySum AcquiringCommissionSum { get; set; }

            [Display(Name = "Дата комиссии")]
            public DateTime? AcquiringCommissionDate { get; set; }

            [Display(Name = "Поступило")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

            [Display(Name = "Учесть в")]
            public string TaxationSystemType { get; set; }

            [Display(Name = "Патент")]
            public string PatentName { get; set; }

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
