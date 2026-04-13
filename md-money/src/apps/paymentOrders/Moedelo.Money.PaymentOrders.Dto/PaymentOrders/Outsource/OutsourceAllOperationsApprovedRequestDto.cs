using System;
using System.Collections.Generic;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outsource
{
    public class OutsourceAllOperationsApprovedRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Учитывать только оплаченные операции
        /// </summary>
        public bool IsOnlyPaid { get; set; }
    }
}
