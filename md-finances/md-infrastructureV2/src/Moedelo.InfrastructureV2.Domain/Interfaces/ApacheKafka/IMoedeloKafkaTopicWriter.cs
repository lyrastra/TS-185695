using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

public interface IMoedeloKafkaTopicWriter
{
    Task<string> WriteAsync<T>(string topicName, string key, T value) where T : MoedeloKafkaMessageValueBase;
    /// <summary>
    /// Удостовериться, что внутренний пул продюсеров может функционировать должным образом
    /// </summary>
    [Obsolete("Используй IWebAppConfigChecker в PingController и эта проверка будет проведена автоматически")]
    void EnsureRawProducerPoolIsHealthy();
}
