using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Consul;

public interface IConsulCatalogWatcher
{
    Task AddWatchCatalogAsync(string keyPath, Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange, int delayRetrySec = 120);
        
    Task AddStaticCatalogAsync(string keyPath, Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange);
}