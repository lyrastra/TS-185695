using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Sales
{
    /// <summary>
    /// Продажи (sale/outgoing): УПД
    /// </summary>
    public class SaleUpdStateDefinition
        : SaleDocumentStateDefinition<
            SaleUpdStateDefinition,
            SaleUpdStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.SaleUpd;

        public class State
        {
            public long DocumentBaseId { get; set; }

            #region Вкладка "Заполнение"

            [Display(Name = "Номер документа")]
            public string Number { get; set; }

            [Display(Name = "Дата документа")]
            public DateTime Date { get; set; }
            
            [Display(Name = "Статус")]
            public UpdStatus Status { get; set; }
            
            [Display(Name = "Тип")]
            public SaleUpdType Type { get; set; }
            
            [Display(Name = "Документ прошлого периода")]
            public ForgottenDocumentInfo ForgottenDocument { get; set; }

            /// <summary>
            /// идентификатор контрагента (техн. поле)
            /// </summary>
            public int KontragentId { get; set; }

            [Display(Name = "Контрагент")]
            public string KontragentName { get; set; }
            
            /// <summary>
            /// Номер заявки (OZON) (если комиссионер OZON)
            /// </summary>
            [Display(Name = "Иные сведения об отгрузке, передаче")]
            public string ShipmentDetail { get; set; }
            
            /// <summary>
            /// Дата и интервал времени (если комиссионер OZON)
            /// </summary>
            [Display(Name = "Дата доставки")]
            public string DeliveryDate { get; set; }
            
            [Display(Name = "Плательщик")]
            public string PayerName { get; set; }

            [Display(Name = "Грузополучатель")]
            public string ReceiverName { get; set; }
            
            /// <summary>
            /// идентификатор грузоотправителя (техн. поле)
            /// </summary>
            public int? SenderId { get; set; }
            
            [Display(Name = "Грузоотправитель")]
            public string SenderName { get; set; }
            
            [Display(Name = "Поставщик")]
            public string SupplierName { get; set; }

            [Display(Name = "По договору")]
            public string ContractName { get; set; }

            [Display(Name = "По счёту")]
            public string BillName { get; set; }
            
            [Display(Name = "Учесть в")]
            public TaxationSystemType TaxSystemType { get; set; }
            
            [Display(Name = "Склад")]
            public string StockName { get; set; }
            
            [Display(Name = "Тип НДС")]
            public NdsPositionType NdsPositionType { get; set; }

            [Display(Name = "Импортируемый товар")]
            public bool? IsImportProduct { get; set; }

            [Display(Name = "Позиции")]
            public DocPosition[] Positions { get; set; }

            [Display(Name = "Сумма без НДС")]
            public MoneySum SumWithoutNds { get; set; }

            [Display(Name = "Сумма НДС")]
            public MoneySum NdsSum { get; set; }

            [Display(Name = "Сумма с НДС")]
            public MoneySum SumWithNds { get; set; }
            
            [Display(Name = "Платежи")]
            public LinkedPayment[] Payments { get; set; }
            
            [Display(Name = "Принять НДС к вычету")]
            public NdsDeduction[] NdsDeductions { get; set; }

            [Display(Name = "Подписан")]
            public string SignStatus { get; set; }
            
            [Display(Name = "Печать и подпись")]
            public bool UseStampAndSign { get; set; }
            
            /// <summary>
            /// Идентификатор госконтракта (ИГК)
            /// </summary>
            [Display(Name = "ИГК")]
            public string GovernmentContractId { get; set; }

            /// <summary>
            /// Данные о транспортировке и грузе
            /// </summary>
            [Display(Name = "Данные о транспортировке и грузе")]
            public string TransportationDetails { get; set; }

            #endregion

            #region Вкладка "Учёт"

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

                /// <note> null - для агрегирующей позиции без типа </note>
                [Display(Name = "Тип")]
                public ItemType? Type { get; set; }
                
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                
                [Display(Name = "Маркировка")]
                public ProductLabel[] Labels { get; set; }

                [Display(Name = "Ед. изм.")]
                public string Unit { get; set; }

                /// <note> null - для агрегирующей позиции без кол-ва </note>
                [Display(Name = "Кол-во")]
                public decimal? Amount { get; set; }

                [Display(Name = "Цена")]
                public MoneySum Price { get; set; }

                [Display(Name = "Сумма без НДС")]
                public MoneySum SumWithoutNds { get; set; }

                /// <note> null - для агрегирующей позиции </note>
                [Display(Name = "НДС")]
                public NdsRateType? NdsRate { get; set; }
                
                [Display(Name = "Код операции")]
                public string NdsDeclarationSection7Code { get; set; }

                [Display(Name = "Сумма НДС")]
                public MoneySum NdsSum { get; set; }

                [Display(Name = "Сумма с НДС")]
                public MoneySum SumWithNds { get; set; }

                [Display(Name = "Страна")]
                public string GtdCountry { get; set; }

                [Display(Name = "ГТД")]
                public string GtdNumber { get; set; }
                
                [Display(Name = "Код")]
                public string NdsOperationCode { get; set; }

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
            
            public class ProductLabel
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                
                [Display(Name = "Список кодов")]
                public string[] Codes { get; set; }
            }
            
            public class NdsDeduction
            {
                [Display(Name = "Наименование")]
                public string Name { get; set; }
                [Display(Name = "Сумма")]
                public MoneySum Sum { get; set; }
            }
            #endregion
        }

        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new[] {AccessRule.UsnAccountantTariff, AccessRule.ViewAllUpdsSales};

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