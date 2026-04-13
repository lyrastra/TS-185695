#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Common.SqlDataAccess.Abstractions.Models;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.SqlDataAccess.Abstractions;

public abstract class MoedeloSqlDbExecutorBase : IMoedeloSqlDbExecutorBase
{
    private static readonly QuerySetting DefaultQuerySetting = new QuerySetting(null, 30);

    private readonly ISqlDbExecutor sqlDbExecutor;
    private readonly SettingValue connectionString;
    private readonly IAuditTracer auditTracer;

    protected MoedeloSqlDbExecutorBase(
        ISqlDbExecutor sqlDbExecutor,
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .QueryAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .HashSetQueryAsync(cnn, queryObject, settings ?? DefaultQuerySetting, equalityComparer, cancellationToken)
                .ConfigureAwait(false);
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .FirstOrDefaultAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .ExecuteAsync(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }
        
    public async Task<TR> QueryMultipleAsync<TR>(IQueryObject queryObject,
        Func<IGridReader, Task<TR>> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .QueryMultipleAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .QueryAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .HashSetQueryAsync(cnn, queryObject, settings ?? DefaultQuerySetting, equalityComparer, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }
        
    public async Task QueryAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        Action<IReadOnlyList<TR>, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
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
        
    public async Task HashSetQueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        Action<HashSet<TR>, IOutParameterReader> readAction,
        QuerySetting? settings = null,
        IEqualityComparer<TR>? equalityComparer = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await sqlDbExecutor
                .HashSetQueryAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, equalityComparer, cancellationToken)
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .FirstOrDefaultAsync<TR>(cnn, queryObject, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
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

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
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
        
    public async Task<int> ExecuteAsync(
        IQueryObjectWithDynamicParams queryObject,
        Action<IOutParameterReader> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .ExecuteAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }
        
    public async Task<TR> QueryMultipleAsync<TR>(IQueryObjectWithDynamicParams queryObject,
        Func<IGridReader, Task<TR>> readAction,
        QuerySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            return await sqlDbExecutor
                .QueryMultipleAsync(cnn, queryObject, readAction, settings ?? DefaultQuerySetting, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }
        
    public async Task BulkCopyAsync(IBulkCopyQueryObject queryObject,
        BulkCopySetting? settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = connectionString.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, queryObject as IAuditTrailSpanNameSource)
            .WithConnectionString(cnn)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await sqlDbExecutor
                .BulkCopyAsync(cnn, queryObject, settings, cancellationToken)
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
        var spanBuilder = auditTracer.BuildSpan(AuditSpanType.MsSqlDbQuery, spanName);

        if (Transaction.Current != null)
        {
            spanBuilder.WithTag("AmbientTransaction", Transaction.Current.TransactionInformation);
        }

        return spanBuilder;
    }
}