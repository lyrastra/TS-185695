using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations;

[InjectAsSingleton(typeof(IProducerPoolConfigCheck), typeof(IWebAppConfigCheck))]
internal sealed class ProducerPoolConfigCheck : IProducerPoolConfigCheck
{
    private const string Tag = nameof(ProducerPoolConfigCheck);

    private readonly object checkingLock = new();
    private bool isChecked;
    private readonly ILogger logger;
    private readonly IKafkaProducer producer;
    private readonly IMoedeloApacheKafkaConfig config;

    public ProducerPoolConfigCheck(IKafkaProducer producer,
        IMoedeloApacheKafkaConfig config,
        ILogger logger)
    {
        this.producer = producer;
        this.config = config;
        this.logger = logger;
    }

    /// <summary>
    /// Удостовериться, что внутренний пул продюсеров может функционировать должным образом
    /// </summary>
    public void Check()
    {
        if (isChecked) return;
        
        lock (checkingLock)
        {
            if (isChecked) return;

            producer.EnsureRawProducerPoolIsHealthy(config.BrokerEndpoints);
            logger.Info(Tag, "Пул продюсеров Apache Kafka готов к работе");

            // если не было исключений - всё хорошо
            isChecked = true;
        }
    }
}
