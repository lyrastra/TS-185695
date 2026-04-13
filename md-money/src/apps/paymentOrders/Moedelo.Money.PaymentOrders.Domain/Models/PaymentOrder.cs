using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class PaymentOrder
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public int FirmId { get; set; }

        public DateTime Date { get; set; }

        public DateTime? SaleDate { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        public int? KontragentId { get; set; }

        /// <summary>
        /// Имя контрагента или сотрудника
        /// </summary>
        public string KontragentName { get; set; }

        public int? TransferSettlementAccountId { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }
        public bool IncludeNds { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// НДС для посредничества
        /// </summary>
        public NdsType? MediationNdsType { get; set; }
        public decimal? MediationNdsSum { get; set; }

        public string PaymentSnapshot { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public MoneyDirection Direction { get; set; }

        public OperationType OperationType { get; set; }

        public BankDocType OrderType { get; set; }

        public PaymentPriority PaymentPriority { get; set; } = PaymentPriority.Fifth;

        public PaymentStatus PaidStatus { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary> Тип периода: ГД, ПЛ, КВ, МС </summary>
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }

        /// <summary>
        /// Номер в периоде: для МС — номер месяца, КВ — номер кваратала, ПЛ – номер полугодия, для ГД — 0.
        /// </summary>
        public int? BudgetaryPeriodNumber { get; set; }

        /// <summary> Год платежа </summary>
        public int? BudgetaryPeriodYear { get; set; }

        public DateTime? BudgetaryPeriodDate { get; set; }

        public BudgetaryAccountCodes? BudgetaryTaxesAndFees { get; set; }

        public int? KbkId { get; set; }

        public KbkPaymentType? KbkPaymentType { get; set; }

        public int? TradingObjectId { get; set; }

        public int? SalaryWorkerId { get; set; }

        public PaymentToNaturalPersonsType? UnderContract { get; set; }

        /// <summary>
        /// Признак долгосрочного займа
        /// </summary>
        public bool? IsLongTermLoan { get; set; }

        /// <summary>
        /// Сумма процентов по займу
        /// </summary>
        public decimal? LoanInterestSum { get; set; }

        /// <summary>
        /// Коммисия для операции эквайринга
        /// </summary>
        public decimal? AcquiringCommission { get; set; }

        /// <summary>
        /// Дата коммисии для операции эквайринга
        /// </summary>
        public DateTime? AcquiringCommissionDate { get; set; }

        /// <summary>
        /// Признак посредничества
        /// </summary>
        public bool? IsMediation { get; set; }

        /// <summary>
        /// Вознаграждение посредника
        /// </summary>
        public decimal? MediationCommission { get; set; }

        // Учет в БУ/НУ

        /// <summary>
        /// Бух. счет контрагента в виджете БУ
        /// </summary>
        public int? KontragentAccountCode { get; set; }

        /// <summary>
        /// Учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; } = true;

        /// <summary>
        /// Режим виджета БУ/НУ "Учитывать вручную"
        /// </summary>
        public ProvidePostingType PostingsAndTaxMode { get; set; } = ProvidePostingType.Auto;

        /// <summary>
        /// Режим виджета НУ "Учитывать вручную"
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; } = ProvidePostingType.Auto;

        // Технические поля

        public string SourceFileId { get; set; }

        public OperationState OperationState { get; set; }

        public long? DuplicateId { get; set; }

        // Валютные поля

        /// <summary>
        /// Курс
        /// </summary>
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// Курсовая разница
        /// </summary>
        public decimal? ExchangeRateDiff { get; set; }

        /// <summary>
        /// Сумма при покупке валюты:
        /// для списания(оплата покупки валюты) в рублях
        /// для поступления в купленной валюте
        /// </summary>
        public decimal? TotalSum { get; set; }

        /// <summary>
        /// Идентификатор привязанного патента
        /// </summary>
        public long? PatentId { get; set; }

        /// <summary>
        /// Признак, нужно ли игнорировать номер текущей платежки
        /// при генерировании нового номера
        /// </summary>
        public bool? IsIgnoreNumber { get; set; }

        /// <summary>
        /// Признак: целевое поступление
        /// </summary>
        public bool? IsTargetIncome { get; set; }

        /// <summary>
        /// Состояние обработки Аутом
        /// </summary>
        public OutsourceState? OutsourceState { get; set; }
    }
}
