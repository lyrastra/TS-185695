using System;

namespace Moedelo.CommonV2.EventBus.Billing
{
    /// <summary>
    /// Изменение первой даты начала платежа
    /// </summary>
    public class ChangeFirstStartPaymentDateEvent
    {
        public int PaymentId { get; set; }

        public int FirmId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
