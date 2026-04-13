using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Client.Money.Dto;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;

namespace Moedelo.Finances.Api.Controllers.Money
{
    [RoutePrefix("Money/Balances")]
    [WebApiRejectUnauthorizedRequest]
    public class MoneyBalancesController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneySourceReader sourceReader;
        private readonly IMoneyBalancesReader balanceReader;
        private readonly IReconciliationForUserInitiator reconciliationService;

        public MoneyBalancesController(
            IUserContext userContext,
            IMoneySourceReader sourceReader,
            IMoneyBalancesReader balanceReader,
            IReconciliationForUserInitiator reconciliationService)
        {
            this.userContext = userContext;
            this.sourceReader = sourceReader;
            this.balanceReader = balanceReader;
            this.reconciliationService = reconciliationService;
        }

        [HttpPost]
        [Route("")]
        public async Task<List<MoneySourceBalanceDto>> GetAsync(BalanceRequestDto request)
        {
            if (request.Sources.Count == 0)
            {
                return new List<MoneySourceBalanceDto>();
            }
            var sourcesBase = MoneyBalancesMapper.Map(request.Sources);
            var sources = await sourceReader.GetAsync(userContext, sourcesBase).ConfigureAwait(false);
            var balances = await balanceReader.GetAsync(userContext, sources, request.OnDate).ConfigureAwait(false);
            return MoneyBalancesMapper.Map(balances);
        }

        [HttpPost]
        [Route("ReconcileWithService")]
        public async Task<IHttpActionResult> ReconcileWithServiceAsync(ReconcileRequestDto request)
        {
            if (await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false))
            {
                return Ok();
            }
            var balances = MoneyBalancesMapper.Map(request.Balances);
            
            await reconciliationService.InitiateAsync(userContext, balances, request.OnDate).ConfigureAwait(false);
            
            return Ok();
        }
    }
}