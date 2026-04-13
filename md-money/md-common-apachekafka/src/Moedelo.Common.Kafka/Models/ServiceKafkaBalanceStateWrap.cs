using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Kafka.Models;

internal class ServiceKafkaBalanceStateWrap
{
    private ServiceKafkaBalanceState state;

    public ServiceKafkaBalanceStateWrap()
    {
        this.JsonString = string.Empty;
        state = null;
    }

    public ServiceKafkaBalanceStateWrap(string jsonString)
    {
        JsonString = jsonString;
        state = jsonString.FromJsonString<ServiceKafkaBalanceState>();
    }

    public delegate Task OnChangeHandler(ServiceKafkaBalanceState state);
    public event OnChangeHandler OnChange;

    public Task SetJsonStringAsync(string value)
    {
        if (value == JsonString)
        {
            return Task.CompletedTask;
        }

        JsonString = value ?? string.Empty;
        state = value != null ? JsonString.FromJsonString<ServiceKafkaBalanceState>() : null;
        var handlers = OnChange?
            .GetInvocationList()
            .OfType<OnChangeHandler>()
            .ToArray();
        if (handlers?.Any() == true)
        {
            return RunEventHandlersAsync(handlers, state);
        }

        return Task.CompletedTask;
    }

    private static async Task RunEventHandlersAsync(IEnumerable<OnChangeHandler> handlers, ServiceKafkaBalanceState state)
    {
        foreach (var handler in handlers)
        {
            await handler(state);
        }
    }

    public string JsonString { get; private set; }

    public Task NotifyAboutQuotaIsMissedAsync()
    {
        // форсируем срабатывание уведомлений
        JsonString = string.Empty;

        return SetJsonStringAsync(null);
    }
}
