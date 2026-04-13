using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;

public interface IGridReader
{
    Task<IReadOnlyList<T>> ReadArrayAsync<T>();
        
    Task<HashSet<T>> ReadHashSetAsync<T>(IEqualityComparer<T>? equalityComparer = null);

    Task<T> ReadFirstOrDefaultAsync<T>();
}