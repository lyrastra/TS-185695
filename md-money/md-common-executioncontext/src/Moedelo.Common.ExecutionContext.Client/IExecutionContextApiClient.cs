using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Common.ExecutionContext.Client
{
    public interface IExecutionContextApiClient
    {
        Task<string> GetTokenFromPublicAsync(string token, int? companyId);
        Task<string> GetTokenFromPublicAsync(string token, int? companyId, CancellationToken cancellationToken);

        Task<string> GetTokenFromUserContextAsync(int firmId, int userId);
        Task<string> GetTokenFromUserContextAsync(int firmId, int userId, CancellationToken cancellationToken);

        Task<string> GetTokenFromApiKeyAsync(string apiKey);
        Task<string> GetTokenFromApiKeyAsync(string apiKey, CancellationToken cancellationToken);

        Task<string> GetUnidentifiedTokenAsync();
        Task<string> GetUnidentifiedTokenAsync(CancellationToken cancellationToken);
    }
}