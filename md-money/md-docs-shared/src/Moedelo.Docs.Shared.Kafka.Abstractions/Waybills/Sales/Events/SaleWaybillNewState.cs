using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events
{
    public sealed class SaleWaybillNewState
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
        /// Документ прошлого периода: дата
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }
        
        /// <summary>
        /// Документ прошлого периода: номер
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }
        
        /// <summary>
        /// Флаг "НДС"
        /// </summary>
        public bool UseNDS { get; set; }

        /// <summary>
        /// Тип начисления НДС (сверху/в т.ч.)
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }
        
        /// <summary> Грузоотправитель </summary>
        public int? SenderId { get; set; }

        /// <summary> Поставщик </summary>
        public int? SupplierId { get; set; }
        
        /// <summary>
        /// Грузополучатель
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }
        
        /// <summary>
        /// Сумма с НДС
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Включен флаг "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }
        
        /// <summary>
        /// Тип накладной
        /// </summary>
        public WaybillTypeCode TypeCode { get; set; }

        /// <summary>
        /// Статус документа (Подписан/Скан/Нет)
        /// </summary>
        public SignStatus SignStatus { get; set; }

        /// <summary> 
        /// Откуда был создан документ 
        /// </summary>
        public CreationPlace CreatedFrom { get; set; }

        /// <summary>
        /// Провести в НУ
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; }
        
        /// <summary>
        /// Провести в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Учесть в системе налогообложения
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Счет контрагента
        /// </summary>
        public int KontragentAccountCode { get; set; }
        
        /// <summary>
        /// Идентификатор субконто
        /// </summary>
        public long SubcontoId { get; set; }
        
        /// <summary>
        /// Позиции накладной
        /// </summary>
        public Item[] Items { get; set; }
        
        /// <summary>
        /// по договору
        /// </summary>
        public LinkedContract Contract { get; set; }

        /// <summary>
        /// по счету
        /// </summary>
        public LinkedBill Bill { get; set; }
        
        /// <summary>
        /// Списать со склада
        /// </summary>
        public long? StockId { get; set; }
        
        /// <summary>
        /// Документ о покупке (в накладной на возврат; накладные из покупок)
        /// </summary>
        public LinkedWaybill[] ReasonWaybills { get; set; }
        
        /// <summary>
        /// Счет-фактура
        /// </summary>
        public LinkedInvoice Invoice { get; set; }
        
        /// <summary>
        /// Платежи
        /// </summary>
        public LinkedPayment[] Payments { get; set; }

        /// <summary>
        /// Передана только ЧАСТЬ ДАННЫХ! Модель обогатить через API в случае необходимости.
        /// </summary>
        public bool IsTruncated { get; set; }

        public sealed class LinkedInvoice
        {
            /// <summary>
            /// Базовый идентификатор сч-ф (из продаж, с типом "Обычный") 
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
            /// Принять НДС к вычету
            /// </summary>
            public NdsDeduction[] NdsDeductions { get; set; }
            
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
        
        public sealed class Item
        {
            /// <summary>
            /// Наименование
            /// </summary>
            public string Name { get; set; }
        
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
            /// Идентификатор товара
            /// </summary>
            public long StockProductId { get; set; }

            /// <summary>
            /// Ставка НДС
            /// </summary>
            public NdsType NdsType { get; set; }

            /// <summary>
            /// Ед. изм.
            /// </summary>
            public string Unit { get; set; }

            /// <summary>
            /// Скидка (%)
            /// </summary>
            public decimal? DiscountRate { get; set; }

            /// <summary>
            /// Идентификатор кода операции (если ставка без НДС) 
            /// </summary>
            public int? NdsDeclarationSection7CodeId { get; set; }
        }
        
        public sealed class LinkedWaybill
        {
            /// <summary>
            /// Базовый идентификатор накладной (из покупок) 
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
    }
}