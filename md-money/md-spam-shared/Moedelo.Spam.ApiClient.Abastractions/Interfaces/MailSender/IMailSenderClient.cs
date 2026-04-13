using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.MailSender;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.MailSender
{
    public interface IMailSenderClient
    {
        Task<MailResponseDto> SendAsync(EmailDto request);

        Task<BaseEmailResponseDto> FillAndSendEmailAsync(EmailParamsV2Dto request);

        Task<bool> HasMailByTrackingCodeAsync(Guid trackingCode, CancellationToken cancellationToken);
    }
}