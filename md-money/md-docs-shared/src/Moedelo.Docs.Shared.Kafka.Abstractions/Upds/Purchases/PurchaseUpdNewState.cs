using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases
{
    public class PurchaseUpdNewState
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
        /// Номер забытого документа, который вводится в настроящем за прошлый период
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public KontragentInfo Kontragent { get; set; }

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
        /// по договору
        /// </summary>
        public LinkedContract Contract { get; set; }
        
        /// <summary>
        /// Счет-фактура (на ОСНО при статусе = 2 (только док-т)
        /// </summary>
        public LinkedInvoice Invoice { get; set; }
        
        /// <summary>
        /// Принять НДС к вычету
        /// (!) при статусе УПД = 1 (док-т и сч-ф) вычеты НДС в самом УПД, при статусе = 2 - в связанном сч-ф
        /// </summary>
        public NdsDeduction[] NdsDeductions { get; set; }
        
        /// <summary>
        /// Восстановить НДС
        /// (!) при статусе УПД = 1 (док-т и сч-ф) воссановление НДС в самом УПД, при статусе = 2 - в связанном сч-ф
        /// </summary>
        public NdsRecover[] NdsRecovers { get; set; }

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
        /// На 14.07.2023 имеем 3 больших документов из ~1_400_000 (+3 за полгода).
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
            /// Рассчитанные автоматически суммы отредактированы пользователем
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
            
            /// <summary> Счет расхода во входящем УПД с типом услуга </summary>
            public CostSyntheticAccountCode? ActivityAccountCode { get; set; }

            /// <summary>
            /// Страна импортируемого товара
            /// </summary>
            public string GtdCountry { get; set; }

            /// <summary>
            /// ГТД импортируемого товара
            /// </summary>
            public string GtdNumber { get; set; }
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
            public PaymentForPurchaseDocumentType PaymentType { get; set; } 
            
            /// <summary>
            /// Сумма связи
            /// </summary>
            public decimal LinkSum { get; set; }
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
        }
        
        public class NdsDeduction
        {
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
        
        public sealed class KontragentInfo
        {
            /// <summary>
            /// Идентификатор
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Название
            /// </summary>
            public string Name { get; set; }
        }

        #endregion
    }
}