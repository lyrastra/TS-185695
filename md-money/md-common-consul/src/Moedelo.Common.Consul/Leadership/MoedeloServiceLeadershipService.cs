using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Consul.Internals;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul.Leadership;

[InjectAsSingleton(typeof(IMoedeloServiceLeadershipService))]
internal sealed class MoedeloServiceLeadershipService : IMoedeloServiceLeadershipService
{
    private readonly ISettingsConfigurations settingsConfigurations;
    private readonly IMoedeloConsulSessionalKeyValueApiClient mdConsulClient;

    public MoedeloServiceLeadershipService(
        ISettingsConfigurations settingsConfigurations,
        IMoedeloConsulSessionalKeyValueApiClient mdConsulClient)
    {
        this.settingsConfigurations = settingsConfigurations;
        this.mdConsulClient = mdConsulClient;
    }

    private string LeadershipRootDirectoryPath => $"{settingsConfigurations.Config.Environment}/runtime/election";

    private string GetLeadershipKeyPath(string leadershipName)
    {
        return Path.Combine(LeadershipRootDirectoryPath,
                settingsConfigurations.Config.Domain,
                settingsConfigurations.Config.AppName,
                leadershipName,
                "leader")
            .Replace('\\', '/');
    }

    public string ConsulSessionId => mdConsulClient.ConsulSessionId;

    public ValueTask<bool> AcquireLeadershipAsync(
        string leadershipName,
        CancellationToken cancellationToken)
    {
        var keyPath = GetLeadershipKeyPath(leadershipName);
        var keyValue = $"{Env.MachineName}:{Env.EntryAssemblyName}:PID{Environment.ProcessId}";

        return mdConsulClient.AcquireKeyValueAsync(keyPath, keyValue, cancellationToken);
    }

    public ValueTask ReleaseLeadershipAsync(string leadershipName, CancellationToken cancellationToken)
    {
        var keyPath = GetLeadershipKeyPath(leadershipName);

        return mdConsulClient.ReleaseAcquiredKeyValueAsync(keyPath, cancellationToken);
    }
}
