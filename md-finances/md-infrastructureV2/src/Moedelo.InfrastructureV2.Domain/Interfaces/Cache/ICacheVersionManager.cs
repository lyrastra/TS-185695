using System;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Cache;

public interface ICacheVersionManager
{
    bool? IsCurrentVersionValid { get; }

    string Version { get; }

    Task ChangeVersionAsync();

    Task<bool> UpdateCurrentVersionAsync(Action<string, string> func = null);

    void Reset();
}