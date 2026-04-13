using Moedelo.ErptV2.Dto.SendReports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.NotSendedReports;

namespace Moedelo.ErptV2.Client.SendReports
{
    public interface ISendReportsClient: IDI
    {
        Task<GetCodeResponseDto> GetCodeAsync(int firmId, int userId);

        Task<bool> CheckCodeAsync(int firmId, int userId, string code);

        Task<SendCodeResponseDto> SendAsync(int firmId, int userId, SendReportsRequestDto dto);

        Task<GetNotSentReportsResponseDto> GetNotSentReportsAsync(GetNotSentReportsRequestDto dto);
    }
}
