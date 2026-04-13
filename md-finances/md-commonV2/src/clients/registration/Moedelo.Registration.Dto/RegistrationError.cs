using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.Registration.Dto
{
    public class RegistrationError
    {
        public RegistrationErrorCode ErrorCode { get; set; }

        public string ErrorMessages { get; set; }
    }
}