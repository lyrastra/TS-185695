using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.MySqlDataAccess.Abstractions;

public interface IMySqlDbExecutor
{
    Task<TR[]> QueryAsync<TR>(
        string connectionString,
        QueryObject queryObject,
        QuerySetting settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> FirstOrDefaultAsync<TR>(
        string connectionString,
        QueryObject queryObject,
        QuerySetting settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<int> ExecuteAsync(
        string connectionString,
        QueryObject queryObject,
        QuerySetting settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
}
