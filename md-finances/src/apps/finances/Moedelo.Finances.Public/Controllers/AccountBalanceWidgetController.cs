using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.AccountBalances;
using Moedelo.Finances.Public.Mappers;

namespace Moedelo.Finances.Public.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("AccountBalanceWidget")]
    public class AccountBalanceWidgetController(IUserContext userContext, IAccountBalanceService accountBalanceService) : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var firmsSettlementAccountsBalances = await accountBalanceService
                .GetFirmSettlementAccountBalanceAsync(userContext, cancellationToken)
                .ConfigureAwait(false);

            return Data(AccountBalanceWidgetMapper.Map(firmsSettlementAccountsBalances));
        }
    }
}