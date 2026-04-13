using System;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

public interface IPublisher<in T> where T : class
{
    Task PublishAsync(T data, uint currentRetryCount = 0, TimeSpan? delay = null);
}