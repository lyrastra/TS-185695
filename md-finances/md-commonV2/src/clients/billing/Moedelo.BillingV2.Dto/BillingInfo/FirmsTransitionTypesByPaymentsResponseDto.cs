using System.ComponentModel;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public enum FirmTransitionTypeByPayments
    {
        [Description("Неизвестно")]
        None = 0,

        [Description("Переход с триала")]
        TransitionFromTrial = 1,

        [Description("Переход с регистрации ИП/ООО")]
        TransitionFromRegistrationMasterTrial = 2,

        [Description("Продление")]
        Prolongation = 3
    }

    public class FirmsTransitionTypesByPaymentsResponseDto
    {
        public int FirmId { get; set; }

        public FirmTransitionTypeByPayments TransitionType { get; set; }
    }
}