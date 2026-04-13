using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Incoming
{
    public class PaymentFromCustomerStateDefinition
        : IncomingPurseOperationStateDefinition<
            PaymentFromCustomerStateDefinition,
            PaymentFromCustomerStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType =>
            ChangeLogEntityType.Money_PurseOperations_Incoming_PaymentFromCustomer;

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

            [Display(Name = "Плательщик")]
            public string ContractorName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public int ContractorId { get; set; }

            [Display(Name = "По договору")]
            public string ContractName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? ContractBaseId { get; set; }

            [Display(Name = "Поступило")]
            public MoneySum Sum { get; set; }

            [Display(Name = "В том числе НДС")]
            public bool? WithNds { get; set; }

            [Display(Name = "Ставка НДС")]
            public string NdsType { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Комментарий")]
            public string Comment { get; set; }

            [Display(Name = "Учесть в")]
            public string TaxationSystemType { get; set; }

            [Display(Name = "Патент")]
            public string PatentName { get; set; }

            [Display(Name = "Связанные документы")]
            public LinkedDocumentInfo[] LinkedDocuments { get; set; }

            [Display(Name = "По счету")]
            public string BillName { get; set; }

            // сохраняется на всякий случай, можно и не сохранять
            public long? BillBaseId { get; set; }

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
