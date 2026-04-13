using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbExecutors;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Dao;

[InjectAsSingleton(typeof(IKafkaConsumerMessageMemoryRepository))]
[InjectAsSingleton(typeof(IMoedeloKafkaConsumerMessageMemoryPostgresRepository))]
internal sealed class MoedeloKafkaConsumerMessageMemoryPostgresRepository : IMoedeloKafkaConsumerMessageMemoryPostgresRepository
{
    private const int MaxInitialStateReadAttempts = 3;
    private const int QueryTimeoutInSeconds = 15;
    private static readonly QuerySetting DefaultQuerySetting = new (timeout: QueryTimeoutInSeconds);
    private static readonly TimeSpan ReQueryDelay = TimeSpan.FromSeconds(1);
    
    private readonly IMoedeloKafkaConsumerMessageMemoryDbExecutor dbExecutor;
    private readonly IDbModelMapper dbModelMapper;
    private readonly ISqlScriptReader scriptReader;
    private readonly ILogger logger;

    public MoedeloKafkaConsumerMessageMemoryPostgresRepository(
        IMoedeloKafkaConsumerMessageMemoryDbExecutor dbExecutor,
        ISqlScriptReader scriptReader,
        IDbModelMapper dbModelMapper,
        ILogger<MoedeloKafkaConsumerMessageMemoryPostgresRepository> logger)
    {
        this.dbExecutor = dbExecutor;
        this.scriptReader = scriptReader;
        this.dbModelMapper = dbModelMapper;
        this.logger = logger;
    }

    public Task SaveAsync(IPartitionConsumingReadOnlyState partitionConsumingState,
        CancellationToken cancellationToken)
    {
        var querySql = scriptReader.Get(this, "Internals.Dao.Sql.Save.sql");
        var dbModel = dbModelMapper.MapToDbModel(partitionConsumingState);
        var queryObject = new QueryObject(querySql, dbModel);

        logger.LogStateIsSaving(dbModel);

        return dbExecutor.ExecuteAsync(queryObject, cancellationToken: cancellationToken);
    }

    private static volatile int stateLoadingCount = 0;
    
    public async Task<IPartitionConsumingReadOnlyState> GetOrCreateAsync(string consumerGroupId,
        string topic,
        int partition,
        CancellationToken cancellationToken)
    {
        var querySql = scriptReader.Get(this, "Internals.Dao.Sql.Get.sql");
        var queryParams = new PartitionConsumingStateQuery(consumerGroupId, topic, partition);
        var queryObject = new QueryObject(querySql, queryParams);

        var warningThreshold = TimeSpan.FromSeconds(2);
        var leftAttempts = MaxInitialStateReadAttempts;

        var loadingGlobalCount = Interlocked.Increment(ref stateLoadingCount);

        while (leftAttempts-- > 0)
        {
            var stopWatch = Stopwatch.StartNew();

            try
            {
                var dbModel = await dbExecutor
                    .FirstOrDefaultAsync<PartitionConsumingDbModel?>(queryObject, DefaultQuerySetting,
                        cancellationToken)
                    .ConfigureAwait(false);
                stopWatch.Stop();

                var domainModel = dbModelMapper.MapToDomain(dbModel, queryParams);
                var tooSlow = stopWatch.Elapsed > warningThreshold;

                if (tooSlow)
                {
                    logger.LogStateIsLoadedButTooSlow(
                        domainModel,
                        MaxInitialStateReadAttempts - leftAttempts,
                        stopWatch.Elapsed,
                        loadingGlobalCount);
                }
                else
                {
                    logger.LogStateIsLoaded(
                        domainModel,
                        MaxInitialStateReadAttempts - leftAttempts,
                        stopWatch.Elapsed,
                        loadingGlobalCount);
                }

                return domainModel;
            }
            catch (Exception exception) when (leftAttempts > 0)
            {
                logger.LogStateLoadingFailedError(
                    exception,
                    consumerGroupId,
                    topic,
                    partition,
                    leftAttempts,
                    stopWatch.Elapsed);
            }

            await Task.Delay(ReQueryDelay, cancellationToken).ConfigureAwait(false);
        }

        // по идее, мы никогда не должны сюда попасть
        throw new Exception($"{topic}[{partition}] ошибка при попытке загрузки состояния для {consumerGroupId}");
    }
}
