using Moedelo.BankIntegrations.Enums;
using System;

namespace Moedelo.BankIntegrations.Models.PaymentOrder
{
    /// <summary>  Реквизиты плательщика в п/п </summary>
    public class PaymentOrder
    {
        public OrderDetails Payer { get; set; }

        /// <summary>  Реквизиты получателя платежа  </summary>
        public OrderDetails Recipient { get; set; }

        /// <summary> Флаг платежа нерезиденту </summary>
        public bool? IsNonResidentPayment { get; set; }

        /// <summary> Номер платежа </summary>
        public string PaymentNumber { get; set; }

        /// <summary> Дата платежа </summary>
        public DateTime OrderDate { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Sum { get; set; }

        /// <summary> Тип НДС </summary>
        public NdsType NdsType { get; set; }

        /// <summary> Сумма НДС </summary>
        public decimal NdsSum { get; set; }

        /// <summary> Входящая/исходящая платежка  </summary>
        public PaymentDirection Direction { get; set; }

        /// <summary> Назначение платежа(НазначениеПлатежа) </summary>
        public string Purpose { get; set; }

        /// <summary> Назначение платежа кодовое (КодНазПлатежа) </summary>
        public string PurposeCode { get; set; }

        /// <summary> Поле 21. Очередность платежа(Очередность) </summary>
        public PaymentPriority PaymentPriority { get; set; }

        /// <summary> Поле 101. Статус плательщика </summary>
        public BudgetaryPayerStatus BudgetaryPayerStatus { get; set; }

        /// <summary> Поле 104. Код бюджетной классификации (КБК) </summary>
        public string Kbk { get; set; }

        /// <summary> Поле 105. Код ОКАТО </summary>
        public string BudgetaryOkato { get; set; }

        /// <summary> Поле 106. Показатель основания платежа (ПоказательОснования) </summary>
        public BudgetaryPaymentBase BudgetaryPaymentBase { get; set; }

        /// <summary> Поле 107. Показатель налогового периода(ПоказательПериода) </summary>
        public BudgetaryPeriod BudgetaryPeriod { get; set; }

        /// <summary> Поле 108. Показатель номера документа(ПоказательНомера) </summary>
        public string BudgetaryDocNumber { get; set; }

        /// <summary> Поле 109. Показатель даты документа(ПоказательДаты) </summary>
        public DateTime? BudgetaryDocDate { get; set; }

        /// <summary> Поле 110. Показатель типа платежа (ПоказательТипа) не используется, проставляется 0 </summary>
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }

        /// <summary> 
        /// «Вид платежа» заполняется, только если делается «электронный» платеж,
        ///  то есть вы отправляете платежку через систему «Банк-клиент». 
        /// Здесь так и пишется: «электронно».  
        /// </summary>
        public string KindOfPay { get; set; }

        /// <summary> Поле 2. Эта цифра всегда одинакова и неизменна (0401060). И означает она номер унифицированной формы платежного поручения </summary>
        public string NumberTop { get; set; }

        /// <summary>  Вид документа 
        /// «01» — Платёжное поручение,
        /// «02» — Платёжное требование,
        /// 09» — Мемориальный ордер  </summary>
        public BankDocType BankDocType { get; set; }

        /// <summary> Уникальный идентификатор платежа (поле 22). С 31 марта 2014 года согласно Указанию N 3025-У ЦБР. (Код= ) </summary>
        public string CodeUin { get; set; }

        /// <summary> поле не идет в бланк, оно для определения типа операци при импорте выписок </summary>
        public OrderType OrderType { get; set; }

        /// <summary> Поле для индентификации платежа внутри между микросервисами </summary>
        public Guid Guid { get; set; }
    }
}
