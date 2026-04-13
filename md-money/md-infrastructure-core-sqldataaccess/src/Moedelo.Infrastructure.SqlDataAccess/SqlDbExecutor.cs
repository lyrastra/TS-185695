using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Infrastructure.SqlDataAccess.Internals;
using Moedelo.Infrastructure.SqlDataAccess.Options;

namespace Moedelo.Infrastructure.SqlDataAccess
{
    [InjectAsSingleton(typeof(ISqlDbExecutor))]
    internal sealed class SqlDbExecutor : ISqlDbExecutor
    {
        private static readonly QuerySetting emptyQuerySettings = new QuerySetting();
        private static readonly BulkCopySetting emptyBulkCopySettings = new BulkCopySetting();
        private const int DefaultTimeoutSeconds = 30;
        
        static SqlDbExecutor()
        {
            // По умолчанию Dapper мапит C# DateTime в DbType.DateTime (DateTime.MinValue при этом сохранить нельзя)
            SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
            SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
        }

        public SqlDbExecutor(IOptions<MsSqlDbExecutorOptions> options)
        {
            var optionsValue = options.Value;

            if (optionsValue.ReplaceUnspecifiedKindOfDateTimeByLocal)
            {
                SqlMapper.AddTypeHandler(new DateTimeUnspecifiedKindFixingHandler());
            }
        }

        public async Task<IReadOnlyList<TR>> QueryAsync<TR>(string connectionString,
            IQueryObject queryObject,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return result as IReadOnlyList<TR> ?? result.ToArray();
        }

        public async Task<HashSet<TR>> HashSetQueryAsync<TR>(string connectionString,
            IQueryObject queryObject,
            QuerySetting settings = null,
            IEqualityComparer<TR> equalityComparer = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return new HashSet<TR>(result, equalityComparer);
        }

        public async Task<TR> FirstOrDefaultAsync<TR>(string connectionString,
            IQueryObject queryObject,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return result;
        }

        public async Task<int> ExecuteAsync(string connectionString,
            IQueryObject queryObject,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return result;
        }

        public async Task<TR> QueryMultipleAsync<TR>(string connectionString,
            IQueryObject queryObject,
            Func<IGridReader, Task<TR>> readAction,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                queryObject.QueryParams,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            await using var dbResult = await cnn.QueryMultipleAsync(commandDefinition).ConfigureAwait(false);
            var reader = new GridReaderWrapper(dbResult);
            var result = await readAction(reader).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return result;
        }

        public async Task<IReadOnlyList<TR>> QueryAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return result as IReadOnlyList<TR> ?? result.ToArray();
        }

        public async Task<HashSet<TR>> HashSetQueryAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            QuerySetting settings = null,
            IEqualityComparer<TR> equalityComparer = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            if (settings == null)
            {
                settings = emptyQuerySettings;
            }

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return new HashSet<TR>(result, equalityComparer);
        }

        public async Task QueryAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            Action<IReadOnlyList<TR>, IOutParameterReader> readAction,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            
            readAction(result as IReadOnlyList<TR> ?? result.ToArray(), new OutParameterReader(dynamicParameters));
        }

        public async Task HashSetQueryAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            Action<HashSet<TR>, IOutParameterReader> readAction,
            QuerySetting settings = null,
            IEqualityComparer<TR> equalityComparer = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            readAction(new HashSet<TR>(result, equalityComparer), new OutParameterReader(dynamicParameters));
        }

        public async Task<TR> FirstOrDefaultAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            return result;
        }

        public async Task FirstOrDefaultAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            Action<TR, IOutParameterReader> readAction,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.QueryFirstOrDefaultAsync<TR>(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);

            readAction(result, new OutParameterReader(dynamicParameters));
        }

        public async Task<int> ExecuteAsync(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            Action<IOutParameterReader> readAction,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;

            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            var result = await cnn.ExecuteAsync(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            readAction(new OutParameterReader(dynamicParameters));

            return result;
        }

        public async Task<TR> QueryMultipleAsync<TR>(string connectionString,
            IQueryObjectWithDynamicParams queryObject,
            Func<IGridReader, Task<TR>> readAction,
            QuerySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyQuerySettings;
            
            var dynamicParameters = GetParameters(queryObject);

            await using var cnn = new SqlConnection(connectionString);
            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await CreateTempTables(cnn, queryObject.TemporaryTables, settings.TimeoutSeconds, cancellationToken)
                .ConfigureAwait(false);

            var commandDefinition = new CommandDefinition(
                queryObject.Sql,
                dynamicParameters,
                settings.Transaction,
                settings.TimeoutSeconds,
                queryObject.CommandType,
                cancellationToken: cancellationToken);

            await using var dbResult = await cnn.QueryMultipleAsync(commandDefinition).ConfigureAwait(false);
            await DropTempTables(cnn, queryObject.TemporaryTables).ConfigureAwait(false);
            var reader = new GridReaderWrapper(dbResult);
            return await readAction(reader).ConfigureAwait(false);
        }

        public async Task BulkCopyAsync(string connectionString,
            IBulkCopyQueryObject queryObject,
            BulkCopySetting settings = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentExtensions.NotNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentExtensions.NotNull(queryObject, nameof(queryObject));

            settings ??= emptyBulkCopySettings;

            await using var cnn = new SqlConnection(connectionString);
            using var sqlBulkCopy = new SqlBulkCopy(cnn, settings.CopyOptions, settings.Transaction);
            sqlBulkCopy.DestinationTableName = queryObject.Name;
            sqlBulkCopy.BulkCopyTimeout = settings.Timeout ?? 30;

            if (settings.Mappings != null)
            {
                foreach (var mapping in settings.Mappings)
                {
                    sqlBulkCopy.ColumnMappings.Add(mapping);
                }
            }

            await cnn.OpenAsync(cancellationToken).ConfigureAwait(false);
            await sqlBulkCopy.WriteToServerAsync(queryObject.DataTable, cancellationToken).ConfigureAwait(false);
        }

        private static async Task CreateTempTables(
            SqlConnection cnn,
            IReadOnlyCollection<TemporaryTable> tempTables,
            int? timeout,
            CancellationToken cancellationToken)
        {
            if (tempTables == null || tempTables.Count == 0)
            {
                return;
            }

            foreach (var tempTable in tempTables)
            {
                await cnn.ExecuteAsync(tempTable.CreateSql).ConfigureAwait(false);

                using var sqlBulkCopy = new SqlBulkCopy(cnn);
                sqlBulkCopy.DestinationTableName = tempTable.Name;
                sqlBulkCopy.BulkCopyTimeout = timeout ?? DefaultTimeoutSeconds;

                await sqlBulkCopy
                    .WriteToServerAsync(tempTable.DataTable, cancellationToken)
                    .ConfigureAwait(false);
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
    }
}
