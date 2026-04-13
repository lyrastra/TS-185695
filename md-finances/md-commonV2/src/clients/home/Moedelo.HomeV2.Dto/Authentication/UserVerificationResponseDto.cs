using Moedelo.Common.Enums.Enums.Home;

namespace Moedelo.HomeV2.Dto.Authentication
{
    public class UserVerificationResponseDto
    {
        public string Token { get; set; }

        public string Login { get; set; }

        public AuthenticationResults Status { get; set; }
    }
}