using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.Consul;

// ReSharper disable once ClassNeverInstantiated.Global
[InjectAsSingleton(typeof(IConsulCatalogWatcher))]
internal sealed class ConsulCatalogWatcher : ConsulCatalogWatcherBase
{
    private readonly IConsulHttpApiClient consulHttpApiClient;

    public ConsulCatalogWatcher(IConsulHttpApiClient consulHttpApiClient)
    {
        this.consulHttpApiClient = consulHttpApiClient;
    }

    protected override Task<HttpResponseMessage> CallHttpGetConsulKvApiAsync(
        string keyPath,
        string uriQuery,
        CancellationToken cancellationToken,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return consulHttpApiClient.GetAsync(keyPath, uriQuery, cancellationToken);
    }
}
