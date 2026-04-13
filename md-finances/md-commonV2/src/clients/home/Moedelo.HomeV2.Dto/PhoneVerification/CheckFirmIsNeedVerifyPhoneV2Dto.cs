using Moedelo.Common.Enums.Enums.PhoneVerification;

namespace Moedelo.HomeV2.Dto.PhoneVerification
{
    public class CheckFirmIsNeedVerifyPhoneV2Dto
    {
        public PhoneVerificationNeed PhoneVerificationNeed { get; set; }

        public string RedirectUrl { get; set; }
    }
}