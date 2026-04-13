using System.Threading;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Public.Mappers.Money;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Moedelo.Finances.Public.Controllers.Money
{
    [RoutePrefix("Money")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MoneySourcesController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneySourceReader moneySourceReader;

        public MoneySourcesController(
            IUserContext userContext,
            IMoneySourceReader moneySourceReader)
        {
            this.userContext = userContext;
            this.moneySourceReader = moneySourceReader;
        }

        [HttpGet]
        [Route("Sources")]
        public async Task<IHttpActionResult> GetSourcesAsync(CancellationToken cancellationToken)
        {
            var host = Request.RequestUri.Host;
            var sources = await moneySourceReader
                .GetAsync(userContext, cancellationToken)
                .ConfigureAwait(false);
            return Data(sources.MapToClient(host));
        }
    }
}