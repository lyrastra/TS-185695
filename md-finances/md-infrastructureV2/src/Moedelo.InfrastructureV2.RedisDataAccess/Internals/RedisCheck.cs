using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Internals;

public interface IRedisCheck: IWebAppConfigCheck;

[InjectAsSingleton(typeof(IWebAppConfigCheck), typeof(IRedisCheck))]
internal sealed class RedisCheck : IRedisCheck
{
    private const string Tag = nameof(RedisCheck);

    private readonly object checkingLock = new();
    private bool isChecked;
    private readonly ILogger logger;
    private readonly IAuthorizationRedisDbExecutor dbExecutor;

    public RedisCheck(IAuthorizationRedisDbExecutor dbExecutor,
        ILogger logger)
    {
        this.dbExecutor = dbExecutor;
        this.logger = logger;
    }

    public void Check()
    {
        if (isChecked) return;
        
        lock (checkingLock)
        {
            if (isChecked) return;

            dbExecutor.ExistsAsync("CheckRedisOnStart").Wait();
            logger.Info(Tag, "Redis готов к работе");

            // если не было исключений - всё хорошо
            isChecked = true;
        }
    }
}
