using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using BudgetaryPaymentType = Moedelo.Common.Enums.Enums.Accounting.BudgetaryPaymentType;

namespace Moedelo.AccountingV2.Dto.PaymentOrder
{
    public class CreatePaymentRequestDto
    {
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
        public string BudgetaryOktmo { get; set; }

        /// <summary> Поле 106. Показатель основания платежа (ПоказательОснования) </summary>
        public BudgetaryPaymentBase BudgetaryPaymentBase { get; set; }

        /// <summary> Поле 107. Показатель налогового периода(ПоказательПериода) </summary>
        public BudgetaryPeriod BudgetaryPeriod { get; set; }

        /// <summary> Поле 110. Показатель типа платежа (ПоказательТипа) </summary>
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }

        /// <summary>  Вид документа 
        /// «01» — Платёжное поручение,
        /// «02» — Платёжное требование,
        /// 09» — Мемориальный ордер  </summary>
        public BankDocType BankDocType { get; set; }

        public string RecipientName { get; set; }

        public string RecipientInn { get; set; }

        public string RecipientKpp { get; set; }

        public string RecipientSettlementNumber { get; set; }

        public string RecipientBankName { get; set; }

        public string RecipientBankBik { get; set; }

        public string RecipientBankCorrespondentAccount { get; set; }
        public int BudgetaryTaxesAndFees { get; set; }
        public int? SettlementAccountId { get; set; }
        public int? KbkId { get; set; }
        
        public Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType BizBudgetaryPaymentType { get; set; }
        public BudgetaryPaymentSubtype BizBudgetaryPaymentSubtype { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
        public long? PatentId { get; set; }
    }
}
