using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePaymentsSaveModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }
        
        public IReadOnlyCollection<ChargePayment> ChargePayments { get; set; }

        #region поля "без сотрудника" (сохранение из импорта/подтверждение из аута)

        /// <summary>
        /// ИНН получателя платежа
        /// </summary>
        public string PayeeInn { get; set; }

        /// <summary>
        /// Счёт получателя платежа
        /// </summary>
        public string PayeeAccount { get; set; }

        /// <summary>
        /// ФИО получателя платежа
        /// </summary>
        public string PayeeName { get; set; }

        #endregion
    }
}
