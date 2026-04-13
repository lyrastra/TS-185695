using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Data;
using Moedelo.Finances.Public.Mappers;

namespace Moedelo.Finances.Public.Controllers
{
    [RoutePrefix("Data")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly ISetupDataService setupDataService;

        public DataController(
            IUserContext userContext,
            ISetupDataService setupDataService)
        {
            this.userContext = userContext;
            this.setupDataService = setupDataService;
        }

        [HttpGet]
        [Route("Setup")]
        public async Task<IHttpActionResult> GetAsync(CancellationToken ctx)
        {
            var setup = await setupDataService.GetAsync(userContext, ctx).ConfigureAwait(false);
            return Data(setup.MapToClient());
        }

        [HttpGet]
        [Route("GetAccessToMoneyEdit")]
        public async Task<IHttpActionResult> GetAccessToMoneyEditAsync()
        {
            var result = await userContext.HasAnyRuleAsync(AccessRule.AccessToEditAccountingBank, AccessRule.AccessToEditAccountingCash).ConfigureAwait(false);
            return Data(result);
        }
    }
}
