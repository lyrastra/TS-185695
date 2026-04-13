using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess;

[InjectAsSingleton(typeof(MoedeloPostgresqlSqlExecutorOptions))]
public sealed class MoedeloPostgresqlSqlExecutorOptions
{
    private static int guid = 1; 
    private readonly int uid = guid++;

    public bool EnableLegacyTimestampBehavior { get; set; } = true;
    public bool ReplaceUnspecifiedKindOfDateTimeByLocal { get; set; } = false;
}
