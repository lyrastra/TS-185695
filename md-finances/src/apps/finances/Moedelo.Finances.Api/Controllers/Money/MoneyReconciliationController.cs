using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.Finances.Api.Controllers.Money
{
    [RoutePrefix("Money/Reconciliation")]
    [WebApiRejectUnauthorizedRequest]
    public class MoneyReconciliationController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IReconciliationForBackofficeService reconciliationForBackofficeService;
        private readonly IReconciliationForUserProcessor reconciliationForUserService;

        public MoneyReconciliationController(
            IUserContext userContext,
            IReconciliationForBackofficeService reconciliationForBackofficeService,
            IReconciliationForUserProcessor reconciliationForUserService)
        {
            this.userContext = userContext;
            this.reconciliationForBackofficeService = reconciliationForBackofficeService;
            this.reconciliationForUserService = reconciliationForUserService;
        }

        [HttpPost]
        [Route("ForBackoffice")]
        public async Task<IHttpActionResult> ReconcileForBackofficeAsync(ReconciliationForBackofficeRequestDto dto)
        {
            var request = MoneyReconciliationMapper.MapToDomain(dto);
            await reconciliationForBackofficeService.ProcessAsync(userContext, request).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost]
        [Route("ForUser")]
        public async Task<IHttpActionResult> ReconcileForUserAsync(ReconciliationForUserRequestDto dto)
        {
            var request = MoneyReconciliationMapper.MapToDomain(dto);
            await reconciliationForUserService.ProcessAsync(userContext, request).ConfigureAwait(false);
            return Ok();
        }
    }
}