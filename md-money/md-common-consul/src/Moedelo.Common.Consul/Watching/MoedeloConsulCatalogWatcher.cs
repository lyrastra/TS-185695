using System.Runtime.CompilerServices;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Infrastructure.Consul;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul.Watching;

[InjectAsSingleton(typeof(IMoedeloConsulCatalogWatcher))]
internal sealed class MoedeloConsulCatalogWatcher : ConsulCatalogWatcherBase, IMoedeloConsulCatalogWatcher
{
    private readonly IMoedeloConsulApiClient consulHttpApiClient;

    public MoedeloConsulCatalogWatcher(IMoedeloConsulApiClient consulHttpApiClient)
    {
        this.consulHttpApiClient = consulHttpApiClient;
    }

    protected override async Task<HttpResponseMessage> CallHttpGetConsulKvApiAsync(
        string keyPath,
        string uriQuery,
        CancellationToken cancellationToken,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var auditTrailSpanName = $"{nameof(MoedeloConsulCatalogWatcher)}.{memberName}";

        return await consulHttpApiClient
            .GetAsync(keyPath, uriQuery, cancellationToken, auditTrailSpanName)
            .ConfigureAwait(false);
    }
}
