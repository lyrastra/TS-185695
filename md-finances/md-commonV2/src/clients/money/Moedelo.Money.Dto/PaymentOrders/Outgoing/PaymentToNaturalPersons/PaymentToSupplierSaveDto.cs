using System;
using System.Collections.Generic;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    /// <summary>
    /// Модель для сохранения операции "Выплаты физ. лицам"
    /// </summary>
    public class PaymentToNaturalPersonsSaveDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Начисления
        /// </summary>
        public IReadOnlyCollection<EmployeePaymentsSaveDto> EmployeePayments { get; set; }

        /// <summary>
        /// Тип начисления
        /// </summary>
        public PaymentToNaturalPersonsType PaymentType { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Оплачен
        /// </summary>
        public bool IsPaid { get; set; }
    }
}