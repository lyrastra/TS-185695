using System;

namespace Moedelo.BackofficeV2.Dto.SendBill
{
    public class SendBillItemRequestDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string BillNumber { get; set; }
    }
}