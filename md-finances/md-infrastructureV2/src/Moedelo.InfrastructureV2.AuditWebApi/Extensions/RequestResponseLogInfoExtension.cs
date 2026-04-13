#nullable enable
using System.Net.Http;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.AuditWebApi.Extensions;

internal static class RequestResponseLogInfoExtension
{
    public static async Task<string?> TryGetBodyAsync(this HttpResponseMessage message)
    {
        try
        {
            if (message.IsSuccessStatusCode && message.Content.IsApplicationJson())
            {
                return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public static async Task<string?> TryGetBodyAsync(this HttpRequestMessage message)
    {
        try
        {
            if (message.Method == HttpMethod.Delete || message.Method == HttpMethod.Get)
            {
                // для таких запросов не логируем тело (оно отсутствует)
                return null;
            }

            if (message.Method == HttpMethod.Post || message.Method == HttpMethod.Put)
            {
                return await GetParamsFromContentAsync(message).ConfigureAwait(false);
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    private static async Task<string?> GetParamsFromContentAsync(HttpRequestMessage message)
    {
        if (message.Content.IsApplicationJson())
        {
            return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        return null;
    }

    private static bool IsApplicationJson(this HttpContent content)
    {
        return content?.Headers?.ContentType?.MediaType == "application/json";
    }
}