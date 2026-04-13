using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Changelog.Shared.Domain.Enums;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming
{
    public class CurrencyPaymentFromCustomerStateDefinition
        : IncomingPaymentOrderStateDefinition<
            CurrencyPaymentFromCustomerStateDefinition,
            CurrencyPaymentFromCustomerStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PaymentOrders_Incoming_CurrencyPaymentFromCustomer;

        public class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Сумма")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Итого")]
            public MoneySum TotalSum { get; set; }

            [Display(Name = "Назначение")]
            public string Description { get; set; }

            [Display(Name = "Валютный счет")]
            public string SettlementAccountNumber { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int SettlementAccountId { get; set; }

            [Display(Name = "Плательщик")]
            public ContractorInfo Contractor { get; set; }

            [Display(Name = "Договор")]
            public string ContractName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? ContractBaseId { get; set; }

            [Display(Name = "Проведено вручную в налоговом учете")]
            public bool IsManualTaxPostings { get; set; }

            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Связанные документы")]
            public LinkedDocumentInfo[] LinkedDocuments { get; set; }

            [Display(Name = "Учесть в")]
            public string TaxationSystemType { get; set; }

            [Display(Name = "Патент")]
            public string PatentName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? PatentId { get; set; }
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
