using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Purchases.Events
{
    public class PurchaseWaybillNewState
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
        /// Компенсируется заказчиком
        /// </summary>
        public bool IsCompensatedByCustomer => MiddlemanContract != null;
        
        /// <summary>
        /// Посреднический договор
        /// </summary>
        public LinkedMiddlemanContract MiddlemanContract { get; set; }
        
        /// <summary>
        /// Флаг "НДС"
        /// </summary>
        public bool UseNds { get; set; }

        /// <summary>
        /// Тип начисления НДС (сверху/в т.ч.)
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }
        
        /// <summary>
        /// Сумма с НДС
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Тип накладной
        /// </summary>
        public WaybillTypeCode TypeCode { get; set; }

        /// <summary>
        /// Накладная создана из ВА (Имущество)
        /// </summary>
        public bool IsFromFixedAssetInvestment { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public virtual bool? IncludeNdsInFixedAsset { get; set; }
        
        /// <summary>
        /// Оплачено в остатках
        /// </summary>
        public decimal? BalanceSum { get; set; }

        /// <summary> 
        /// Откуда был создан документ 
        /// </summary>
        public CreationPlace CreatedFrom { get; set; }

        /// <summary>
        /// Несоответствие по количеству/качеству
        /// </summary>
        public virtual bool DiscrepancyNumberOrQuality { get; set; }

        /// <summary>
        /// Провести в НУ
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; }
        
        /// <summary>
        /// Провести в БУ
        /// </summary>
        public virtual bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Учесть в системе налогообложения
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Счет контрагента
        /// </summary>
        public int KontragentAccountCode { get; set; }
        
        /// <summary>
        /// Позиции накладной
        /// </summary>
        public Item[] Items { get; set; }
        
        /// <summary>
        /// по договору
        /// </summary>
        public LinkedContract Contract { get; set; }
        
        /// <summary>
        /// Оприходовать на склад
        /// </summary>
        public long? StockId { get; set; }
        
        /// <summary>
        /// Идентификатор субконто
        /// </summary>
        public long SubcontoId { get; set; }
        
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

        public class LinkedInvoice
        {
            /// <summary>
            /// Базовый идентификатор сч-ф (из покупок, с типом "Обычный") 
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
            
            /// <summary>
            /// Восстановить НДС
            /// </summary>
            public NdsRecover[] NdsRecovers { get; set; }
            
            public class NdsDeduction
            {
                /// <summary>
                /// Инедентификатор вычета
                /// </summary>
                public long Id { get; set; }

                /// <summary>
                /// Дата вычета
                /// </summary>
                public DateTime Date { get; set; }
                
                /// <summary>
                /// Сумма НДС, принятая к вычету
                /// </summary>
                public decimal Sum { get; set; }
            }
            
            public class NdsRecover
            {
                /// <summary>
                /// Базовый идентификатор сч-ф/УПД (из покупок, с типом "Авансовый") 
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
                /// Сумма НДС к восстановлению
                /// </summary>
                public decimal Sum { get; set; }
            }
        }
        
        public class LinkedPayment
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
            public PaymentForPurchaseDocumentType PaymentType { get; set; } 
            
            /// <summary>
            /// Сумма связи
            /// </summary>
            public decimal LinkSum { get; set; }
        }
        
        public class LinkedContract
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
            /// Название
            /// </summary>
            public string Name { get; set; }
        }
        
        public class LinkedMiddlemanContract
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
            /// Название
            /// </summary>
            public string Name { get; set; }
            
            /// <summary>
            /// Идентификатор заказчика
            /// </summary>
            public int CustomerId { get; set; }
        }
        
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
            /// Фактическое количество (при несоответствии кол-ва/кач-ва)
            /// </summary>
            public decimal RealCount { get; set; }

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
            public virtual decimal NdsSum { get; set; }

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
            public virtual NdsType NdsType { get; set; }
        }
    }
}