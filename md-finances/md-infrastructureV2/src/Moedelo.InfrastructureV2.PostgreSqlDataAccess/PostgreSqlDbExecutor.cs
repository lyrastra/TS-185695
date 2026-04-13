using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.PostgreSqlDataAccess.Extensions;
using Npgsql;

namespace Moedelo.InfrastructureV2.PostgreSqlDataAccess
{
    public abstract class PostgreSqlDbExecutor : IDbExecutor
    {
        private static readonly QuerySetting EmptyQuerySettings = new QuerySetting();
        private readonly SettingValue connectionStringSetting;
        private readonly IAuditTracer auditTracer;

        protected PostgreSqlDbExecutor
        (
            SettingValue connectionStringSetting,
            IAuditTracer auditTracer
        )
        {
            this.connectionStringSetting = connectionStringSetting;
            this.auditTracer = auditTracer;
        }

        public async Task<List<TR>> QueryAsync<TR>(
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync<TR>(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        return result.ToList();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<TR> SingleAsync<TR>(
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QuerySingleAsync<TR>(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<TR> SingleOrDefaultAsync<TR>(
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QuerySingleOrDefaultAsync<TR>(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<List<TR>> QueryAsync<TR>(
            IList<QueryObject> queryObjects,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObjects)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var count = queryObjects.Count;

                        for (var i = 0; i < count - 1; i++)
                        {
                            var queryObject = queryObjects[i];
                            var queryParams = GetQueryParams(queryObject.QueryParams);

                            await cnn.ExecuteAsync(
                                queryObject.Sql,
                                queryParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);
                        }

                        var resultQueryObject = queryObjects[count - 1];
                        var resultQueryParams = GetQueryParams(resultQueryObject.QueryParams);
                        var result = await cnn.QueryAsync<TR>(
                            resultQueryObject.Sql,
                            resultQueryParams,
                            settings.Transaction,
                            settings.Timeout,
                            resultQueryObject.CommandType).ConfigureAwait(false);

                        return result.ToList();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<List<TR>> QueryDistinctAsync<TR>(
            QueryObject queryObject,
            Type[] types,
            Func<object[], TR> map,
            string splitOn = "Id",
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync(
                            queryObject.Sql,
                            types,
                            map,
                            queryParams,
                            settings.Transaction,
                            commandTimeout: settings.Timeout,
                            commandType: queryObject.CommandType,
                            splitOn: splitOn).ConfigureAwait(false);

                        return result.Distinct().ToList();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<List<TR>> QueryNoDistinctAsync<TR>(
            QueryObject queryObject,
            Type[] types,
            Func<object[], TR> map,
            string splitOn = "Id",
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync(
                            queryObject.Sql,
                            types,
                            map,
                            queryParams,
                            settings.Transaction,
                            commandTimeout: settings.Timeout,
                            commandType: queryObject.CommandType,
                            splitOn: splitOn).ConfigureAwait(false);

                        return result.ToList();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<List<TR>> QueryAsync<TR, TT>(
            string temporyName,
            List<TT> temporyData,
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TT : class
        {
            if (string.IsNullOrWhiteSpace(temporyName) || temporyData == null)
            {
                throw new ArgumentNullException();
            }

            var temporaryTableSql = CreateTemporaryTableSql<TT>(temporyName);
            var temporaryTableData = temporyData.ToListTvp(temporyName);

            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);
                        await cnn.ExecuteAsync(temporaryTableSql).ConfigureAwait(false);

                        PostgreBulkCopy(cnn, $"temp_{temporyName}", temporaryTableData);

                        try
                        {
                            var result = await cnn.QueryAsync<TR>(
                                queryObject.Sql,
                                queryParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);

                            return result.ToList();
                        }
                        finally
                        {
                            try
                            {
                                await cnn.ExecuteAsync($"drop table temp_{temporyName}").ConfigureAwait(false);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<List<TR>> QueryAsync<TR>(
            Dictionary<string, IList> temporaryData,
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!temporaryData.Any() || temporaryData.Any(dataEntry => dataEntry.Value == null))
            {
                throw new ArgumentOutOfRangeException(nameof(temporaryData));
            }

            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var createdTemporaryNameList = new List<string>();

                        try
                        {
                            foreach (var dataEntry in temporaryData)
                            {
                                var listType = GetListType(dataEntry);

                                var temporaryTableSql = CreateTemporaryTableSql(dataEntry.Key, listType);
                                var temporaryTableData = dataEntry.Value.ToListTvp(listType, dataEntry.Key);

                                await cnn.ExecuteAsync(temporaryTableSql).ConfigureAwait(false);
                                createdTemporaryNameList.Add(dataEntry.Key);

                                PostgreBulkCopy(cnn, $"temp_{dataEntry.Key}", temporaryTableData);
                            }

                            var result =
                                await cnn.QueryAsync<TR>(
                                    queryObject.Sql,
                                    queryParams,
                                    settings.Transaction,
                                    settings.Timeout,
                                    queryObject.CommandType).ConfigureAwait(false);

                            return result.ToList();
                        }
                        finally
                        {
                            foreach (var temporaryName in createdTemporaryNameList)
                            {
                                try
                                {
                                    await cnn.ExecuteAsync($"drop table temp_{temporaryName}").ConfigureAwait(false);
                                }
                                catch
                                {
                                    //ignore
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<List<TR>> QueryAsync<TR>(
            QueryObjectWithDynamicParams queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var dynamicParams = GetDynamicParameters(queryObject);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync<TR>(
                            queryObject.Sql,
                            dynamicParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        return result.ToList();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task ExecuteAsync(
            IDictionary<string, IList> temporaryData,
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (temporaryData.Any(dataEntry => dataEntry.Value == null))
            {
                throw new ArgumentOutOfRangeException(nameof(temporaryData));
            }

            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var createdTemporaryNameList = new List<string>();

                        try
                        {
                            foreach (var dataEntry in temporaryData)
                            {
                                var listType = GetListType(dataEntry);

                                var temporaryTableSql = CreateTemporaryTableSql(dataEntry.Key, listType);
                                var temporaryTableData = dataEntry.Value.ToListTvp(listType, dataEntry.Key);

                                await cnn.ExecuteAsync(temporaryTableSql).ConfigureAwait(false);
                                createdTemporaryNameList.Add(dataEntry.Key);

                                PostgreBulkCopy(cnn, $"temp_{dataEntry.Key}", temporaryTableData);
                            }

                            await cnn.ExecuteAsync(
                                queryObject.Sql,
                                queryParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);
                        }
                        finally
                        {
                            foreach (var temporaryName in createdTemporaryNameList)
                            {
                                try
                                {
                                    await cnn.ExecuteAsync($"drop table temp_{temporaryName}").ConfigureAwait(false);
                                }
                                catch
                                {
                                    //ignore
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task ExecuteAsync<TT>(
            string temporyName,
            List<TT> temporyData,
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TT : class
        {
            if (string.IsNullOrWhiteSpace(temporyName) || temporyData == null)
            {
                throw new ArgumentNullException();
            }

            var temporaryTableSql = CreateTemporaryTableSql<TT>(temporyName);
            var temporaryTableData = temporyData.ToListTvp(temporyName);

            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);
                        await cnn.ExecuteAsync(temporaryTableSql).ConfigureAwait(false);

                        PostgreBulkCopy(cnn, $"temp_{temporyName}", temporaryTableData);

                        try
                        {
                            await cnn.ExecuteAsync(
                                queryObject.Sql,
                                queryParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);
                        }
                        finally
                        {
                            try
                            {
                                await cnn.ExecuteAsync($"drop table temp_{temporyName}").ConfigureAwait(false);
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        private static Type GetListType(KeyValuePair<string, IList> dataEntry)
        {
            if (dataEntry.Value.Count > 0)
            {
                return dataEntry.Value[0].GetType();
            }

            var genericArguments = dataEntry.Value.GetType().GetGenericArguments();
            if (genericArguments.Length > 0)
            {
                return genericArguments[0];
            }

            throw new ArgumentException("Can not determine list type");
        }

        public async Task<TR> FirstOrDefaultAsync<TR>(
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync<TR>(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        return result.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<TR> FirstOrDefaultDistinctAsync<TR>(
            QueryObject queryObject,
            Type[] types,
            Func<object[], TR> map,
            string splitOn = "Id",
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync(
                            queryObject.Sql,
                            types,
                            map,
                            queryParams,
                            settings.Transaction,
                            commandTimeout: settings.Timeout,
                            commandType: queryObject.CommandType,
                            splitOn: splitOn).ConfigureAwait(false);

                        return result.Distinct().FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<TR> FirstOrDefaultNoDistinctAsync<TR>(
            QueryObject queryObject,
            Type[] types,
            Func<object[], TR> map,
            string splitOn = "Id",
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync(
                            queryObject.Sql,
                            types,
                            map,
                            queryParams,
                            settings.Transaction,
                            commandTimeout: settings.Timeout,
                            commandType: queryObject.CommandType,
                            splitOn: splitOn).ConfigureAwait(false);

                        return result.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<TR> FirstOrDefaultAsync<TR>(
            IList<QueryObject> queryObjects,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObjects)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var count = queryObjects.Count;

                        for (var i = 0; i < count - 1; i++)
                        {
                            var queryObject = queryObjects[i];
                            var queryParams = GetQueryParams(queryObject.QueryParams);

                            await cnn.ExecuteAsync(
                                queryObject.Sql,
                                queryParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);
                        }

                        var resultQueryObject = queryObjects[count - 1];
                        var resultQueryParams = GetQueryParams(resultQueryObject.QueryParams);

                        var result =
                            await cnn.QueryAsync<TR>(
                                resultQueryObject.Sql,
                                resultQueryParams,
                                settings.Transaction,
                                settings.Timeout,
                                resultQueryObject.CommandType).ConfigureAwait(false);

                        return result.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task QueryMultipleAsync(
            QueryObject queryObject,
            Func<IGridReader, Task> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        using (var gridReader = await cnn.QueryMultipleAsync(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false))
                        {
                            var wrapper = new GridReaderWrapper(gridReader);
                            await readAction(wrapper).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<T> QueryMultipleAsync<T>(
            QueryObject queryObject,
            Func<IGridReader, Task<T>> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        using (var gridReader = await cnn.QueryMultipleAsync(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false))
                        {
                            var wrapper = new GridReaderWrapper(gridReader);
                            var result = await readAction(wrapper).ConfigureAwait(false);

                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task QueryMultipleAsync(
            QueryObjectWithDynamicParams queryObject,
            Func<IGridReader, Task> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var dynamicParams = GetDynamicParameters(queryObject);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        using (var gridReader = await cnn.QueryMultipleAsync(
                            queryObject.Sql,
                            dynamicParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false))
                        {
                            var wrapper = new GridReaderWrapper(gridReader);
                            await readAction(wrapper).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task QueryMultipleAsync(
            QueryObjectWithDynamicParams queryObject,
            Func<IGridReader, IOutParameterReader, Task> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var dynamicParams = GetDynamicParameters(queryObject);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        using (var gridReader = await cnn.QueryMultipleAsync(
                            queryObject.Sql,
                            dynamicParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false))
                        {
                            var wrapper = new GridReaderWrapper(gridReader);
                            await readAction(wrapper, new OutParameterReader(dynamicParams)).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task QueryAsync<TR>(
            QueryObjectWithDynamicParams queryObject,
            Action<IEnumerable<TR>, IOutParameterReader> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var dynamicParams = GetDynamicParameters(queryObject);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var result = await cnn.QueryAsync<TR>(
                            queryObject.Sql,
                            dynamicParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        readAction(result, new OutParameterReader(dynamicParams));
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<int> ExecuteAsync(
            QueryObject queryObject,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var queryParams = GetQueryParams(queryObject.QueryParams);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var count = await cnn.ExecuteAsync(
                            queryObject.Sql,
                            queryParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        return count;
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<int> ExecuteAsync(
            IList<QueryObject> queryObjects,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObjects)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var count = queryObjects.Count;

                        for (var i = 0; i < count - 1; i++)
                        {
                            var queryObject = queryObjects[i];
                            var queryParams = GetQueryParams(queryObject.QueryParams);

                            await cnn.ExecuteAsync(
                                queryObject.Sql,
                                queryParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);
                        }

                        var resultQueryObject = queryObjects[count - 1];
                        var resultQueryParams = GetQueryParams(resultQueryObject.QueryParams);

                        var result = await cnn.ExecuteAsync(
                            resultQueryObject.Sql,
                            resultQueryParams,
                            settings.Transaction,
                            settings.Timeout,
                            resultQueryObject.CommandType).ConfigureAwait(false);

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task ExecuteAsync(
            QueryObjectWithDynamicParams queryObject,
            Action<IOutParameterReader> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;
            var dynamicParams = GetDynamicParameters(queryObject);

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        await cnn.ExecuteAsync(
                            queryObject.Sql,
                            dynamicParams,
                            settings.Transaction,
                            settings.Timeout,
                            queryObject.CommandType).ConfigureAwait(false);

                        readAction(new OutParameterReader(dynamicParams));
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task BulkCopyAsync(
            string table,
            DataTable data,
            BulkCopySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (string.IsNullOrWhiteSpace(table) || data == null)
            {
                throw new ArgumentNullException();
            }

            var connectionString = connectionStringSetting.Value;

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        PostgreBulkCopy(cnn, table, data);
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        private static void PostgreBulkCopy(NpgsqlConnection cnn, string tableName, DataTable dataTable)
        {
            using (var writer = cnn.BeginBinaryImport(GetBinaryImportSql(tableName, dataTable)))
            {
                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    var row = dataTable.Rows[i];
                    writer.WriteRow(row.ItemArray);
                }

                writer.Complete();
            }
        }

        private static string GetBinaryImportSql(string tableName, DataTable dataTable)
        {
            var columns = new List<string>(dataTable.Columns.Count);

            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                var column = dataTable.Columns[i];
                columns.Add(column.ColumnName);
            }

            return $"copy {tableName} ({string.Join(",", columns)}) FROM stdin BINARY";
        }

        private static DynamicParameters GetDynamicParameters(QueryObjectWithDynamicParams queryObject)
        {
            var dynamicParams = new DynamicParameters();

            foreach (var param in queryObject.DynamicParams)
            {
                dynamicParams.Add(param.Name, ChangeIfTvp(param.Value), param.DbType, param.Direction, param.Size,
                    param.Precision, param.Scale);
            }

            return dynamicParams;
        }

        private static object GetQueryParams(object queryParams)
        {
            if (queryParams == null)
            {
                return null;
            }

            var list = queryParams as IEnumerable<object>;

            if (list != null)
            {
                return list.Select(PrepareQueryParams).ToList();
            }

            return PrepareQueryParams(queryParams);
        }

        private static object PrepareQueryParams(object queryParams)
        {
            if (queryParams is IDictionary<string, object>)
            {
                return PrepareQueryParams((IDictionary<string, object>)queryParams);
            }

            var result = new ExpandoObject();
            var valueType = queryParams.GetType();
            var props = new List<PropertyInfo>(valueType.GetProperties());

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(queryParams, null);
                propValue = ChangeIfTvp(propValue);
                ((IDictionary<string, object>)result).Add(prop.Name, propValue);
            }

            return result;
        }

        private static object PrepareQueryParams(IDictionary<string, object> queryParams)
        {
            var result = (IDictionary<string, object>)new ExpandoObject();

            foreach (var keyValuePair in queryParams)
            {
                var value = ChangeIfTvp(keyValuePair.Value);
                result.Add(keyValuePair.Key, value);
            }

            return result;
        }

        private static object ChangeIfTvp(object propValue)
        {
            var dataTable = propValue as DataTable;

            return dataTable == null ? propValue : dataTable.AsTableValuedParameter(dataTable.TableName);
        }

        private static string CreateTemporaryTableSql<T>(string name) where T : class
        {
            return CreateTemporaryTableSql(name, typeof(T));
        }

        private static string CreateTemporaryTableSql(string name, Type type)
        {
            var propertyInfos = type.GetProperties();

            if (!type.IsClass && type != typeof(string))
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            var sql = new StringBuilder();

            sql.Append($"create temp table temp_{name} (");

            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var property = propertyInfos[i];
                var isNullable = property.PropertyType.IsGenericType
                                 && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                 || property.PropertyType == typeof(string);

                sql.Append(
                    $"{(i == 0 ? "" : " ,")}{property.Name} {PostgreSqlHelper.GetDbTypeName(property.PropertyType)} {(isNullable ? "null" : "not null")}");
            }

            sql.Append(")");

            return sql.ToString();
        }

        public async Task QueryAsync<TR>(
            Dictionary<string, IList> temporaryData,
            QueryObjectWithDynamicParams queryObject,
            Action<IEnumerable<TR>, IOutParameterReader> readAction,
            QuerySetting settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!temporaryData.Any() || temporaryData.Any(dataEntry => dataEntry.Value == null))
            {
                throw new ArgumentOutOfRangeException(nameof(temporaryData));
            }

            if (settings == null)
            {
                settings = EmptyQuerySettings;
            }

            var connectionString = connectionStringSetting.Value;

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnectionString(connectionString)
                .WithQueryObject(queryObject)
                .Start())
            {
                try
                {
                    using (var cnn = new NpgsqlConnection(connectionString))
                    {
                        await cnn.OpenAsync().ConfigureAwait(false);

                        var createdTemporaryNameList = new List<string>();

                        try
                        {
                            foreach (var dataEntry in temporaryData)
                            {
                                var listType = GetListType(dataEntry);

                                var temporaryTableSql = CreateTemporaryTableSql(dataEntry.Key, listType);
                                var temporaryTableData = dataEntry.Value.ToListTvp(listType, dataEntry.Key);

                                await cnn.ExecuteAsync(temporaryTableSql).ConfigureAwait(false);
                                createdTemporaryNameList.Add(dataEntry.Key);

                                PostgreBulkCopy(cnn, $"temp_{dataEntry.Key}", temporaryTableData);
                            }

                            var dynamicParams = GetDynamicParameters(queryObject);

                            var result = await cnn.QueryAsync<TR>(
                                queryObject.Sql,
                                dynamicParams,
                                settings.Transaction,
                                settings.Timeout,
                                queryObject.CommandType).ConfigureAwait(false);
                            readAction(result, new OutParameterReader(dynamicParams));
                        }
                        finally
                        {
                            foreach (var temporaryName in createdTemporaryNameList)
                            {
                                try
                                {
                                    await cnn.ExecuteAsync($"drop table temp_{temporaryName}").ConfigureAwait(false);
                                }
                                catch
                                {
                                    //ignore
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        private IAuditSpanBuilder GetAuditSpanBuilder(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            var spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
            return auditTracer.BuildSpan(AuditSpanType.MsSqlDbQuery, spanName).WithStartDateUtc(DateTimeOffset.UtcNow);
        }

        private class GridReaderWrapper : IGridReader
        {
            private readonly SqlMapper.GridReader reader;

            public GridReaderWrapper(SqlMapper.GridReader reader)
            {
                this.reader = reader;
            }

            public async Task<List<T>> ReadListAsync<T>()
            {
                var result = await reader.ReadAsync<T>().ConfigureAwait(false);

                return result.ToList();
            }

            public async Task<T> ReadFirstOrDefaultAsync<T>()
            {
                var result = await reader.ReadAsync<T>().ConfigureAwait(false);

                return result.FirstOrDefault();
            }
        }

        private class OutParameterReader : IOutParameterReader
        {
            private readonly DynamicParameters dynamicParameters;

            public OutParameterReader(DynamicParameters dynamicParameters)
            {
                this.dynamicParameters = dynamicParameters;
            }

            public T Read<T>(string paramName)
            {
                return dynamicParameters.Get<T>(paramName);
            }
        }
    }
}