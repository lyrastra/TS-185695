using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.Auth.OAuth2.Abstractions;
using Moedelo.CommonV2.Auth.OAuth2.DataAccess;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.CommonV2.Auth.OAuth2.Services
{
    [InjectAsSingleton(typeof(IOAuthUserToFirmAccessChecker))]
    internal sealed class OAuthUserToFirmAccessChecker : IOAuthUserToFirmAccessChecker
    {
        private const int UndefinedFirm = 0;
        
        private readonly IMoedeloDbExecutor moedeloDbExecutor;
        private readonly IMoedeloReadOnlyDbExecutor moedeloReadOnlyDbExecutor;

        public OAuthUserToFirmAccessChecker(
            IMoedeloDbExecutor moedeloDbExecutor,
            IMoedeloReadOnlyDbExecutor moedeloReadOnlyDbExecutor)
        {
            this.moedeloDbExecutor = moedeloDbExecutor;
            this.moedeloReadOnlyDbExecutor = moedeloReadOnlyDbExecutor;
        }

        public Task<bool> DoesUserHaveOAuthAccessToFirmAsync(int userId, int firmId,
            CancellationToken cancellationToken)
        {
            if (firmId == UndefinedFirm)
            {
                return IsUserProfOutsourcerOrLostAccountAsync(userId, cancellationToken);
            }

            return DoesUserFirmDataExistAsync(userId, firmId, cancellationToken);
        }

        private Task<bool> IsUserProfOutsourcerOrLostAccountAsync(int userId, CancellationToken cancellationToken)
        {
            var queryObject = new QueryObject(Queries.IsProfOutsourceOrLostAccount, new { UserId = userId });
                
            return QueryFromBothExecutorsAsync(queryObject, cancellationToken);
        }

        private Task<bool> DoesUserFirmDataExistAsync(int userId, int firmId, CancellationToken cancellationToken)
        {
            var queryObject = new QueryObject(Queries.CheckUserFirmId, new { UserId = userId, FirmId = firmId });

            return QueryFromBothExecutorsAsync(queryObject, cancellationToken);
        }

        private async Task<bool> QueryFromBothExecutorsAsync(
            QueryObject queryObject,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "")
        {
            return await moedeloReadOnlyDbExecutor
                       .FirstOrDefaultAsync<int>(queryObject, memberName: memberName, cancellationToken: cancellationToken)
                       .ConfigureAwait(false) == 1
                   || await moedeloDbExecutor
                       .FirstOrDefaultAsync<int>(queryObject, memberName: memberName, cancellationToken: cancellationToken)
                       .ConfigureAwait(false) == 1;
        }
    }
}
