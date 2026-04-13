using System.Collections.Generic;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Http.Abstractions.Headers;

[InjectAsSingleton(typeof(IAuthHeadersGetter))]
internal sealed class AuthHeadersGetter : IAuthHeadersGetter
{
    private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;

    public AuthHeadersGetter(IExecutionInfoContextAccessor executionInfoContextAccessor)
    {
        this.executionInfoContextAccessor = executionInfoContextAccessor;
    }

    public IEnumerable<KeyValuePair<string, string>> EnumerateHeaders()
    {
        var token = executionInfoContextAccessor.ExecutionInfoContextToken;

        if (token != null)
        {
            yield return new KeyValuePair<string, string>("Authorization", $"Bearer {token}");
        }
    }
}
