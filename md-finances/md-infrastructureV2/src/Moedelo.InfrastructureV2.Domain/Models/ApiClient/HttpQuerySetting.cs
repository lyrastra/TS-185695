using System;

namespace Moedelo.InfrastructureV2.Domain.Models.ApiClient;

public class HttpQuerySetting
{
    public HttpQuerySetting(TimeSpan? timeout = null)
    {
        var defaultTimeout = new TimeSpan(0, 1, 40);
        Timeout = timeout.GetValueOrDefault(defaultTimeout);
    }

    public TimeSpan Timeout { get; set; }
    public bool DontThrowOn404 { get; set; }
}