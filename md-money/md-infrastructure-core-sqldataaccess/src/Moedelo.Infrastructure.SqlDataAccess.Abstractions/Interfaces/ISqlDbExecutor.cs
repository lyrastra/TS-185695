using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;

public interface ISqlDbExecutor
{
    Task<IReadOnlyList<TR>> QueryAsync<TR>(
        string connectionString,
        IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<HashSet<TR>> HashSetQueryAsync<TR>(
        string connectionString,
        IQueryObject queryObject,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default);

    Task<TR> FirstOrDefaultAsync<TR>(
        string connectionString,
        IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<int> ExecuteAsync(
        string connectionString,
        IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<TR> QueryMultipleAsync<TR>(
        string connectionString,
        IQueryObject queryObject,
        Func<IGridReader, Task<TR>> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TR>> QueryAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<HashSet<TR>> HashSetQueryAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default);

    Task QueryAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<IReadOnlyList<TR>, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);
        
    Task HashSetQueryAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<HashSet<TR>, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default);

    Task<TR> FirstOrDefaultAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task FirstOrDefaultAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<TR, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<int> ExecuteAsync(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task<TR> QueryMultipleAsync<TR>(
        string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Func<IGridReader, Task<TR>> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default);

    Task BulkCopyAsync(
        string connectionString,
        IBulkCopyQueryObject queryObject,
        BulkCopySetting? settings = null,
        CancellationToken cancellationToken = default);
}
