using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SpamV2.Dto.SmsSender;

namespace Moedelo.SpamV2.Client.SmsSender
{
    public interface ISmsSenderClient : IDI
    {
        [Obsolete("Используй md-spam-shared/ISmsApiClient.SendAsync")]
        Task<List<SmsSendResponseDto>> SendSmsAsync(SmsSendRequestDto dto);
    }
}