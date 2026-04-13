using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Reflection;

namespace Moedelo.Common.Consul.Internals;

[InjectAsSingleton(typeof(IMoedeloConsulSessionNamingStrategy))]
internal sealed class MoedeloConsulSessionNamingStrategy : IMoedeloConsulSessionNamingStrategy
{
    private readonly ISettingsConfigurations mdConfiguration;
    private volatile int sessionNumber = 0;

    public MoedeloConsulSessionNamingStrategy(ISettingsConfigurations mdConfiguration)
    {
        this.mdConfiguration = mdConfiguration;
    }

    public string GenerateSessionName()
    {
        var newSessionNumber = Interlocked.Increment(ref this.sessionNumber);
        var domain = mdConfiguration.Config.Domain;
        var appName = mdConfiguration.Config.AppName;

        return $"{Env.MachineName}:{domain}:{appName}:{Env.EntryAssemblyName}:PID{Environment.ProcessId}:N{newSessionNumber}";
    }
}
