using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing
{
    public class PaymentToAccountablePersonWithMissingEmployeeDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public int SettlementAccountId { get; set; }
        public string Description { get; set; }

        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public bool IsIgnoreNumber { get; set; }
        public OutsourceState? OutsourceState { get; set; }

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
    }
}
