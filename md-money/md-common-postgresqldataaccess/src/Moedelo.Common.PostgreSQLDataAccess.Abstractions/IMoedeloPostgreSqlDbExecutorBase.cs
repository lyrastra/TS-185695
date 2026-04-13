#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.PostgreSqlDataAccess.Abstractions;

public interface IMoedeloPostgreSqlDbExecutorBase
{
    Task<IReadOnlyList<TR>> QueryAsync<TR>(
        IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<HashSet<TR>> HashSetQueryAsync<TR>(
        IQueryObject queryObject,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> FirstOrDefaultAsync<TR>(
        IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<int> ExecuteAsync(
        IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<IReadOnlyList<TR>> QueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<HashSet<TR>> HashSetQueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task QueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        Action<TR[], IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> FirstOrDefaultAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task FirstOrDefaultAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        Action<TR, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<int> ExecuteAsync(
        IQueryObjectWithDynamicParams queryObject,
        Action<IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task BulkCopyAsync(
        IBulkCopyQueryObject queryObject,
        CancellationToken cancellationToken = default(CancellationToken),
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
}
