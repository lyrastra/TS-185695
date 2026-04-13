using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.DataAccess.Extensions;

namespace Moedelo.InfrastructureV2.DataAccess.Internals;

internal sealed class QueryTemporaryTables : IAsyncDisposable
{
    private readonly SqlConnection connection;
    private readonly IEnumerable<TemporaryTable> tempTableList;

    internal QueryTemporaryTables(SqlConnection connection, IEnumerable<TemporaryTable> tempTableList)
    {
        this.connection = connection;
        this.tempTableList = tempTableList;
    }

    public async ValueTask DisposeAsync()
    {
        await connection
            .DropTemporaryTablesAsync(tempTableList, ignoreException: true)
            .ConfigureAwait(false);
    }
}