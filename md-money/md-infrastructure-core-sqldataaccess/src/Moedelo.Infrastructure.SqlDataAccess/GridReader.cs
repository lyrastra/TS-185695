using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;

namespace Moedelo.Infrastructure.SqlDataAccess
{
    internal sealed class GridReaderWrapper : IGridReader
    {
        private readonly SqlMapper.GridReader reader;

        public GridReaderWrapper(SqlMapper.GridReader reader)
        {
            this.reader = reader;
        }

        public async Task<IReadOnlyList<T>> ReadArrayAsync<T>()
        {
            var result = await reader.ReadAsync<T>().ConfigureAwait(false);

            switch (result)
            {
                case IReadOnlyList<T> readOnlyList:
                    return readOnlyList;
                default:
                    return result.ToArray();
            }
        }

        public async Task<HashSet<T>> ReadHashSetAsync<T>(IEqualityComparer<T> equalityComparer = null)
        {
            var result = await reader.ReadAsync<T>().ConfigureAwait(false);

            return new HashSet<T>(result, equalityComparer);
        }

        public async Task<T> ReadFirstOrDefaultAsync<T>()
        {
            var result = await reader.ReadFirstOrDefaultAsync<T>().ConfigureAwait(false);

            return result;
        }
    }
}