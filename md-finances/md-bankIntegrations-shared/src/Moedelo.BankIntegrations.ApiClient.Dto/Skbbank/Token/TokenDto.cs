namespace Moedelo.BankIntegrations.ApiClient.Dto.Skbbank
{
    public class TokenDto
    {
        public AccessTokenModel AccessToken { get; set; }

        public RefreshTokenModel RefreshToken { get; set; }

        public class AccessTokenModel
        {
            public string Value { get; set; }
        }

        public class RefreshTokenModel
        {
            public string Value { get; set; }
        }

    }
}