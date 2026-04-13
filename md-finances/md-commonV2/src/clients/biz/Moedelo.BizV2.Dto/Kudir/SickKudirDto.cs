using System;

namespace Moedelo.BizV2.Dto.Kudir
{
    public class SickKudirDto
    {
        public DateTime Date { get; set; }
        public string PaymentOrderNumber { get; set; }
        public decimal Sum { get; set; }
        public bool IsNdfl { get; set; }
    }
}
