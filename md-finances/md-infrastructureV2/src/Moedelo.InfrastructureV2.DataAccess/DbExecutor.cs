using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Data.SqlClient;
using SqlBulkCopyColumnMapping = Microsoft.Data.SqlClient.SqlBulkCopyColumnMapping;
using SqlBulkCopyOptions = Microsoft.Data.SqlClient.SqlBulkCopyOptions;
using Dapper;
using Moedelo.InfrastructureV2.DataAccess.Extensions;
using Moedelo.InfrastructureV2.DataAccess.Internals;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.DataAccess;

public abstract class DbExecutor : IDbExecutor
{
    private static readonly QuerySetting EmptyQuerySettings = new();
    private static readonly BulkCopySetting EmptyBulkCopySetting = new();

    private readonly SettingValue connectionStringSetting;
    private readonly IAuditTracer auditTracer;

    static DbExecutor()
    {
        // По умолчанию Dapper мапит C# DateTime в DbType.DateTime (DateTime.MinValue при этом сохранить нельзя)
        SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
    }

    protected DbExecutor(
        SettingValue connectionStringSetting,
        IAuditTracer auditTracer)
    {
        this.connectionStringSetting = connectionStringSetting;
        this.auditTracer = auditTracer;
    }

    public Task<List<TR>> QueryAsync<TR>(
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInsideAuditTrailAsync(
            memberName, sourceFilePath, sourceLineNumber,
            queryObject, settings, cancellationToken,
            static async (cnn, commandDefinition) =>
            {
                var queryResult = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

                return queryResult.ToList();
            });
    }

    public Task<TR> SingleAsync<TR>(
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInsideAuditTrailAsync(
            memberName, sourceFilePath, sourceLineNumber,
            queryObject, settings, cancellationToken,
            static (cnn, commandDefinition) => cnn.QuerySingleAsync<TR>(commandDefinition));
    }

    public Task<TR> SingleOrDefaultAsync<TR>(
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInsideAuditTrailAsync(
            memberName, sourceFilePath, sourceLineNumber,
            queryObject, settings, cancellationToken,
            static (cnn, commandDefinition) => cnn.QuerySingleOrDefaultAsync<TR>(commandDefinition));
    }

