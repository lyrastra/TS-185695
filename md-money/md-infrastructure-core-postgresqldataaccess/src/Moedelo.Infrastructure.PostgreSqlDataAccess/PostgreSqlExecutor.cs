using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Internals;
using Npgsql;
using NpgsqlTypes;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess;

[InjectAsSingleton(typeof(IPostgreSqlExecutor))]
internal sealed class PostgreSqlExecutor: IPostgreSqlExecutor
{
    private static readonly QuerySetting DefaultQuerySettings = new QuerySetting();

    public PostgreSqlExecutor(IOptions<MoedeloPostgresqlSqlExecutorOptions> optionsObject)
    {
        var options = optionsObject.Value;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", options.EnableLegacyTimestampBehavior);

        if (options.ReplaceUnspecifiedKindOfDateTimeByLocal)
        {
            SqlMapper.AddTypeHandler(new DateTimeUnspecifiedKindFixingHandler());
        }

        SqlMapper.AddTypeHandler(new PassThroughHandler<IPAddress>(NpgsqlDbType.Inet));
        SqlMapper.AddTypeHandler(new PassThroughHandler<PhysicalAddress>(NpgsqlDbType.MacAddr));
    }

    public async Task<IReadOnlyList<TR>> QueryAsync<TR>(string connectionString,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            return result as IReadOnlyList<TR> ?? result.ToArray();
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<HashSet<TR>> HashSetQueryAsync<TR>(string connectionString,
        IQueryObject queryObject,
        QuerySetting settings = null,
        IEqualityComparer<TR> equalityComparer = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));
            
        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            return new HashSet<TR>(result, equalityComparer);
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<TR> FirstOrDefaultAsync<TR>(string connectionString,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            return await cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition).ConfigureAwait(false);
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<int> ExecuteAsync(string connectionString,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            return await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<IReadOnlyList<TR>> QueryAsync<TR>(string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();
        var dynamicParameters = GetParameters(queryObject);

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            return result as IReadOnlyList<TR> ?? result.ToArray();
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<HashSet<TR>> HashSetQueryAsync<TR>(string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null,
        IEqualityComparer<TR> equalityComparer = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();
        var dynamicParameters = GetParameters(queryObject);

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            return new HashSet<TR>(result, equalityComparer);
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task QueryAsync<TR>(string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<TR[], IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();
        var dynamicParameters = GetParameters(queryObject);

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);

            readAction(result.ToArray(), new OutParameterReader(dynamicParameters));
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<TR> FirstOrDefaultAsync<TR>(string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();
        var dynamicParameters = GetParameters(queryObject);

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            return await cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition).ConfigureAwait(false);
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task FirstOrDefaultAsync<TR>(string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<TR, IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();
        var dynamicParameters = GetParameters(queryObject);

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition).ConfigureAwait(false);

            readAction(result, new OutParameterReader(dynamicParameters));
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }

    public async Task<int> ExecuteAsync(string connectionString,
        IQueryObjectWithDynamicParams queryObject,
        Action<IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= DefaultQuerySettings;
        IReadOnlyCollection<TemporaryTable> createdTemporaryTables = Array.Empty<TemporaryTable>();
        var dynamicParameters = GetParameters(queryObject);

        await using var cnn = new NpgsqlConnection(connectionString);
        try
        {
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            createdTemporaryTables = await CreateTempTablesAsync(cnn, queryObject.TemporaryTables, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.Timeout,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);

            readAction(new OutParameterReader(dynamicParameters));

            return result;
        }
        finally
        {
            await DropTempTables(cnn, createdTemporaryTables).ConfigureAwait(false);
        }
    }
        
    public async Task BulkCopyAsync(string connectionString,
        IBulkCopyQueryObject queryObject,
        CancellationToken cancellationToken = default)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        await using var cnn = new NpgsqlConnection(connectionString);
        await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);

        var binaryImportSql = GetBinaryImportSql(queryObject.TableName, queryObject.TableHeaders);
        await using var writer = await cnn
            .BeginBinaryImportAsync(binaryImportSql, cancellationToken)
            .ConfigureAwait(false);

        var tableHeaders = queryObject.TableHeaders;
            
        foreach (var row in queryObject.TableRows)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);

            for (var i = 0; i < row.Count; i++)
            {
                await writer
                    .WriteAsync(row[i], tableHeaders[i].DbTypeName, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }
        
    private static async Task<IReadOnlyCollection<TemporaryTable>> CreateTempTablesAsync(
        NpgsqlConnection cnn, 
        IReadOnlyCollection<TemporaryTable> tempTables,
        CancellationToken cancellationToken)
    {
        if (tempTables == null || tempTables.Count == 0)
        {
            return Array.Empty<TemporaryTable>();
        }

        foreach (var tempTable in tempTables)
        {
            await cnn.ExecuteAsync(tempTable.CreateSql).ConfigureAwait(false);
            var dataTable = tempTable.DataTable;

            var binaryImportSql = GetBinaryImportSql(dataTable.TableName, dataTable);

            await using var writer = await cnn
                .BeginBinaryImportAsync(binaryImportSql, cancellationToken)
                .ConfigureAwait(false);

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];
                await writer.WriteRowAsync(cancellationToken, row.ItemArray).ConfigureAwait(false);
            }

            await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
        }
        cancellationToken.ThrowIfCancellationRequested();

        return tempTables;
    }

    private static async Task DropTempTables(
        IDbConnection cnn,
        IReadOnlyCollection<TemporaryTable> tempTables)
    {
        if (tempTables == null || tempTables.Count == 0 || cnn.State != ConnectionState.Open)
        {
            return;
        }

        foreach (var tempTable in tempTables)
        {
            await cnn.ExecuteAsync(tempTable.DropSql).ConfigureAwait(false);
        }
    }

    private static DynamicParameters GetParameters(IQueryObjectWithDynamicParams queryObject)
    {
        var dynamicParameters = new DynamicParameters();

        foreach (var param in queryObject.DynamicParams)
        {
            dynamicParameters.Add(
                param.Name,
                param.Value,
                param.DbType,
                param.Direction,
                param.Size,
                param.Precision,
                param.Scale);
        }

        return dynamicParameters;
    }

    private static string GetBinaryImportSql(string tableName, IEnumerable<DbTypedColumnInfo> dataTable)
        => $"copy {tableName} ({string.Join(",", dataTable.Select(column => column.ColumnName))}) FROM stdin BINARY";

    private static IEnumerable<string> EnumerateColumnNames(DataTable dataTable)
    {
        for (var i = 0; i < dataTable.Columns.Count; i++)
        {
            yield return dataTable.Columns[i].ColumnName;
        }
    }

    private static string GetBinaryImportSql(string tableName, DataTable dataTable)
    {
        var columnNames = EnumerateColumnNames(dataTable);

        return $"copy {tableName} ({string.Join(",", columnNames)}) FROM stdin BINARY";
    }
}