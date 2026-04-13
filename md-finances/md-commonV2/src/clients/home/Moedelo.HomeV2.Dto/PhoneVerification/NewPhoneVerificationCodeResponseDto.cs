namespace Moedelo.HomeV2.Dto.PhoneVerification
{
    public class NewPhoneVerificationCodeResponseDto
    {
        public string Code { get; set; }

        public string CodeSalt { get; set; }

        public string CodeHash { get; set; }
    }
}