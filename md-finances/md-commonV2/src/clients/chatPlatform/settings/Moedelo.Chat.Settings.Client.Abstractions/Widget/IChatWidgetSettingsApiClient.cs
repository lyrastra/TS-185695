using System.Threading;
using System.Threading.Tasks;
using Moedelo.Chat.Settings.Client.Abstractions.Widget.Dto;

namespace Moedelo.Chat.Settings.Client.Abstractions.Widget
{
    public interface IChatWidgetSettingsApiClient
    {
        Task<ChatWidgetSettingsDto> GetWidgetSettingsAsync(
            CancellationToken cancellationToken = default);
    }
}
