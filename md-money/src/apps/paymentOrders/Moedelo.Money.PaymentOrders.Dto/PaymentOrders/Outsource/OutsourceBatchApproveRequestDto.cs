using System;
using System.Collections.Generic;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders
{
    public class OutsourceBatchApproveRequestDto
    {
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }
        public DateTime InitialDate { get; set; }
    }
}
