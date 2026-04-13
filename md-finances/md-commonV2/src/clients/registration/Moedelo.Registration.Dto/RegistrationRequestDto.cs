namespace Moedelo.Registration.Dto
{
    public class RegistrationRequestDto
    {
        public RegistrationDataNewDto RegData { get; set; }

        public bool NeedConfirmEmail { get; set; }

        public bool NotAddTrialPayment { get; set; }

        public bool NotSendToEmarsys { get; set; }
    }
}