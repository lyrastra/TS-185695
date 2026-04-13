using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Attributes;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Sales
{
    /// <summary>
    /// Продажи (sale/outgoing): накладная
    /// </summary>
    public class SaleWaybillStateDefinition
        : SaleDocumentStateDefinition<
            SaleWaybillStateDefinition,
            SaleWaybillStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.SaleWaybill;

        public class State
        {
            public long DocumentBaseId { get; set; }

            #region Вкладка "Заполнение"

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }
            
            [Display(Name = "Документ прошлого периода")]
            public ForgottenDocumentInfo ForgottenDocument { get; set; }

            [Display(Name = "Тип накладной")]
            public WaybillType Type { get; set; }

            /// <summary>
            /// идентификатор контрагента (техн. поле)
            /// </summary>
            public int KontragentId { get; set; }

            [Display(Name = "Контрагент")]
            public string KontragentName { get; set; }
            
            /// <summary>
            /// идентификатор грузоотправителя (техн. поле)
            /// </summary>
            public int? SenderId { get; set; }
            
            [Display(Name = "Грузоотправитель")]
            public string SenderName { get; set; }
            
            /// <summary>
            /// идентификатор поставщика (техн. поле)
            /// </summary>
            public int? SupplierId { get; set; }
            
            [Display(Name = "Поставщик")]
            public string SupplierName { get; set; }
            
            /// <summary>
            /// идентификатор грузополучателя (техн. поле)
            /// </summary>
            public int? ReceiverId { get; set; }
            
            [Display(Name = "Грузополучатель")]
            public string ReceiverName { get; set; }
            
            /// <summary>
            /// идентификатор плательщика (техн. поле)
            /// </summary>
            public int? PayerId { get; set; }
            
            [Display(Name = "Плательщик")]
            public string PayerName { get; set; }

            [Display(Name = "По договору")]
            public string ContractName { get; set; }

            [Display(Name = "По счёту")]
            public string BillName { get; set; }
            
            [Display(Name = "Продать со склада")]
            public string StockName { get; set; }
            
            /// <summary>
            /// В накладной на возврат: связанные накладные
            /// </summary>
            [Display(Name = "Документ о покупке")]
            public LinkedWaybill[] Waybills { get; set; }

            [Display(Name = "НДС")]
            public bool UseNds { get; set; }
            
            [Display(Name = "Тип НДС")]
            public NdsPositionType NdsPositionType { get; set; }
            
            [Display(Name = "Учесть в")]
            public TaxationSystemType TaxSystemType { get; set; }

            [Display(Name = "Скидка")]
            public bool HasDiscount { get; set; }

            [Display(Name = "Позиции")]
            public DocPosition[] Positions { get; set; }

            [Display(Name = "Платежи")]
            public LinkedPayment[] Payments { get; set; }

            [Display(Name = "Сумма без НДС")]
            public MoneySum SumWithoutNds { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Сумма с НДС")]
            public MoneySum SumWithNds { get; set; }

            [Display(Name = "Счёт-фактура")]
            public LinkedInvoice Invoice { get; set; }

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

            [Display(Name = "Проведено в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Проведено в НУ")]
            public TaxProvideType TaxProvideType { get; set; }

            #endregion Вкладка "Учёт"

            #region Типы данных
            public class DocPosition
            {
                /// <summary>
                /// идентификатор позиции (техн.): вычисляемый, не равен одноименному полю в БД
                /// </summary>
                public string Id { get; set; }
                
                /// <summary>
                /// осознанно не использовано "Name", чтобы различать разные товары с одинаковыми именами (сопоставление по индексу) 
                /// </summary>
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Ед. изм.")]
                public string Unit { get; set; }

                /// <note> null - для агрегирующей позиции без кол-ва </note>
                [Display(Name = "Кол-во")]
                public decimal? Amount { get; set; }

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

                /// <note> null - для агрегирующей позиции </note>
                [Display(Name = "НДС")]
                public NdsRateType? NdsRate { get; set; }
                
                [Display(Name = "Код операции")]
                public string NdsDeclarationSection7Code { get; set; }

                /// <summary>
                /// идентификатор товара (техн.)
                /// </summary>
                public long ProductId { get; set; }
            }

            public class ForgottenDocumentInfo
            {
                [Display(Name = "Номер")]
                public string Number { get; set; }
                
                [Display(Name = "Дата")]
                public DateTime Date { get; set; }
            }
            
            public class LinkedPayment
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
            }
            
            public class LinkedWaybill
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; } 
            }
            
            public class LinkedInvoice
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Принять НДС к вычету")]
                public NdsDeduction[] NdsDeductions { get; set; }
                
                public class NdsDeduction
                {
                    [Display(Name = "Наименование")]
                    public string Name { get; set; }
                    [Display(Name = "Сумма")]
                    public MoneySum Sum { get; set; }
                }
            }
            #endregion
        }

        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.UsnAccountantTariff, AccessRule.ViewAllWaybillsSales};

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
