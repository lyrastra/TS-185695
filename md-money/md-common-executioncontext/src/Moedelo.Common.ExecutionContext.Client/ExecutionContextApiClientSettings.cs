using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Client;

[InjectAsSingleton(typeof(IExecutionContextApiClientSettings))]
internal sealed class ExecutionContextApiClientSettings : IExecutionContextApiClientSettings
{
    private const int DefaultRetryCount = 3;
    private const int DefaultRetryDelayMs = 50;
    private const int DefaultMaxCallDurationMs = 3000;
    private readonly SettingValue apiEndpoint;
    private readonly IntSettingValue retryCount;
    private readonly IntSettingValue retryDelayMs;
    private readonly IntSettingValue maxCallDurationMs;

    private readonly ConcurrentDictionary<string, IReadOnlyDictionary<ExecutionContextApiMethod, string>> urlCache = new();
    private readonly ConcurrentDictionary<int, HttpQuerySetting> querySettingsCache = new();


    public ExecutionContextApiClientSettings(ISettingRepository settingRepository)
    {
        apiEndpoint = settingRepository.Get("AuthContextApiEndpoint").ThrowExceptionIfNull(true);
        retryCount = settingRepository.GetInt("AuthContextApiEndpointMaxRetryCount", DefaultRetryCount);
        retryDelayMs = settingRepository.GetInt("AuthContextApiEndpointRetryDelayMilliseconds", DefaultRetryDelayMs);
        maxCallDurationMs = settingRepository.GetInt("AuthContextApiEndpointMaxCallDurationMilliseconds", DefaultMaxCallDurationMs);
    }

    public string GetApiMethodUri(ExecutionContextApiMethod method)
    {
        return urlCache.GetOrAdd(apiEndpoint.Value, CreateEndpointApiMethodsMap)[method];
    }

    private static IReadOnlyDictionary<ExecutionContextApiMethod, string> CreateEndpointApiMethodsMap(string endpoint)
    {
        return new Dictionary<ExecutionContextApiMethod, string>
        {
            [ExecutionContextApiMethod.FromPublic] = $"{endpoint}/private/api/Token/FromPublic",
            [ExecutionContextApiMethod.FromUserContext] = $"{endpoint}/private/api/Token/FromUserContext",
            [ExecutionContextApiMethod.FromApiKey] = $"{endpoint}/private/api/Token/FromApiKeyMd5",
            [ExecutionContextApiMethod.Unidentified] = $"{endpoint}/private/api/Token/Unidentified"
        };
    }

    public int RetryCount => retryCount.Value;
    public TimeSpan RetryDelay => TimeSpan.FromMilliseconds(retryDelayMs.Value);

    public HttpQuerySetting QuerySettings => querySettingsCache
        .GetOrAdd(
            maxCallDurationMs.Value,
            static milliseconds => new HttpQuerySetting(TimeSpan.FromMilliseconds(milliseconds)));
}
