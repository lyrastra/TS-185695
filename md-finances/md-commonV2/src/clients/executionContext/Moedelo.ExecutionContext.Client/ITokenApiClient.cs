using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ExecutionContext.Client
{
    /// <summary>
    /// Получение ExecutionContext-токена
    /// </summary>
    public interface ITokenApiClient : IDI
    {
        /// <summary>
        /// Получает токен на основе пары { firmId, userId }
        /// </summary>
        Task<string> GetFromUserContextAsync(int firmId, int userId);

        /// <summary>
        /// Получает токен для неавторизованного запроса
        /// </summary>
        Task<string> GetUnidentified();
    }
}