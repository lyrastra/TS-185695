using Moedelo.Accounting.Enums;
using Moedelo.Accounting.Enums.PaymentOrder;
using Moedelo.Accounting.Enums.PaymentOrder.BudgetaryPayment;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PaymentOrder
{
    public class GetFileRequestDto
    {
        public FileFormat Format { get; set; }

        /// <summary> Номер платежа </summary>
        public string PaymentNumber { get; set; }

        /// <summary> Дата платежа </summary>
        public string OrderDate { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Sum { get; set; }

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
        public string BudgetaryDocDate { get; set; }

        /// <summary> Поле 110. Показатель типа платежа (ПоказательТипа) </summary>
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }

        /// <summary>  Вид документа 
        /// «01» — Платёжное поручение,
        /// «02» — Платёжное требование,
        /// 09» — Мемориальный ордер  </summary>
        public BankDocType BankDocType { get; set; }

        public string PayerName { get; set; }

        public string PayerInn { get; set; }

        public string PayerKpp { get; set; }

        public string PayerSettlementNumber { get; set; }

        public string PayerBankName { get; set; }

        public string PayerBankBik { get; set; }

        public string PayerBankCorrespondentAccount { get; set; }

        public bool PayerIsOoo { get; set; }

        public string RecipientName { get; set; }

        public string RecipientInn { get; set; }

        public string RecipientKpp { get; set; }

        public string RecipientSettlementNumber { get; set; }

        public string RecipientBankName { get; set; }

        public string RecipientBankBik { get; set; }

        public string RecipientBankCorrespondentAccount { get; set; }

        public string PayerBankCity { get; set; }

        public PaymentDirection PaymentDirection { get; set; }
    }
}