using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

public interface IGridReader
{
    Task<List<T>> ReadListAsync<T>();

    Task<T> ReadFirstOrDefaultAsync<T>();
}