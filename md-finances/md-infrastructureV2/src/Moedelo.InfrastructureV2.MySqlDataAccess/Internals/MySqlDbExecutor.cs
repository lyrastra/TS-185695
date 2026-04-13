using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.MySqlDataAccess.Abstractions;
using Moedelo.InfrastructureV2.MySqlDataAccess.Extensions;
using MySql.Data.MySqlClient;

namespace Moedelo.InfrastructureV2.MySqlDataAccess.Internals;

[InjectAsSingleton(typeof(IMySqlDbExecutor))]
internal sealed class MySqlDbExecutor : IMySqlDbExecutor
{
    private const int DefaultTimeout = 30;
    private static readonly QuerySetting EmptyQuerySettings = new QuerySetting(timeout: DefaultTimeout);
    private static readonly MySqlBulkLoaderSettings EmptyMySqlBulkLoaderSettings = new MySqlBulkLoaderSettings();

    private readonly IAuditTracer auditTracer;

    static MySqlDbExecutor()
    {
        SqlMapper.AddTypeHandler(new DateTimeHandler());
        SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
    }

    public MySqlDbExecutor(IAuditTracer auditTracer)
    {
        this.auditTracer = auditTracer;
    }

    public async Task<TR[]> QueryAsync<TR>(
        string connectionString,
        QueryObject queryObject,
        QuerySetting settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= EmptyQuerySettings;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnectionString(connectionString)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await using var cnn = new MySqlConnection(connectionString);
            await cnn.OpenAsync().ConfigureAwait(false);

            try
            {
                await CreateTempTables(cnn, queryObject.TemporaryTables, settings.Timeout ?? 30).ConfigureAwait(false);

                var result = await cnn.QueryAsync<TR>(
                    queryObject.Sql,
                    queryObject.QueryParams,
                    settings.Transaction,
                    settings.Timeout,
                    queryObject.CommandType
                ).ConfigureAwait(false);

                return result.ToArray();
            }
            finally
            {
                await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<TR> FirstOrDefaultAsync<TR>(
        string connectionString,
        QueryObject queryObject,
        QuerySetting settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= EmptyQuerySettings;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnectionString(connectionString)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await using var cnn = new MySqlConnection(connectionString);
            await cnn.OpenAsync().ConfigureAwait(false);

            try
            {
                await CreateTempTables(cnn, queryObject.TemporaryTables, settings.Timeout ?? 30).ConfigureAwait(false);

                var result = await cnn.QueryFirstOrDefaultAsync<TR>(
                    queryObject.Sql,
                    queryObject.QueryParams,
                    settings.Transaction,
                    settings.Timeout,
                    queryObject.CommandType
                ).ConfigureAwait(false);

                return result;
            }
            finally
            {
                await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<int> ExecuteAsync(
        string connectionString,
        QueryObject queryObject,
        QuerySetting settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= EmptyQuerySettings;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnectionString(connectionString)
            .WithQueryObject(queryObject)
            .Start();
        try
        {
            await using var cnn = new MySqlConnection(connectionString);
            await cnn.OpenAsync().ConfigureAwait(false);

            try
            {
                await CreateTempTables(cnn, queryObject.TemporaryTables, settings.Timeout ?? 30).ConfigureAwait(false);

                var result = await cnn.ExecuteAsync(
                    queryObject.Sql,
                    queryObject.QueryParams,
                    settings.Transaction,
                    settings.Timeout,
                    queryObject.CommandType
                ).ConfigureAwait(false);

                return result;
            }
            finally
            {
                await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task BulkCopyAsync<T>(
        string connectionString,
        MySqlBulkLoaderObject<T> queryObject,
        MySqlBulkLoaderSettings settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
        ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

        settings ??= EmptyMySqlBulkLoaderSettings;

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnectionString(connectionString)
            .Start();
        try
        {
            await using var cnn = new MySqlConnection(connectionString);
            await cnn.OpenAsync().ConfigureAwait(false);

            using var file = new MySqlBulkFile($"{Guid.NewGuid()}.txt");
            await file.WriteAsync(queryObject.Data).ConfigureAwait(false);

            file.Close();

            var bulkLoader = new MySqlBulkLoader(cnn)
            {
                FileName = file.Name,
                TableName = queryObject.Name,
                FieldTerminator = settings.FieldTerminator,
                LineTerminator = settings.LineTerminator,
            };

            if (settings.Timeout.HasValue)
            {
                bulkLoader.Timeout = settings.Timeout.Value;
            }

            await bulkLoader.LoadAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    private static async Task CreateTempTables(
        MySqlConnection cnn,
        IReadOnlyCollection<TemporaryTable> tempTables,
        int timeout)
    {
        if (tempTables == null || tempTables.Count == 0)
        {
            return;
        }

        foreach (var tempTable in tempTables)
        {
            await cnn.ExecuteAsync(tempTable.CreateSql).ConfigureAwait(false);

            using var file = new MySqlBulkFile($"{Guid.NewGuid()}.txt");
            await file.WriteAsync(tempTable.Data).ConfigureAwait(false);

            file.Close();

            var bulkLoader = new MySqlBulkLoader(cnn)
            {
                FileName = file.Name,
                TableName = tempTable.Name,
                FieldTerminator = EmptyMySqlBulkLoaderSettings.FieldTerminator,
                LineTerminator = EmptyMySqlBulkLoaderSettings.LineTerminator,
                Timeout = timeout
            };

            await bulkLoader.LoadAsync().ConfigureAwait(false);
        }
    }

    private static async Task DropTempTables(
        IDbConnection cnn,
        IReadOnlyCollection<TemporaryTable> tempTables)
    {
        if (tempTables == null || tempTables.Count == 0)
        {
            return;
        }

        foreach (var tempTable in tempTables)
        {
            try
            {
                await cnn.ExecuteAsync(tempTable.DropSql).ConfigureAwait(false);
            }
            catch
            {
                //ignore
            }
        }
    }

    private IAuditSpanBuilder GetAuditSpanBuilder(string memberName, string sourceFilePath, int sourceLineNumber)
    {
        var spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
        return auditTracer.BuildSpan(AuditSpanType.MySqlDbQuery, spanName).WithStartDateUtc(DateTimeOffset.UtcNow);
    }
}
