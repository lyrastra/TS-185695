using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;

public interface IPostgreSqlExecutor
{
    Task<IReadOnlyList<TR>> QueryAsync<TR>(
        string connectionString, 
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<HashSet<TR>> HashSetQueryAsync<TR>(
        string connectionString, 
        IQueryObject queryObject, 
        QuerySetting settings = null,
        IEqualityComparer<TR> equalityComparer = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<TR> FirstOrDefaultAsync<TR>(
        string connectionString, 
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<int> ExecuteAsync(
        string connectionString, 
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<IReadOnlyList<TR>> QueryAsync<TR>(
        string connectionString, 
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<HashSet<TR>> HashSetQueryAsync<TR>(
        string connectionString, 
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null, 
        IEqualityComparer<TR> equalityComparer = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task QueryAsync<TR>(
        string connectionString, 
        IQueryObjectWithDynamicParams queryObject, 
        Action<TR[], IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<TR> FirstOrDefaultAsync<TR>(
        string connectionString, 
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task FirstOrDefaultAsync<TR>(
        string connectionString, 
        IQueryObjectWithDynamicParams queryObject, 
        Action<TR, IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<int> ExecuteAsync(
        string connectionString, 
        IQueryObjectWithDynamicParams queryObject, 
        Action<IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default(CancellationToken));

    Task BulkCopyAsync(
        string connectionString,
        IBulkCopyQueryObject queryObject,
        CancellationToken cancellationToken = default(CancellationToken));
}