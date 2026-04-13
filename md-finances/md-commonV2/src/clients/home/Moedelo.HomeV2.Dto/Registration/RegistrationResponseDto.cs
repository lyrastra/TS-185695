using Moedelo.Common.Enums.Enums.Home;

namespace Moedelo.HomeV2.Dto.Registration
{
    public class RegistrationResponseDto
    {
        public RegistrationResponseStatusCode Status { get; set; }

        public string Error { get; set; }

        public string Token { get; set; }
    }
}