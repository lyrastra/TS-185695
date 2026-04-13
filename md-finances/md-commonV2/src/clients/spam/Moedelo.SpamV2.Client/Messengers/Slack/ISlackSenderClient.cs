using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SpamV2.Dto.Messengers;

namespace Moedelo.SpamV2.Client.Messengers.Slack
{
    public interface ISlackSenderClient : IDI
    {
        void Send(SlackSendOptionsDto dto);
        Task SendAsync(SlackSendOptionsDto dto);
    }
}
