using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess.Internals;

internal sealed class GridReaderWrapper : IGridReader
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
