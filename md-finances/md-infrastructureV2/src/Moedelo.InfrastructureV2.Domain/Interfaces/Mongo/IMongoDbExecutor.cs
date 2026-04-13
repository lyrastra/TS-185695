using System.Runtime.CompilerServices;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Mongo;

public interface IMongoDbExecutor
{
    Task<T> FindByIdAsync<T>(
        string id,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where T : class, IMongoObject;

    Task InsertAsync<T>(
        T document,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where T : class, IMongoObject;

    Task UpdateAsync<T>(
        T document,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where T : class, IMongoObject;

    Task DeleteByIdAsync<T>(
        string id,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where T : class, IMongoObject;
}