namespace Moedelo.SberbankCryptoEndpointV2.Dto.Sso
{
    public class UserInfoResponseDto
    {
        public string Iss { get; set; }

        public string Sub { get; set; }

        public string Aud { get; set; }

        public string Fio { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string OrganizationIdHash { get; set; }

        public string OrganizationName { get; set; }

        public string Error { get; set; }
    }
}