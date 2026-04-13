using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Changelog.Shared.Domain.Enums;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming
{
    public sealed class OtherIncomingStateDefinition
        : IncomingCashOrderStateDefinition<
            OtherIncomingStateDefinition,
            OtherIncomingStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_CashOrders_Incoming_Other;

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

            [Display(Name = "В том числе НДС")]
            public bool WithNds { get; set; }

            [Display(Name = "Ставка НДС")]
            public string NdsType { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Плательщик")]
            public string ContractorName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int ContractorId { get; set; }

            [Display(Name = "По договору")]
            public string ContractName { get; set; }

            public long? ContractBaseId { get; set; }

            [Display(Name = "Основание")]
            public string Destination { get; set; }

            [Display(Name = "Приложение")]
            public string Comment { get; set; }

            [Display(Name = "Учесть в")]
            public string TaxationSystemType { get; set; }

            [Display(Name = "Патент")]
            public string PatentName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? PatentId { get; set; }

            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Связанные счета")]
            public LinkedDocumentInfo[] LinkedBills { get; set; }

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
