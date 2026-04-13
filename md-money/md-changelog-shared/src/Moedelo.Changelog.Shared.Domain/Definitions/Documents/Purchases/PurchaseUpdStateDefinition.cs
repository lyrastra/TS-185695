using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Definitions.Documents.Sales;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Purchases
{
    /// <summary>
    /// Покупки (purchase/incoming): УПД
    /// </summary>
    public class PurchaseUpdStateDefinition
        : PurchaseDocumentStateDefinition<
            PurchaseUpdStateDefinition,
            PurchaseUpdStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.PurchaseUpd;

        public class State
        {
            public long DocumentBaseId { get; set; }

            #region Заполнение
            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }
            
            [Display(Name = "Статус")]
            public UpdStatus Status { get; set; }
            
            [Display(Name = "Документ прошлого периода")]
            public ForgottenDocumentInfo ForgottenDocument { get; set; }
            
            /// <summary>
            /// идентификатор контрагента (техн. поле)
            /// </summary>
            public int KontragentId { get; set; }
            
            [Display(Name = "Поставщик")]
            public string KontragentName { get; set; }
            
            [Display(Name = "По договору")]
            public string ContractName { get; set; }
            
            [Display(Name = "Склад")]
            public string StockName { get; set; }
            
            [Display(Name = "Учесть в")]
            public TaxationSystemType TaxSystemType { get; set; }
            
            [Display(Name = "НДС")]
            public bool UseNds { get; set; }

            [Display(Name = "Тип НДС")]
            public NdsPositionType NdsPositionType { get; set; }
            
            [Display(Name = "Импортируемый товар")]
            public bool? IsImportProduct { get; set; }

            [Display(Name = "Позиции")]
            public DocPosition[] Positions { get; set; }

            [Display(Name = "Платежи")]
            public LinkedPayment[] Payments { get; set; }
            
            [Display(Name = "Счёт-фактура")]
            public LinkedInvoice Invoice { get; set; }
            
            [Display(Name = "Принять НДС к вычету")]
            public bool? AcceptNdsDeduction { get; set; }
            
            [Display(Name = "НДС к вычету")]
            public NdsDeduction[] NdsDeductions { get; set; }

            [Display(Name = "Восстановить НДС")]
            public NdsRecover[] NdsRecovers { get; set; }
            
            [Display(Name = "Сумма без НДС")]
            public MoneySum SumWithoutNds { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Сумма с НДС")]
            public MoneySum SumWithNds { get; set; }

            #endregion Заполнение

            #region Вкладка "Учёт"

            [Display(Name = "Проведено в БУ")]
            public virtual bool ProvideInAccounting { get; set; }
            
            [Display(Name = "Проведено в НУ")]
            public TaxProvideType TaxProvideType { get; set; }

            #endregion Вкладка "Учёт"

            public class DocPosition
            {
                /// <summary>
                /// идентификатор позиции (техн.): вычисляемый, не равен одноименному полю в БД
                /// </summary>
                public string Id { get; set; }

                /// <note> null - для агрегирующей позиции без типа </note>
                [Display(Name = "Тип")]
                public ItemType? Type { get; set; }
                
                [Display(Name = "Наименование")]
                public string Name { get; set; }

                [Display(Name = "Ед. изм.")]
                public string Unit { get; set; }

                /// <note> null - для агрегирующей позиции без кол-ва </note>
                [Display(Name = "Кол-во")]
                public decimal? Amount { get; set; }

                [Display(Name = "Цена")]
                public MoneySum Price { get; set; }

                [Display(Name = "Сумма без НДС")]
                public MoneySum SumWithoutNds { get; set; }

                /// <note> null - для агрегирующей позиции</note>
                [Display(Name = "НДС")]
                public NdsRateType? NdsRate { get; set; }

                [Display(Name = "Сумма НДС")]
                public MoneySum NdsSum { get; set; }

                [Display(Name = "Сумма c НДС")]
                public MoneySum SumWithNds { get; set; }
                
                [Display(Name = "Страна")]
                public string GtdCountry { get; set; }

                [Display(Name = "ГТД")]
                public string GtdNumber { get; set; }
                
                /// <summary> Вкладка Учет. Если тип УПД = 1 </summary>
                [Display(Name = "Код")]
                public string NdsOperationCode { get; set; }
                
                /// <summary> Вкладка Учет. Для услуг: счет расхода </summary>
                [Display(Name = "Счет")]
                public string ActivityAccountCode { get; set; }
                
                /// <summary>
                /// идентификатор товара (техн.)
                /// </summary>
                public long? ProductId { get; set; }
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
            }

            /// <summary>
            /// Принятие НДС к вычету
            /// </summary>
            public class NdsDeduction
            {
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

        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.UsnAccountantTariff, AccessRule.ViewAllUpdsBuying};

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