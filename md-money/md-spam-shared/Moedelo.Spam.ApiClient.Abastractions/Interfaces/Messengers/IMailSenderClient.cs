using Moedelo.Spam.ApiClient.Abastractions.Dto.Messengers;
using System.Threading.Tasks;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.Messengers
{
    public interface ISkypeSenderClient
    {
        Task SendAsync(SkypeSendOptionsDto request);
    }
}