using System;
using Moedelo.Common.Enums.Enums.Integration;
using BankDocType = Moedelo.Common.Enums.Enums.Accounting.BankDocType;
using BudgetaryPayerStatus = Moedelo.Common.Enums.Enums.Accounting.BudgetaryPayerStatus;
using BudgetaryPaymentBase = Moedelo.Common.Enums.Enums.FinancialOperations.BudgetaryPaymentBase;
using BudgetaryPaymentType = Moedelo.Common.Enums.Enums.Accounting.BudgetaryPaymentType;
using PaymentDirection = Moedelo.Common.Enums.Enums.Accounting.PaymentDirection;
using PaymentPriority = Moedelo.Common.Enums.Enums.Accounting.PaymentPriority;

namespace Moedelo.CashV2.Dto.YandexKassa.PaymentImport
{
    public class PaymentOrderDto
    {
        /// <summary> Реквизиты плательщика </summary>
        public OrderDetailsDto Payer { get; set; }

        /// <summary>  Реквизиты получателя платежа  </summary>
        public OrderDetailsDto Recipient { get; set; }

        /// <summary> Номер платежа </summary>
        public string PaymentNumber { get; set; }

        /// <summary> Дата платежа </summary>
        public DateTime OrderDate { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Sum { get; set; }

        /// <summary> Входящая/исходящая платежка  </summary>
        public PaymentDirection Direction { get; set; }

        /// <summary> Назначение платежа(НазначениеПлатежа) </summary>
        public string Purpose { get; set; }

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
        public BudgetaryPeriodDto BudgetaryPeriod { get; set; }

        /// <summary> Поле 108. Показатель номера документа(ПоказательНомера) </summary>
        public string BudgetaryDocNumber { get; set; }

        /// <summary> Поле 109. Показатель даты документа(ПоказательДаты) </summary>
        public DateTime? BudgetaryDocDate { get; set; }

        /// <summary> Поле 110. Показатель типа платежа (ПоказательТипа) </summary>
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

        public string CodeUin { get; set; }

        /// <summary> поле не идет в бланк, оно для определения типа операци при импорте выписок </summary>
        public OrderType OrderType { get; set; }
    }
}