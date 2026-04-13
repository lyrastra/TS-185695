#nullable enable
using System;
using System.Configuration;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.Consul.NetFramework;

[InjectAsSingleton(typeof(IConsulEndpointConfig))]
internal sealed class ConsulEndpointConfig : IConsulEndpointConfig
{
#if DEBUG
    private const string DefaultConsulHost = "localhost";
#else
        private const string DefaultConsulHost = "org.moedelo.consul";
#endif

    private const int DefaultConsulPort = 8500;
    private const string ConsulHostVarName = "CONSUL_HOST";
    private const string ConsulPortVarName = "CONSUL_PORT";

    private readonly Lazy<ConsulServiceAddress> lazyConfig = new(() =>
        TryGetFromEnvironment()
        ?? TryGetFromAppConfig()
        ?? new ConsulServiceAddress(DefaultConsulHost, DefaultConsulPort));

    /// <summary>
    /// получение из разных источников, причем если в источнике мы получили оба парметра
    /// то результат готов и к следующему не переходим.
    /// Источники по приоритету:
    /// 1 чтение из переменных окружения (CONSUL_HOST, CONSUL_PORT)
    /// 2 чтение из конфига приложения
    /// 3 значения по умолчанию
    /// 
    /// </summary>
    /// <returns></returns>
    public ConsulServiceAddress GetConfig() => lazyConfig.Value;

    private static ConsulServiceAddress? TryGetFromEnvironment()
    {
        var host = Environment.GetEnvironmentVariable(ConsulHostVarName, EnvironmentVariableTarget.Process) ?? 
                   Environment.GetEnvironmentVariable(ConsulHostVarName, EnvironmentVariableTarget.Machine);

        var port = Environment.GetEnvironmentVariable(ConsulPortVarName, EnvironmentVariableTarget.Process) ?? 
                   Environment.GetEnvironmentVariable(ConsulPortVarName, EnvironmentVariableTarget.Machine);

        return Create(host, port);
    }

    private static ConsulServiceAddress? TryGetFromAppConfig()
    {
        var configuration = ConfigurationManager.AppSettings;
        var host = configuration[ConsulHostVarName];
        var port = configuration[ConsulPortVarName];
            
        return Create(host, port);
    }

    private static ConsulServiceAddress? Create(string? host, string? port)
    {
        if (string.IsNullOrWhiteSpace(host) == false && int.TryParse(port, out var portNumber))
        {
            return new ConsulServiceAddress(host, portNumber);
        }

        return null;
    }
}