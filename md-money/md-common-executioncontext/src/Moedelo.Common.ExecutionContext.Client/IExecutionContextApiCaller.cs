using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Common.ExecutionContext.Client;

internal interface IExecutionContextApiCaller
{
    /// <summary>
    /// Вызов POST с телом
    /// </summary>
    /// <param name="apiMethod">вызываемый метод</param>
    /// <param name="requestJsonBody">json-кодированное тело запроса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>ответ в виде строки</returns>
    Task<string> PostWithRetryAsync(
        ExecutionContextApiMethod apiMethod,
        string requestJsonBody,
        CancellationToken cancellationToken);

    /// <summary>
    /// Вызов POST без тела 
    /// </summary>
    /// <param name="apiMethod">вызываемый метод</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>ответ в виде строки</returns>
    Task<string> PostWithRetryAsync(
        ExecutionContextApiMethod apiMethod,
        CancellationToken cancellationToken);
}
