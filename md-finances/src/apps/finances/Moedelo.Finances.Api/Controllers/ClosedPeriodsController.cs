using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.ClosedPeriods;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;
using Moedelo.Finances.Dto.ClosedPeriods;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("ClosedPeriods")]
    [WebApiRejectUnauthorizedRequest]
    public class ClosedPeriodsController(
        IUserContext userContext,
        IClosedPeriodsService closedPeriodsService,
        IMoneyClosedPeriodsDataReader reader) : ApiController
    {
        [HttpGet]
        [Route("LastClosedDate")]
        public async Task<string> GetLastClosedDateAsync(CancellationToken cancellationToken)
        {
            var result = await closedPeriodsService
                .GetLastClosedDateAsync(userContext, cancellationToken)
                .ConfigureAwait(false);
            return JsonConvert.SerializeObject(result, new IsoDateConverter());
        }

        [HttpGet]
        [Route("CountOrdersNonProvidedInAccounting")]
        public async Task<List<MoneyDocumentsCountDto>> CountOrdersNonProvidedInAccounting(
            DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var result = await reader
                .GetNonProvidedInAccountingOrderCountsAsync(userContext, startDate, endDate, cancellationToken)
                .ConfigureAwait(false);
            return MoneyClosedPeriodsDataMapper.Map(result);
        }
        
        [HttpGet]
        [Route("OrdersNonProvidedInAccounting")]
        public async Task<List<MoneyDocumentsCountDto>> OrdersNonProvidedInAccounting(DateTime startDate,
            DateTime endDate, CancellationToken cancellationToken)
        {
            var result = await reader
                .GetNonProvidedInAccountingOrdersAsync(userContext, startDate, endDate, cancellationToken)
                .ConfigureAwait(false);

            return MoneyClosedPeriodsDataMapper.Map(result);
        }
    }
}