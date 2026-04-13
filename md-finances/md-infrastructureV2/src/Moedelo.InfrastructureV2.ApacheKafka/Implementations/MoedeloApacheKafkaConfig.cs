using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations;

[InjectAsSingleton(typeof(IMoedeloApacheKafkaConfig))]
// ReSharper disable once UnusedType.Global
internal sealed class MoedeloApacheKafkaConfig : IMoedeloApacheKafkaConfig
{
    public MoedeloApacheKafkaConfig(ISettingRepository settingRepository)
        => this.brokerEndpoints = settingRepository
            .GetRequired("KafkaBrokerEndpoints");

    public string BrokerEndpoints => brokerEndpoints.Value;
    
    private readonly SettingValue brokerEndpoints;
}
