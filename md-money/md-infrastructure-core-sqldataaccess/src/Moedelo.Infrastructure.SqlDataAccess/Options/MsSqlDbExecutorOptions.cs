using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.SqlDataAccess.Options;

[InjectAsSingleton(typeof(MsSqlDbExecutorOptions))]
public sealed class MsSqlDbExecutorOptions
{
    /// <summary>
    /// Заменять DateTimeKind.Unspecified на DateTimeKind.Local при получении данных из SQL Server
    /// </summary>
    public bool ReplaceUnspecifiedKindOfDateTimeByLocal { get; set; } = false;
}
