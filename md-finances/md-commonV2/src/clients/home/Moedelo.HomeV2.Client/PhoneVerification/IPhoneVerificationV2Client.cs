using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.PhoneVerification;
using Moedelo.HomeV2.Dto.PhoneVerification;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.PhoneVerification
{
    public interface IPhoneVerificationV2Client : IDI
    {
        Task<CheckFirmIsNeedVerifyPhoneV2Dto> CheckFirmIsNeedVerifyPhoneAdvancedAsync(FirmIsNeedVerifyRequestV2Dto requestDto);

        Task<PhoneVerificationCodeDto> GetPhoneVerificationCodeAsync(PhoneDto phoneDto);

        Task<PhoneVerificationStatusDto> GetPhoneVerificationStatusAsync(string phone, CancellationToken cancellationToken);

        Task<NewPhoneVerificationCodeResponseDto> GenerateCodeAsync(NewPhoneVerificationCodeRequestDto requestDto, CancellationToken cancellationToken);

        Task<PhoneVerificationCodeValidationResult> ValidateAsync(PhoneVerificationCodeValidationRequestDto requestDto, CancellationToken cancellationToken);
    }
}