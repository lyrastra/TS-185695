using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Purchases
{
    /// <summary>
    /// Покупки (purchase/incoming): накладная
    /// </summary>
    public class PurchaseWaybillStateDefinition
        : PurchaseDocumentStateDefinition<
            PurchaseWaybillStateDefinition,
            PurchaseWaybillStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.PurchaseWaybill;

        public class State
        {
            public long DocumentBaseId { get; set; }

            #region Заполнение
            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }
            
            [Display(Name = "Документ прошлого периода")]
            public ForgottenDocumentInfo ForgottenDocument { get; set; }
            
            [Display(Name = "Тип накладной")]
            public WaybillType Type { get; set; }
            
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
            
            [Display(Name = "Оприходовать")]
            public string StockName { get; set; }
            
            [Display(Name = "НДС")]
            public bool UseNds { get; set; }
            
            [Display(Name = "Наличие несоответствия по количеству/качеству")]
            public bool? HasDiscrepancyNumberOrQuality { get; set; }

            [Display(Name = "Тип НДС")]
            public NdsPositionType NdsPositionType { get; set; }
            
            [Display(Name = "Учесть в")]
            public TaxationSystemType TaxSystemType { get; set; }

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

            #endregion Заполнение

            #region Вкладка "Учёт"

            [Display(Name = "Проведено в БУ")]
            public virtual bool ProvideInAccounting { get; set; }
            
            [Display(Name = "Проведено в НУ")]
            public TaxProvideType TaxProvideType { get; set; }

            [Display(Name = "Счёт контрагента")]
            public string KontragentAccountCode { get; set; }

            #endregion Вкладка "Учёт"

            public class DocPosition
            {
                /// <summary>
                /// идентификатор позиции (техн.): вычисляемый, не равен одноименному полю в БД
                /// </summary>
                public string Id { get; set; }
                
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Ед. изм.")]
                public string Unit { get; set; }

                /// <note> null - для агрегирующей позиции без кол-ва </note>
                [Display(Name = "Кол-во")]
                public decimal? Amount { get; set; }
                
                [Display(Name = "Фактическое кол-во")]
                public decimal? RealAmount { get; set; }

                [Display(Name = "Цена")]
                public MoneySum Price { get; set; }

                [Display(Name = "Сумма без НДС")]
                public MoneySum SumWithoutNds { get; set; }

                [Display(Name = "Сумма НДС")]
                public MoneySum NdsSum { get; set; }

                [Display(Name = "Сумма")]
                public MoneySum SumWithNds { get; set; }

                /// <note> null - для агрегирующей позиции </note>
                [Display(Name = "НДС")]
                public NdsRateType? NdsRate { get; set; }
                
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
            
            public class LinkedInvoice
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Принять НДС к вычету")]
                public bool AcceptNdsDeduction { get; set; }

                [Display(Name = "НДС к вычету")]
                public NdsDeduction[] NdsDeductions { get; set; }

                [Display(Name = "Восстановить НДС")]
                public NdsRecover[] NdsRecovers { get; set; }
                
                /// <summary>
                /// Принятие НДС к вычету
                /// </summary>
                public class NdsDeduction
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
        }

        // note: есть ещё право AccessRule.ViewPersonalWaybillBuying - с таким правом можно видеть только свои
        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.UsnAccountantTariff, AccessRule.ViewAllWaybillsBuying};

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
