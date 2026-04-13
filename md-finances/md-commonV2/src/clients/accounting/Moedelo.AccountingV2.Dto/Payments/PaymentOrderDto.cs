using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Payroll;
using WorkerPaymentType = Moedelo.Common.Enums.Enums.Payroll.WorkerPaymentType;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class PaymentOrderDto
    {
        public PaymentOrderDto(string paymentNumber, string orderDate, decimal sum)
        {
            PaymentNumber = paymentNumber;
            OrderDate = orderDate;
            Sum = sum;
        }

        public PaymentOrderDto()
        {
        }

        public int WorkerId { get; set; }

        /// <summary>
        /// Наименование получателя, если сотрудник то ФИО, если БП - фонд
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// сформированный PaymentOrder переведенный в строку XmlHelper.Serialize()
        /// </summary>
        public string PaymentOrder { get; set; }

        /// <summary> Номер платежа </summary>
        public string PaymentNumber { get; set; }

        /// <summary> Дата платежа </summary>
        public string OrderDate { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Для Бюд. ПП код счета 
        /// </summary>
        public int BudgetaryTaxesAndFees { get; set; }

        /// <summary>
        /// Для Бюд. ПП тип КБК
        /// </summary>
        public int KbkType { get; set; }

        /// <summary>
        /// Период за который мы платим
        /// </summary>
        public string PeriodEndDate { get; set; }

        /// <summary>
        /// Тип выплаты сотруднику зп, ГПД или как подотчетное лицо
        /// </summary>
        public WorkerPaymentType WorkerType { get; set; }

        /// <summary>
        /// Учесть в
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// ПП или квитанция
        /// </summary>
        public PaymentMethodType DocumentType { get; set; }
    }
}