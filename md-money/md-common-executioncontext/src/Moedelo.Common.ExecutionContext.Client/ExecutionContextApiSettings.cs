using System;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Client;

internal interface IExecutionContextApiClientSettings
{
    string GetApiMethodUri(ExecutionContextApiMethod method);
    int RetryCount { get; }
    TimeSpan RetryDelay { get; }
    HttpQuerySetting QuerySettings { get; }
}