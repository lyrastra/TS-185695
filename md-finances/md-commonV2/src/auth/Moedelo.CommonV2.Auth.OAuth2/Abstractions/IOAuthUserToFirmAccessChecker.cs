using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.Auth.OAuth2.Abstractions
{
    public interface IOAuthUserToFirmAccessChecker
    {
        /// <summary>
        /// Проверить, имеет ли указанный пользователь доступ к указанной фирме в рамках oauth.
        /// Если <see cref="firmId"/>=0, то проверяется, имеет ли указанный пользователь доступ к сервису в рамках oauth без указания фирмы.
        /// Если  <see cref="firmId"/>!=0, то проверяется, имеет ли указанный пользователь доступ к указанной фирме
        /// ВНИМАНИЕ: консольные пользователи не могут имет доступ к фирме в рамках oauth. 
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="firmId">идентификатор фирмы (м.б. равен 0)</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>true - имеет доступ, false - не имеет</returns>
        Task<bool> DoesUserHaveOAuthAccessToFirmAsync(int userId, int firmId, CancellationToken cancellationToken = default);
    }
}
