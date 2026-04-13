using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Attributes;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Purchases
{
    /// <summary>
    /// Покупки (purchase/incoming): акт
    /// </summary>
    public class PurchaseStatementStateDefinition
        : PurchaseDocumentStateDefinition<
            PurchaseStatementStateDefinition,
            PurchaseStatementStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.PurchaseStatement;

        public class State
        {
            public long DocumentBaseId { get; set; }

            #region Заполнение
            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }
            
            [Display(Name = "Компенсируется заказчиком")]
            public bool IsCompensatedByCustomer { get; set; }

            /// <summary>
            /// идентификатор контрагента (техн. поле)
            /// </summary>
            public int KontragentId { get; set; }
            
            [Display(Name = "Контрагент")]
            public string KontragentName { get; set; }
            
            [Display(Name = "По договору")]
            public string ContractName { get; set; }
            
            /// <summary>
            /// идентификатор заказчика (техн. поле)
            /// </summary>
            public int? CustomerId { get; set; }

            [Display(Name = "Заказчик")]
            public string CustomerName { get; set; }

            [Display(Name = "Посреднический договор")]
            public string MiddlemanContract { get; set; }

            [Display(Name = "Тип НДС")]
            public NdsPositionType NdsPositionType { get; set; }
            
            [Display(Name = "Учесть в")]
            public TaxationSystemType? TaxSystemType { get; set; }

            [Display(Name = "Позиции")]
            public PositionInfo[] Positions { get; set; }

            [Display(Name = "Платежи")]
            public LinkedPaymentInfo[] LinkedPayments { get; set; }
            
            [Display(Name = "Сумма без НДС")]
            public MoneySum SumWithoutNds { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Сумма с НДС")]
            public MoneySum SumWithNds { get; set; }

            [Display(Name = "Счёт-фактура")]
            public InvoiceLinkedToStatement Invoice { get; set; }

            #endregion Заполнение

            #region Вкладка "Учёт"
            
            [Display(Name = "Счёт контрагента")]
            public string KontragentAccountCode { get; set; }

            [Display(Name = "Проведено в БУ")]
            public virtual bool ProvideInAccounting { get; set; }
            
            [Display(Name = "Проведено в НУ")]
            public TaxProvideType TaxProvideType { get; set; }

            #endregion Вкладка "Учёт"

            public class PositionInfo
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Ед. изм.")]
                public string Unit { get; set; }

                [Display(Name = "Кол-во")]
                public decimal Amount { get; set; }

                [Display(Name = "Цена")]
                public MoneySum Price { get; set; }

                [Display(Name = "Сумма без НДС")]
                public MoneySum SumWithoutNds { get; set; }

                [Display(Name = "Сумма НДС")]
                public MoneySum NdsSum { get; set; }

                [Display(Name = "Сумма")]
                public MoneySum SumWithNds { get; set; }

                [Display(Name = "НДС")]
                public NdsRateType NdsRate { get; set; }
                
                [Display(Name = "Счет")]
                public string ActivityAccountCode { get; set; }
            }

            public class LinkedPaymentInfo
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
            }
            
            public class InvoiceLinkedToStatement
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Принять НДС к вычету")]
                public bool AcceptNdsDeduction { get; set; }

                [Display(Name = "НДС к вычету")]
                public NdsDeductionInfo[] NdsDeductions { get; set; }

                [Display(Name = "Восстановить НДС")]
                public NdsRecover[] NdsRecovers { get; set; }
            }

            /// <summary>
            /// Принятие НДС к вычету
            /// </summary>
            public class NdsDeductionInfo
            {
                public long Id { get; set; }
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
                [Display(Name = "Дата")]
                public DateTime Date { get; set; }
            }

            /// <summary>
            /// Восстановление НДС
            /// </summary>
            public class NdsRecover
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
            }
        }

        // note: есть ещё право AccessRule.ViewPersonalStatementsBuying - с таким правом можно видеть только свои
        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.UsnAccountantTariff, AccessRule.ViewAllStatementsBuying};

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
