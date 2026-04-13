using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.SmsSender;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.SmsSender
{
    public interface ISmsSenderClient
    {
        [Obsolete("Используй ISmsApiClient.SendAsync")]
        Task<List<SmsSendResponseDto>> SendSmsAsync(SmsSendRequestDto dto);
    }
}