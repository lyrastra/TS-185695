using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders
{
    public class OutsourceApproveRequestDto
    {
        public long DocumentBaseId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime InitialDate { get; set; }
    }
}
