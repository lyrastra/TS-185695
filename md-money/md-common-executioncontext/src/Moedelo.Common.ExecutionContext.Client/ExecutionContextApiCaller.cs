using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Client.Exceptions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Client;

[InjectAsSingleton(typeof(IExecutionContextApiCaller))]
internal sealed class ExecutionContextApiCaller : IExecutionContextApiCaller
{
    private readonly IHttpRequestExecuter httpRequestExecutor;
    private readonly IAuditHeadersGetter auditHeadersGetter;
    private readonly IExecutionContextApiClientSettings apiClientSettings;

    public ExecutionContextApiCaller(
        IHttpRequestExecuter httpRequestExecutor,
        IAuditHeadersGetter auditHeadersGetter,
        IExecutionContextApiClientSettings apiClientSettings)
    {
        this.httpRequestExecutor = httpRequestExecutor;
        this.auditHeadersGetter = auditHeadersGetter;
        this.apiClientSettings = apiClientSettings;
    }

    public Task<string> PostWithRetryAsync(ExecutionContextApiMethod apiMethod, string requestJsonBody,
        CancellationToken cancellationToken)
    {
        const string applicationJsonMediaType = "application/json";

        return PostWithRetryAsync(
            apiMethod,
            requestBody: requestJsonBody,
            async static (executor, postParams, cancellation) =>
            {
                using var content = new StringContent(postParams.Body, Encoding.UTF8, applicationJsonMediaType);

                return await executor.PostAsync(
                    postParams.Uri,
                    content,
                    postParams.Headers,
                    postParams.QuerySettings,
                    cancellation);
            },
            cancellationToken);
    }

    public Task<string> PostWithRetryAsync(ExecutionContextApiMethod apiMethod, CancellationToken cancellationToken)
    {
        return PostWithRetryAsync(
            apiMethod,
            requestBody: string.Empty,
            static (executor, postParams, cancellation) =>
                executor.PostAsync(
                    postParams.Uri,
                    postParams.Headers,
                    postParams.QuerySettings,
                    cancellation),
            cancellationToken);
    }

    private readonly record struct PostActionParams(
        string Uri,
        string Body,
        IReadOnlyCollection<KeyValuePair<string, string>> Headers,
        HttpQuerySetting QuerySettings);

    private async Task<string> PostWithRetryAsync(
        ExecutionContextApiMethod apiMethod,
        string requestBody,
        Func<IHttpRequestExecuter, PostActionParams, CancellationToken, Task<string>> postAction,
        CancellationToken cancellationToken)
    {
        var maxAttempts = Math.Max(1, apiClientSettings.RetryCount);
        var uri = apiClientSettings.GetApiMethodUri(apiMethod);
        var attemptDelay = apiClientSettings.RetryDelay;
        var headers = auditHeadersGetter.GetHeaders();

        for (var attemptNum = 0; attemptNum < maxAttempts; ++attemptNum)
        {
            try
            {
                var postActionParams = new PostActionParams(uri, requestBody, headers, apiClientSettings.QuerySettings);

                return await postAction(httpRequestExecutor, postActionParams, cancellationToken);
            }
            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
            {
                if (attemptNum + 1 == maxAttempts)
                {
                    throw new RetryCountLimitExceededException(apiMethod, uri, maxAttempts, exception);
                }

                await Task.Delay(attemptDelay, cancellationToken);
            }
        }

        throw new InvalidOperationException($"Исчерпаны все {maxAttempts} попытки вызвать метод {apiMethod}({uri})");
    }
}
