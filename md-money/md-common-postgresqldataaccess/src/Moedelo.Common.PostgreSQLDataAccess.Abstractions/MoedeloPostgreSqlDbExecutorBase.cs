#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.PostgreSqlDataAccess.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.PostgreSqlDataAccess.Abstractions;

/// <summary>
/// Базовый класс для работы с postgresql базами данных.
/// Предоставляет набор методов для работы с данными.
/// Обеспечивает мониторинг операций в системе auditTrail.
/// </summary>
public abstract class MoedeloPostgreSqlDbExecutorBase : IMoedeloPostgreSqlDbExecutorBase
{
    private static readonly QuerySetting DefaultQuerySetting = new QuerySetting(null, 30);

    private readonly IPostgreSqlExecutor sqlDbExecutor;
    private readonly SettingValue connectionString;
    private readonly IAuditTracer auditTracer;

    protected MoedeloPostgreSqlDbExecutorBase(
        IPostgreSqlExecutor sqlDbExecutor,
        SettingValue connectionString,
        IAuditTracer auditTracer)
    {
        this.sqlDbExecutor = sqlDbExecutor;
        this.connectionString = connectionString;
        this.auditTracer = auditTracer;
    }

    public async Task<IReadOnlyList<TR>> QueryAsync<TR>(IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .QueryAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<HashSet<TR>> HashSetQueryAsync<TR>(IQueryObject queryObject,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .HashSetQueryAsync(cnn, queryObject, settings ?? DefaultQuerySetting, equalityComparer,
                    cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<TR> FirstOrDefaultAsync<TR>(IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .FirstOrDefaultAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<int> ExecuteAsync(IQueryObject queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .ExecuteAsync(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<IReadOnlyList<TR>> QueryAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .QueryAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<HashSet<TR>> HashSetQueryAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .HashSetQueryAsync(cnn, queryObject, settings ?? DefaultQuerySetting, equalityComparer,
                    cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task QueryAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        Action<TR[], IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await sqlDbExecutor
                .QueryAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<TR> FirstOrDefaultAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .FirstOrDefaultAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task FirstOrDefaultAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        Action<TR, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await sqlDbExecutor
                .FirstOrDefaultAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<int> ExecuteAsync(IQueryObjectWithDynamicParams queryObject,
        Action<IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            var result = await sqlDbExecutor
                .ExecuteAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task BulkCopyAsync(IBulkCopyQueryObject queryObject,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber,
                queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await sqlDbExecutor
                .BulkCopyAsync(connectionString.Value, queryObject, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    private IAuditSpanBuilder GetAuditSpanBuilder(string memberName, string sourceFilePath, int sourceLineNumber,
        IAuditTrailSpanNameSource? queryObject)
    {
        var predefinedSpanName = queryObject?.AuditTrailSpanName;
        var spanName = predefinedSpanName ?? $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
        var spanBuilder = auditTracer.BuildSpan(AuditSpanType.PostgreSQLDbQuery, spanName);

        if (Transaction.Current != null)
        {
            spanBuilder.WithTag("AmbientTransaction", Transaction.Current.TransactionInformation);
        }

        return spanBuilder;
    }
}
