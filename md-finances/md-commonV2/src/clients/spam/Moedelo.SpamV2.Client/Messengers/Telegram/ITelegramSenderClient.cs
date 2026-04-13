using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SpamV2.Dto.Messengers;

namespace Moedelo.SpamV2.Client.Messengers.Telegram
{
    public interface ITelegramSenderClient : IDI
    {
        void Send(TelegramSendOptionsDto dto);
        Task SendAsync(TelegramSendOptionsDto dto);
    }
}
