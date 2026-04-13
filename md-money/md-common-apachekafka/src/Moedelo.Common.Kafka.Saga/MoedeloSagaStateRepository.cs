using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Saga.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.Kafka.Saga
{
    [InjectAsSingleton(typeof(IMoedeloSagaStateRepository))]
    internal sealed class MoedeloSagaStateRepository : IMoedeloSagaStateRepository
    {
        private readonly ISqlScriptReader scriptReader;
        private readonly MoedeloSagaDbExecutor dbExecutor;

        public MoedeloSagaStateRepository(ISqlScriptReader scriptReader, MoedeloSagaDbExecutor dbExecutor)
        {
            this.scriptReader = scriptReader;
            this.dbExecutor = dbExecutor;
        }

        public async Task<IMoedeloSagaCurrentStateData> GetAsync(Guid sagaId)
        {
            var queryObject = new QueryObject(
                scriptReader.Get(this, Scripts.Get),
                new {sagaId});
            var result = await dbExecutor.FirstOrDefaultAsync<MoedeloSagaCurrentStateData>(queryObject)
                .ConfigureAwait(false);

            return result;
        }

        public Task SaveNewAsync(MoedeloSagaInitStateData initStateData)
        {
            var queryObject = new QueryObject(
                scriptReader.Get(this, Scripts.Insert),
                initStateData);

            return dbExecutor.ExecuteAsync(queryObject);
        }

        public Task UpdateAsync(MoedeloSagaNewStateData initStateData)
        {
            var queryObject = new QueryObject(
                scriptReader.Get(this, Scripts.Update),
                initStateData);

            return dbExecutor.ExecuteAsync(queryObject);
        }

        private class MoedeloSagaCurrentStateData : IMoedeloSagaCurrentStateData
        {
            public Guid SagaId { get; set; }
            public string StateType { get; set; }
            public string StateData { get; set; }
            public string ExecutionContextToken { get; set; }
        }
    }
}