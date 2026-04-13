using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SpamV2.Dto.MailSender;

namespace Moedelo.SpamV2.Client.MailSender
{
    public interface IMailSenderClient : IDI
    {
        Task<MailResponseDto> SendAsync(EmailDto request);

        Task<BaseEmailResponseDto> FillAndSendEmailAsync(EmailParamsV2Dto request);

        [Obsolete("Не использовать. Использовать FillAndSendEmailAsync")]
        Task<List<SendEmailWithLabelResponseDto>> FillAndSendEmailsAsync(List<EmailParamsV2Dto> emails);
    }
}
