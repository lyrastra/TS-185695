using System;

namespace Moedelo.HomeV2.Dto.PaymentShift
{
    public class PaymentShiftRequestDto
    {
        public int PaymentId { get; set; }

        public DateTime NewStartDate { get; set; }
    }
}