    public async Task<List<TR>> QueryAsync<TR>(
        IList<QueryObject> queryObjects,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObjects)
            .Start();
        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation)
                .ConfigureAwait(false);

            var count = queryObjects.Count;

            for (var i = 0; i < count - 1; i++)
            {
                var queryObject = queryObjects[i];
                var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

                await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
            }

            var resultQueryObject = queryObjects[count - 1];
            var resultCommandDefinition = resultQueryObject.ToDapperCommandDefinition(settings, cancellation);

            var queryResult = await cnn.QueryAsync<TR>(resultCommandDefinition).ConfigureAwait(false);

            return queryResult.ToList();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<List<TR>> QueryDistinctAsync<TR>(
        IQueryObject queryObject,
        Type[] types,
        Func<object[], TR> map,
        string splitOn = "Id",
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var queryParams = queryObject.GetDapperCommandDefinitionParameters();

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync(
                queryObject.Sql,
                types,
                map,
                queryParams,
                settings.Transaction,
                commandTimeout: settings.Timeout,
                commandType: queryObject.CommandType,
                splitOn: splitOn).ConfigureAwait(false);

            return queryResult.Distinct().ToList();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<List<TR>> QueryNoDistinctAsync<TR>(
        IQueryObject queryObject,
        Type[] types,
        Func<object[], TR> map,
        string splitOn = "Id",
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var queryParams = queryObject.GetDapperCommandDefinitionParameters();

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync(
                queryObject.Sql,
                types,
                map,
                queryParams,
                settings.Transaction,
                commandTimeout: settings.Timeout,
                commandType: queryObject.CommandType,
                splitOn: splitOn).ConfigureAwait(false);

            return queryResult.ToList();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<List<TR>> QueryAsync<TR, TT>(
        string tempTableName,
        List<TT> tempTableData,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TT : class
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            await using var tempTable = await cnn
                .CreateTemporaryTableAsync(tempTableName, tempTableData, settings, cancellation)
                .ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            return queryResult.ToList();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<List<TR>> QueryAsync<TR>(
        Dictionary<string, IList> temporaryDataDict,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (!temporaryDataDict.Any() || temporaryDataDict.Any(dataEntry => dataEntry.Value == null))
        {
            throw new ArgumentOutOfRangeException(nameof(temporaryDataDict));
        }

        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            await using var tempTables = await cnn
                .CreateTemporaryTablesAsync(temporaryDataDict, settings, cancellation)
                .ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            return queryResult.ToList();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public Task<List<TR>> QueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInsideAuditTrailAsync(
            memberName, sourceFilePath, sourceLineNumber,
            queryObject, settings, cancellationToken,
            static async (cnn, commandDefinitionWithParams) =>
            {
                var (commandDefinition, _) = commandDefinitionWithParams;
                var queryResult = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

                return queryResult.ToList();
            });
    }

    public async Task ExecuteAsync(
        IDictionary<string, IList> temporaryData,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (temporaryData.Any(dataEntry => dataEntry.Value == null))
        {
            throw new ArgumentOutOfRangeException(nameof(temporaryData));
        }

        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            await using var tempTables = await cnn
                .CreateTemporaryTablesAsync(temporaryData, settings, cancellation)
                .ConfigureAwait(false);

            await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task ExecuteAsync<TT>(
        string tempTableName,
        IEnumerable<TT> tempTableData,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TT : class
    {
        if (string.IsNullOrWhiteSpace(tempTableName) || tempTableData == null)
        {
            throw new ArgumentNullException();
        }

        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            await using var tempTable = await cnn
                .CreateTemporaryTableAsync(tempTableName, tempTableData, settings, cancellation)
                .ConfigureAwait(false);

            await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<TDomainResult> QueryAsync<TDbRow, TDomainResult>(
        IQueryObjectWithDynamicParams queryObject,
        Func<IEnumerable<TDbRow>, IOutParameterReader, TDomainResult> readAction,
        QuerySetting settings,
        CancellationToken cancellationToken,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var connectionString = connectionStringSetting.Value;
        var (commandDefinition, dynamicParams) = queryObject
            .ToDapperCommandDefinitionWithDynamicParameters(settings, cancellationToken);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellationToken)
                .ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync<TDbRow>(commandDefinition).ConfigureAwait(false);

            return readAction(queryResult, new OutParameterReader(dynamicParams));
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public Task<TR> FirstOrDefaultAsync<TR>(
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInsideAuditTrailAsync(
            memberName, sourceFilePath, sourceLineNumber,
            queryObject, settings, cancellationToken,
            static (cnn, commandDefinition) => cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition));
    }

    public async Task<TR> FirstOrDefaultDistinctAsync<TR>(
        IQueryObject queryObject,
        Type[] types,
        Func<object[], TR> map,
        string splitOn = "Id",
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var queryParams = queryObject.GetDapperCommandDefinitionParameters();

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync(
                queryObject.Sql,
                types,
                map,
                queryParams,
                settings.Transaction,
                commandTimeout: settings.Timeout,
                commandType: queryObject.CommandType,
                splitOn: splitOn).ConfigureAwait(false);

            return queryResult.Distinct().FirstOrDefault();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<TR> FirstOrDefaultNoDistinctAsync<TR>(
        IQueryObject queryObject,
        Type[] types,
        Func<object[], TR> map,
        string splitOn = "Id",
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var queryParams = queryObject.GetDapperCommandDefinitionParameters();

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync(
                queryObject.Sql,
                types,
                map,
                queryParams,
                settings.Transaction,
                commandTimeout: settings.Timeout,
                commandType: queryObject.CommandType,
                splitOn: splitOn).ConfigureAwait(false);

            return queryResult.FirstOrDefault();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<TR> FirstOrDefaultAsync<TR>(
        IList<QueryObject> queryObjects,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObjects)
            .Start();
        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var count = queryObjects.Count;

            for (var i = 0; i < count - 1; ++i)
            {
                var commandDefinition = queryObjects[i].ToDapperCommandDefinition(settings, cancellation);

                await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
            }

            var resultQueryObject = queryObjects[count - 1];
            var resultCommandDefinition = resultQueryObject.ToDapperCommandDefinition(settings, cancellation);

            return await cnn.QueryFirstAsync<TR>(resultCommandDefinition).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task QueryMultipleAsync(
        IQueryObject queryObject,
        Func<IGridReader, Task> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation)
                .ConfigureAwait(false);

            using var gridReader = await cnn.QueryMultipleAsync(commandDefinition).ConfigureAwait(false);

            await readAction(new GridReaderWrapper(gridReader)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<T> QueryMultipleAsync<T>(
        IQueryObject queryObject,
        Func<IGridReader, Task<T>> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation)
                .ConfigureAwait(false);

            using var gridReader = await cnn.QueryMultipleAsync(commandDefinition).ConfigureAwait(false);

            return await readAction(new GridReaderWrapper(gridReader)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task QueryMultipleAsync(
        IQueryObjectWithDynamicParams queryObject,
        Func<IGridReader, Task> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            using var gridReader = await cnn.QueryMultipleAsync(commandDefinition).ConfigureAwait(false);
 
            await readAction(new GridReaderWrapper(gridReader)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task QueryMultipleAsync(
        IQueryObjectWithDynamicParams queryObject,
        Func<IGridReader, IOutParameterReader, Task> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var (commandDefinition, dynamicParams) = queryObject
            .ToDapperCommandDefinitionWithDynamicParameters(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            using var gridReader = await cnn.QueryMultipleAsync(commandDefinition).ConfigureAwait(false);
            var wrapper = new GridReaderWrapper(gridReader);
            await readAction(wrapper, new OutParameterReader(dynamicParams)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task QueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject,
        Action<IEnumerable<TR>, IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var (commandDefinition, dynamicParams) = queryObject
            .ToDapperCommandDefinitionWithDynamicParameters(settings, cancellation);

        using var scope = StartAuditTrailScope(memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var queryResult = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            readAction(queryResult, new OutParameterReader(dynamicParams));
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public Task<int> ExecuteAsync(
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInsideAuditTrailAsync(
            memberName, sourceFilePath, sourceLineNumber,
            queryObject, settings, cancellationToken,
            static (cnn, commandDefinition) => cnn.ExecuteAsync(commandDefinition));
    }

    public async Task<int> ExecuteAsync(
        IList<QueryObject> queryObjects,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;

        var connectionString = connectionStringSetting.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObjects)
            .Start();
        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            var count = queryObjects.Count;

            for (var i = 0; i < count - 1; i++)
            {
                var queryObject = queryObjects[i];
                var commandDefinition = queryObject.ToDapperCommandDefinition(settings, cancellation);

                await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
            }

            var resultQueryObject = queryObjects[count - 1];
            var resultCommandDefinition = resultQueryObject.ToDapperCommandDefinition(settings, cancellation);

            return await cnn.ExecuteAsync(resultCommandDefinition).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task ExecuteAsync(
        IQueryObjectWithDynamicParams queryObject,
        Action<IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var (commandDefinition, dynamicParams) = queryObject
            .ToDapperCommandDefinitionWithDynamicParameters(settings, cancellation);

        using var scope = GetAuditSpanBuilder(queryObject, memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);
            await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);

            readAction(new OutParameterReader(dynamicParams));
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task BulkCopyAsync(
        string table,
        DataTable data,
        BulkCopySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (string.IsNullOrWhiteSpace(table) || data == null)
        {
            throw new ArgumentNullException();
        }

        settings ??= EmptyBulkCopySetting;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithTag("DataTable.TableName", data.TableName)
            .WithTag("DataTable.Rows.Count", data.Rows.Count)
            .Start();
        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation)
                .ConfigureAwait(false);

            using var sqlBulkCopy = new SqlBulkCopy(cnn, (SqlBulkCopyOptions)settings.CopyOptions, externalTransaction: null);
            sqlBulkCopy.DestinationTableName = table;
            sqlBulkCopy.BulkCopyTimeout = settings.Timeout ?? 30;

            if (settings.Mappings != null && settings.Mappings.Any())
            {
                foreach (var mapping in settings.Mappings)
                {
                    sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(mapping.SourceColumn, mapping.DestinationColumn));
                }
            }

            await sqlBulkCopy.WriteToServerAsync(data, cancellation).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task QueryAsync<TR>(
        Dictionary<string, IList> temporaryDataDict,
        IQueryObjectWithDynamicParams queryObject,
        Action<IEnumerable<TR>, IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (!temporaryDataDict.Any() || temporaryDataDict.Any(dataEntry => dataEntry.Value == null))
        {
            throw new ArgumentOutOfRangeException(nameof(temporaryDataDict));
        }

        settings ??= EmptyQuerySettings;
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var (commandDefinition, dynamicParams) = queryObject
            .ToDapperCommandDefinitionWithDynamicParameters(settings, cancellation);

        using var scope = GetAuditSpanBuilder(queryObject, memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation).ConfigureAwait(false);

            await using var tempTables = await cnn
                .CreateTemporaryTablesAsync(temporaryDataDict, settings, cancellation)
                .ConfigureAwait(false);

            var queryResult = await cnn
                .QueryAsync<TR>(commandDefinition)
                .ConfigureAwait(false);

            readAction(queryResult, new OutParameterReader(dynamicParams));
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task<SqlConnection> OpenConnectionAsync(string connectionString,
        CancellationToken cancellationToken)
    {
        var cnn = new SqlConnection(connectionString);
        await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);

        return cnn;
    }

    private string GetAuditSpanName(string memberName, string sourceFilePath, int sourceLineNumber)
    {
        var fileName = sourceFilePath.GetSourceFileName();

        return fileName != null
            ? $"{fileName}@{sourceLineNumber}.{memberName}"
            : $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
    }

    private string GetAuditSpanName(IQueryObjectBase queryObject, string memberName, string sourceFilePath,
        int sourceLineNumber)
    {
        return queryObject.AuditTrailSpanName ?? GetAuditSpanName(memberName, sourceFilePath, sourceLineNumber);
    }

    private IAuditSpanBuilder GetAuditSpanBuilder(string memberName, string sourceFilePath, int sourceLineNumber,
        string connectionString)
    {
        var spanName = GetAuditSpanName(memberName, sourceFilePath, sourceLineNumber);
        
        return GetAuditSpanBuilder(spanName, memberName, sourceFilePath, sourceLineNumber, connectionString);
    }

    private IAuditSpanBuilder GetAuditSpanBuilder(IQueryObjectBase queryObject,
        string memberName, string sourceFilePath, int sourceLineNumber,
        string connectionString)
    {
        var spanName = GetAuditSpanName(queryObject, memberName, sourceFilePath, sourceLineNumber);
        
        return GetAuditSpanBuilder(spanName, memberName, sourceFilePath, sourceLineNumber, connectionString);
    }

    private IAuditSpanBuilder GetAuditSpanBuilder(string spanName,
        string memberName, string sourceFilePath, int sourceLineNumber,
        string connectionString)
    {
        return auditTracer
            .BuildSpan(AuditSpanType.MsSqlDbQuery, spanName)
            .WithStartDateUtc(DateTimeOffset.UtcNow)
            .TagCodeSourcePath(memberName, sourceFilePath, sourceLineNumber)
            .WithConnectionString(connectionString);
    }

    private IAuditScope StartAuditTrailScope(
        string memberName,
        string sourceFilePath,
        int sourceLineNumber,
        string connectionString,
        IQueryObject queryObject)
    {
        return GetAuditSpanBuilder(queryObject, memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObject)
            .Start();
    }

    private IAuditScope StartAuditTrailScope(
        string memberName,
        string sourceFilePath,
        int sourceLineNumber,
        string connectionString,
        IQueryObjectWithDynamicParams queryObject)
    {
        return GetAuditSpanBuilder(queryObject, memberName, sourceFilePath, sourceLineNumber, connectionString)
            .WithQueryObject(queryObject)
            .Start();
    }

    private async Task<TResult> RunInsideAuditTrailAsync<TResult>(
        string memberName,
        string sourceFilePath,
        int sourceLineNumber,
        IQueryObject queryObject,
        QuerySetting querySettings,
        CancellationToken? cancellationToken,
        Func<SqlConnection, CommandDefinition, Task<TResult>> action)
    {
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinition(
            querySettings ?? EmptyQuerySettings,
            cancellation);

        using var scope = StartAuditTrailScope(
            memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation)
                .ConfigureAwait(false);

            return await action(cnn, commandDefinition).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    private async Task<TResult> RunInsideAuditTrailAsync<TResult>(
        string memberName,
        string sourceFilePath,
        int sourceLineNumber,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting querySettings,
        CancellationToken? cancellationToken,
        Func<SqlConnection, CommandDefinitionDynamicParameters, Task<TResult>> action)
    {
        var cancellation = cancellationToken ?? default;
        var connectionString = connectionStringSetting.Value;
        var commandDefinition = queryObject.ToDapperCommandDefinitionWithDynamicParameters(
            querySettings ?? EmptyQuerySettings,
            cancellation);

        using var scope = StartAuditTrailScope(
            memberName, sourceFilePath, sourceLineNumber,
            connectionString,
            queryObject);

        try
        {
            using var cnn = await OpenConnectionAsync(connectionString, cancellation)
                .ConfigureAwait(false);

            return await action(cnn, commandDefinition).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }
}
