using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Dto.Money.Table;

namespace Moedelo.Finances.Api.Controllers.Money
{
    [RoutePrefix("Money/UnrecognizedPayments")]
    [WebApiRejectUnauthorizedRequest]
    public class MoneyUnrecognizedPaymentsController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyTableReader moneyTableReader;

        public MoneyUnrecognizedPaymentsController(
            IUserContext userContext,
            IMoneyTableReader moneyTableReader)
        {
            this.userContext = userContext;
            this.moneyTableReader = moneyTableReader;
        }

        [HttpPost, Route("GetUnrecognizedTable")]
        public async Task<UnrecognizedMoneyTableResponseDto> GetUnrecognizedTableAsync([FromUri] MoneyTableRequestDto clientData, CancellationToken cancellationToken)
        {
            var request = clientData.MapTableRequestToDomain();
            var response = await moneyTableReader.GetUnrecognizedAsync(userContext, request, cancellationToken).ConfigureAwait(false);
            return response.MapUnrecognizedTableResponseToClient();
        }

        [HttpGet, Route("Count")]
        public async Task<int> CountAsync(MoneySourceType? sourceType = null, long? sourceId = null, CancellationToken cancellationToken = default)
        {
            var count = await moneyTableReader
                .GetUnrecognizedOperationsCountAsync(userContext, sourceType ?? MoneySourceType.All, sourceId, cancellationToken)
                .ConfigureAwait(false);
            
            return count;
        }
    }
}