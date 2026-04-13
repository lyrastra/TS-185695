using Moedelo.Common.Enums.Enums.PostingEngine;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class PaymentToNaturalPersonResponseDto
    {
        public long DocumentBaseId { get; set; }

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
        public IReadOnlyCollection<EmployeePaymentsResponseDto> EmployeePayments { get; set; }

        /// <summary>
        /// Тип начисления
        /// </summary>
        public PaymentToNaturalPersonsType PaymentType { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак: проведено в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderOutgoingForTransferSalary;

        /// <summary>
        /// Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }
    }
}
