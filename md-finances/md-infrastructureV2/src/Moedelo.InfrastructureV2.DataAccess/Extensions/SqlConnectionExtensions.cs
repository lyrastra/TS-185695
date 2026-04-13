using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Moedelo.InfrastructureV2.DataAccess.Internals;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

internal static class SqlConnectionExtensions
{
    private const int DefaultBulkCopyTimeoutSeconds = 30;

    internal static async Task<IAsyncDisposable> CreateTemporaryTableAsync<TRow>(
        this SqlConnection connection,
        string tableName,
        IEnumerable<TRow> rows,
        QuerySetting settings,
        CancellationToken cancellationToken)
    {
        var dataTable = rows.ToListTvp(typeof(TRow), tableName);
        var tempTable = dataTable.CreateTemporaryTable();
        await connection
            .CreateTemporaryTableAsync(tempTable, settings, cancellationToken)
            .ConfigureAwait(false);

        return new QueryTemporaryTable(connection, tempTable);
    }

    internal static async Task<IAsyncDisposable> CreateTemporaryTablesAsync(
        this SqlConnection connection,
        IEnumerable<KeyValuePair<string, IList>> temporaryData,
        QuerySetting settings,
        CancellationToken cancellationToken)
    {
        var createdTemporaryTablesList = new List<TemporaryTable>();

        try
        {
            await connection
                .CreateTemporaryTablesAsync(temporaryData, settings, createdTemporaryTablesList, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            await connection
                .DropTemporaryTablesAsync(createdTemporaryTablesList, ignoreException: true)
                .ConfigureAwait(false);
            throw;
        }

        return new QueryTemporaryTables(connection, createdTemporaryTablesList);
    }

    private static async Task CreateTemporaryTablesAsync(
        this SqlConnection connection,
        IEnumerable<KeyValuePair<string, IList>> temporaryData,
        QuerySetting settings,
        List<TemporaryTable> createdTemporaryTablesList,
        CancellationToken cancellationToken)
    {
        createdTemporaryTablesList = createdTemporaryTablesList ?? throw new ArgumentNullException(nameof(createdTemporaryTablesList));
        
        foreach (var dataEntry in temporaryData)
        {
            var listItemType = dataEntry.Value.GetListItemType();
            var tempTable = dataEntry.Value
                .ToListTvp(listItemType, dataEntry.Key)
                .CreateTemporaryTable();

            await connection.CreateTemporaryTableAsync(tempTable, settings, cancellationToken).ConfigureAwait(false);

            createdTemporaryTablesList.Add(tempTable);
        }
    }

    private static async Task CreateTemporaryTableAsync(
        this SqlConnection connection,
        TemporaryTable tempTable,
        QuerySetting settings,
        CancellationToken cancellationToken)
    {
        await connection.ExecuteAsync(tempTable.CreationSql).ConfigureAwait(false);

        using var sqlBulkCopy = new SqlBulkCopy(connection);
        sqlBulkCopy.DestinationTableName = tempTable.SqlTableName;
        sqlBulkCopy.BulkCopyTimeout = settings.Timeout ?? DefaultBulkCopyTimeoutSeconds;

        await sqlBulkCopy.WriteToServerAsync(tempTable.DataTable, cancellationToken).ConfigureAwait(false);
    }

    internal static async Task DropTemporaryTableAsync(this SqlConnection connection, TemporaryTable tempTable, bool ignoreException)
    {
        try
        {
            await connection.ExecuteAsync(tempTable.DropSql).ConfigureAwait(false);
        }
        catch when(ignoreException)
        {
            //ignore
        }
    }

    internal static async Task DropTemporaryTablesAsync(
        this SqlConnection connection,
        IEnumerable<TemporaryTable> tempTableList,
        bool ignoreException)
    {
        try
        {
            var dropAllTablesQuerySql = string.Join(";", tempTableList.Select(table => table.DropSql));

            if (string.IsNullOrEmpty(dropAllTablesQuerySql))
            {
                return;
            }

            await connection.ExecuteAsync(dropAllTablesQuerySql).ConfigureAwait(false);
        }
        catch when(ignoreException)
        {
            //ignore
        }
    }
}
