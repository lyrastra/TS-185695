using System.Threading;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Sms;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.Sms
{
    public interface ISmsApiClient
    {
        Task<SendSmsResponseDto> SendAsync(SendSmsRequestDto request, CancellationToken cancellationToken);
    }
}