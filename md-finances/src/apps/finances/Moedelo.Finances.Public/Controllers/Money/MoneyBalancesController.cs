using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Public.ResponseData;
using Moedelo.Finances.Public.Mappers.Money;
using Moedelo.Finances.Public.ClientData.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Public.Controllers
{
    [RoutePrefix("api/v1")]
    public class MoneyBalancesController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneySourceReader sourceReader;
        private readonly IMoneyBalancesReader balanceReader;

        public MoneyBalancesController(
            IUserContext userContext,
            IMoneySourceReader sourceReader,
            IMoneyBalancesReader balanceReader
            )
        {
            this.userContext = userContext;
            this.sourceReader = sourceReader;
            this.balanceReader = balanceReader;        }


        /// <summary>
        /// Возращает баланс на указанную дату
        /// </summary>
        [HttpPost]
        [Route("Money/Balances")]
        [SwaggerOperation(Tags = new[] { "Финансы" })]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MoneySourceBalanceDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Счет(кошелек, касса) с данным идентификатором не найден(а)")]
        public async Task<IHttpActionResult> GetAsync(BalanceRequestDto request)
        {
            if (request.Sources.Count == 0)
            {
                return Data(new List<MoneySourceBalanceDto>());
            }
            var sourcesBase = MoneyBalancesMapper.Map(request.Sources);
            var sources = await sourceReader.GetAsync(userContext, sourcesBase).ConfigureAwait(false);
            if (sources.Count == 0)
            {
                switch (request.Sources[0].Type)
                {
                    case MoneySourceType.SettlementAccount:
                        return NotFound("Расчетный счет с данным идентификатором не найден");
                    case MoneySourceType.Cash:
                        return NotFound("Касса с данным идентификатором не найдена");
                    case MoneySourceType.Purse:
                        return NotFound("Кошелек с данным идентификатором не найден");
                    default:
                        return NotFound("Неправильный Type: Расчетный счет = 1, Касса = 2, Кошелек = 3");
                }
            }

            var balances = await balanceReader.GetAsync(userContext, sources, request.OnDate).ConfigureAwait(false);
            return Data(MoneyBalancesMapper.Map(balances));
        }
    }
}