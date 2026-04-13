using System;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public interface IMoedeloSagaStateRepository
    {
        Task<IMoedeloSagaCurrentStateData> GetAsync(Guid sagaId);

        Task SaveNewAsync(MoedeloSagaInitStateData initStateData);
        
        Task UpdateAsync(MoedeloSagaNewStateData initStateData);
    }
}
