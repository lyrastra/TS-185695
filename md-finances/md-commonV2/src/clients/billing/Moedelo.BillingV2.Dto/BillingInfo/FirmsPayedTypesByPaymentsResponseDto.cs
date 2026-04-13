using System.ComponentModel;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public enum FirmsPayedTypesByPayment
    {
        [Description("Неизвестно")]
        None = 0,

        [Description("Не оплачен и не триал")]
        NotPayed = 1,

        [Description("Оплачен")]
        Payed = 2,

        [Description("Триал")]
        Trial = 3
    }

    public class FirmsPayedTypesByPaymentsResponseDto
    {
        public int FirmId { get; set; }

        public FirmsPayedTypesByPayment PayedType { get; set; }
    }
}