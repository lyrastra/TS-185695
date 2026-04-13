using System.Threading.Tasks;
using Moedelo.Dss.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Dss.Client.QrProtectionCode
{
    public interface IDssQrProtectionCodeClient : IDI
    {
        Task<QrProtectionCodeVerifyResultDto> SendVerificationCodeAsync(string abnGuid);
        Task<QrProtectionCodeVerifyResultDto> VerifyCodeAsync(string abnGuid, string code);
    }
}