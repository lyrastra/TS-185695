using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Attributes;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Sales
{
    /// <summary>
    /// Продажи (sale/outgoing): акт
    /// </summary>
    public class SaleStatementStateDefinition
        : SaleDocumentStateDefinition<
            SaleStatementStateDefinition,
            SaleStatementStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.SaleStatement;

        public class State
        {
            public long DocumentBaseId { get; set; }

            #region Вкладка "Заполнение"

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            [Display(Name = "Тип акта")]
            public StatementType Type { get; set; }

            /// <summary>
            /// идентификатор контрагента (техн. поле)
            /// </summary>
            public int KontragentId { get; set; }

            [Display(Name = "Контрагент")]
            public string KontragentName { get; set; }

            [Display(Name = "По договору")]
            public string ContractName { get; set; }

            [Display(Name = "По счёту")]
            public LinkedBillInfo[] LinkedBills { get; set; }

            [Display(Name = "Тип НДС")]
            public NdsPositionType NdsPositionType { get; set; }

            [Display(Name = "Учесть в")]
            public TaxationSystemType? TaxSystemType { get; set; }

            [Display(Name = "Скидка")]
            public bool HasDiscount { get; set; }

            [Display(Name = "Посреднический договор")]
            public string MiddlemanContract { get; set; }
            
            [Display(Name = "Дополнительная информация")]
            public string AdditionalInfo { get; set; }

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
            public InvoiceLinkedToSaleStatement Invoice { get; set; }

            [Display(Name = "Комментарий")]
            public string Comment { get; set; }

            /// <summary>
            /// Статус оплаты документа (техническое поле)
            /// </summary>
            public DocumentPayStatus Status { get; set; }
            
            [Display(Name = "Подписан")]
            public string OnHands { get; set; }
            
            [Display(Name = "Печать и подпись")]
            public bool UseStampAndSign { get; set; }

            #endregion

            #region Вкладка "Учёт"

            [Display(Name = "Счёт контрагента")]
            public string KontragentAccountCode { get; set; }

            [Display(Name = "Обычный вид деятельности")]
            public bool IsMainActivity { get; set; }

            [Display(Name = "Проведено в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Проведено в НУ")]
            public TaxProvideType TaxProvideType { get; set; }

            #endregion Вкладка "Учёт"

            #region Типы данных
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
                
                [Display(Name = "Скидка"), FieldTypePercent]
                public decimal? DiscountRate { get; set; }

                [Display(Name = "Сумма без НДС")]
                public MoneySum SumWithoutNds { get; set; }

                [Display(Name = "Сумма НДС")]
                public MoneySum NdsSum { get; set; }

                [Display(Name = "Сумма")]
                public MoneySum SumWithNds { get; set; }

                [Display(Name = "НДС")]
                public NdsRateType NdsRate { get; set; }
            }

            public class LinkedPaymentInfo
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
            }

            public class LinkedBillInfo
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; } 
            }
            
            public class InvoiceLinkedToSaleStatement
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Принять НДС к вычету")]
                public NdsDeductionInfo[] NdsDeductions { get; set; }
            }

            /// <summary>
            /// Принятие НДС к вычету
            /// </summary>
            public class NdsDeductionInfo
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
            }

            #endregion
        }

        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.UsnAccountantTariff, AccessRule.ViewAllStatementsSales};

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
