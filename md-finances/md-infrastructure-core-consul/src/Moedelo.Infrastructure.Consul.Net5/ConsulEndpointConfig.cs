using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.Consul.Net5
{
    [InjectAsSingleton(typeof(IConsulEndpointConfig))]
    internal sealed class ConsulEndpointConfig : IConsulEndpointConfig
    {
#if DEBUG
        private const string DefaultConsulHost = "localhost";
#else
        private const string DefaultConsulHost = "org.moedelo.consul";
#endif

        private const int DefaultConsulPort = 8500;

        private const string CONSUL_HOST = "CONSUL_HOST";

        private const string CONSUL_PORT = "CONSUL_PORT";

        private const string AppSettingsJsonFile = "appsettings.json";

        /// <summary>
        /// получение из разных источников, причем если в источнике мы получили оба парметра
        /// то результат готов и к следующему не переходим.
        /// Источники по приоритету:
        /// 1 чтение из переменных окружения (CONSUL_HOST, CONSUL_PORT)
        /// 2 чтение из конфига приложения
        /// 3 значения по умолчанию
        /// </summary>
        public ConsulServiceAddress GetConfig()
        {
            return TryEnvironment()
                   ?? TryConfig()
                   ?? new ConsulServiceAddress(DefaultConsulHost, DefaultConsulPort);
        }

        private static ConsulServiceAddress TryEnvironment()
        {
            var host = Environment.GetEnvironmentVariable(CONSUL_HOST, EnvironmentVariableTarget.Process) ??
                       Environment.GetEnvironmentVariable(CONSUL_HOST, EnvironmentVariableTarget.Machine);
            var port = Environment.GetEnvironmentVariable(CONSUL_PORT, EnvironmentVariableTarget.Process) ??
                       Environment.GetEnvironmentVariable(CONSUL_PORT, EnvironmentVariableTarget.Machine);

            return Create(host, port);
        }

        private static ConsulServiceAddress TryConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsJsonFile);

            var configuration = builder.Build();

            var host = configuration[CONSUL_HOST];
            var port = configuration[CONSUL_PORT];

            return Create(host, port);
        }

        private static ConsulServiceAddress Create(string host, string port)
        {
            if (!string.IsNullOrWhiteSpace(host) && int.TryParse(port, out var p))
            {
                return new ConsulServiceAddress(host, p);
            }

            return null;
        }
    }
}