using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.DataAccess.Extensions;

namespace Moedelo.InfrastructureV2.DataAccess.Internals;

internal sealed class QueryTemporaryTable : IAsyncDisposable
{
    private readonly SqlConnection connection;
    private readonly TemporaryTable tempTable;

    internal QueryTemporaryTable(SqlConnection connection, TemporaryTable tempTable)
    {
        this.connection = connection;
        this.tempTable = tempTable;
    }

    public async ValueTask DisposeAsync()
    {
        await connection
            .DropTemporaryTableAsync(tempTable, ignoreException: true)
            .ConfigureAwait(false);
    }
}
