using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales
{
    public class SaleUpdNewState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public UpdStatus Status { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public UpdSaleType Type { get; set; }

        /// <summary>
        /// Номер забытого документа, который вводится в настроящем за прошлый период
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Иные сведения об отгрузке, передаче (комиссионеры/маркетплейсы)
        /// </summary>
        public string ShipmentDetail { get; set; }

        /// <summary>
        /// Документ для маркетплейса (комиссионеры/маркетплейсы)
        /// </summary>
        public MarketplaceType? MarketplaceType { get; set; }

        /// <summary>
        /// Дата доставки (комиссионеры/маркетплейсы)
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Время доставки с.. (комиссионеры/маркетплейсы)
        /// </summary>
        public TimeSpan? DeliveryStartTime { get; set; }

        /// <summary>
        /// Время доставки ..до (комиссионеры/маркетплейсы)
        /// </summary>
        public TimeSpan? DeliveryEndTime { get; set; }

        /// <summary>
        /// Идентификатор госконтракта (ИГК)
        /// </summary>
        public string GovernmentContractId { get; set; }

        /// <summary>
        /// Данные о транспортировке и грузе
        /// </summary>
        public string TransportationDetails { get; set; }
        
        /// <summary>
        /// Идентификатор грузополучателя
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Идентификатор грузоотправителя
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Учесть в
        /// </summary>
        public TaxationSystemType TaxSystemType { get; set; }

        /// <summary>
        /// Склад
        /// </summary>
        public StockInfo Stock { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Позиции (null, если установлен признак IsTruncated) 
        /// </summary>
        public Item[] Items { get; set; }

        /// <summary>
        /// Платежи
        /// </summary>
        public LinkedPayment[] Payments { get; set; }

        /// <summary>
        /// Принять НДС к вычету
        /// </summary>
        public NdsDeduction[] NdsDeductions { get; set; }
        
        /// <summary>
        /// по договору
        /// </summary>
        public LinkedContract Contract { get; set; }

        /// <summary>
        /// по счету
        /// </summary>
        public LinkedBill Bill { get; set; }

        /// <summary>
        /// Состояние переключетеля "Подписан"
        /// </summary>
        public SignStatus SignStatus { get; set; }

        /// <summary>
        /// Состояние переключетеля "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }

        /// <summary>
        /// Счёт контрагента
        /// </summary>
        public int KontragentAccountCode { get; set; }

        /// <summary>
        /// Провести в НУ
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; }
        
        /// <summary>
        /// Провести в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }
        
        /// <summary>
        /// Идентификтор субконто
        /// </summary>
        public long SubcontoId { get; set; }
        
        /// <summary>
        /// Передана только ЧАСТЬ ДАННЫХ! Модель обогатить через из API в случае необходимости.
        ///
        /// Критерий усечения: более 1000 позиций.
        /// На 14.07.2023 имеем 60 больших документов из ~995_000 (за полгода +47)
        /// </summary>
        public bool IsTruncated { get; set; }

        #region Типы данных

        public class Item
        {
            /// <summary>
            /// Наименование
            /// </summary>
            public string Name { get; set; }
            
            /// <summary>
            /// Ед. изм.
            /// </summary>
            public string Unit { get; set; }
        
            /// <summary>
            /// Цена
            /// </summary>
            public decimal Price { get; set; }
        
            /// <summary>
            /// Количество
            /// </summary>
            public decimal Count { get; set; }

            /// <summary>
            /// Сумма без НДС
            /// </summary>
            public decimal SumWithoutNds { get; set; }

            /// <summary>
            /// Сумма с НДС
            /// </summary>
            public decimal SumWithNds { get; set; }

            /// <summary>
            /// Суммы отредактированы пользователем
            /// </summary>
            public decimal NdsSum { get; set; }

            /// <summary>
            /// Сумма НДС
            /// </summary>
            public bool IsCustomSums { get; set; }

            /// <summary>
            /// Идентификатор товара (null - услуга)
            /// </summary>
            public long? StockProductId { get; set; }

            /// <summary>
            /// Ставка НДС
            /// </summary>
            public NdsType NdsType { get; set; }

            /// <summary>
            /// Код вида операции, если тип УПД = 1 (УПД является также сч-ф)
            /// </summary>
            public int? NdsOperationType { get; set; }

            /// <summary>
            /// Страна импортируемого товара
            /// </summary>
            public string Country { get; set; }

            /// <summary>
            /// ГТД импортируемого товара
            /// </summary>
            public string Declaration { get; set; }

            /// <summary>
            /// Id НДС кода раздела 7
            /// </summary>
            public int? NdsDeclarationSection7CodeId { get; set; }

            /// <summary>
            /// Маркировочные коды
            /// </summary>
            public ProductLabel[] Labels { get; set; }
        }

        public sealed class LinkedPayment
        {
            /// <summary>
            /// Базовый идентификатор
            /// </summary>
            public long DocumentBaseId { get; set; }
        
            /// <summary>
            /// Номер документа
            /// </summary>
            public string Number { get; set; }

            /// <summary>
            /// Дата документа
            /// </summary>
            public DateTime Date { get; set; }
            
            /// <summary>
            /// Тип платежа
            /// </summary>
            public PaymentForSaleDocumentType PaymentType { get; set; } 
            
            /// <summary>
            /// Сумма связи
            /// </summary>
            public decimal LinkSum { get; set; }
        }

        public class ProductLabel
        {
            /// <summary>
            /// Значение кода маркировки
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Тип кода маркировки
            /// </summary>
            public ProductLabelType Type { get; set; }
        }
        
        public class StockInfo
        {
            /// <summary>
            /// Идентификатор
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// Название
            /// </summary>
            public string Name { get; set; }
        }

        public class NdsDeduction
        {
            /// <summary>
            /// Базовый идентификатор сч-ф (из продаж, с типом "Авансовый") 
            /// </summary>
            public long DocumentBaseId { get; set; }
        
            /// <summary>
            /// Номер документа
            /// </summary>
            public string Number { get; set; }

            /// <summary>
            /// Дата документа
            /// </summary>
            public DateTime Date { get; set; }
                
            /// <summary>
            /// Сумма НДС, принятая к вычету
            /// </summary>
            public decimal Sum { get; set; }
        }
        
        public sealed class LinkedBill
        {
            /// <summary>
            /// Базовый идентификатор
            /// </summary>
            public long DocumentBaseId { get; set; }
        
            /// <summary>
            /// Номер документа
            /// </summary>
            public string Number { get; set; }

            /// <summary>
            /// Дата документа
            /// </summary>
            public DateTime Date { get; set; } 
        }
        
        public sealed class LinkedContract
        {
            /// <summary>
            /// Базовый идентификатор
            /// </summary>
            public long DocumentBaseId { get; set; }
        
            /// <summary>
            /// Номер документа
            /// </summary>
            public string Number { get; set; }

            /// <summary>
            /// Дата документа
            /// </summary>
            public DateTime Date { get; set; } 
            
            /// <summary>
            /// Признак "Основной договор"
            /// </summary>
            public bool IsMain { get; set; }

            /// <summary>
            /// Название договора
            /// </summary>
            public string Name { get; set; }
        }

        #endregion
    }
}