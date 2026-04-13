using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Public.Mappers.Money;

namespace Moedelo.Finances.Public.Controllers.Operations
{
    [RoutePrefix("Money/Operations")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MoneyOperationsPostingsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyOperationAccountingPostingsReader accountingPostingsReader;
        private readonly IMoneyOperationTaxPostingsReader taxPostingsReader;

        public MoneyOperationsPostingsController(
            IUserContext userContext,
            IMoneyOperationAccountingPostingsReader accountingPostingsReader,
            IMoneyOperationTaxPostingsReader taxPostingsReader)
        {
            this.userContext = userContext;
            this.accountingPostingsReader = accountingPostingsReader;
            this.taxPostingsReader = taxPostingsReader;
        }

        [HttpGet]
        [Route("{documentBaseId:long}/AccountingPostings")]
        public async Task<IHttpActionResult> GetAccountingPostingsAsync(long documentBaseId)
        {
            var response = await accountingPostingsReader.GetByBaseIdAsync(userContext, documentBaseId).ConfigureAwait(false);
            return Data(MoneyOperationAccountingPostingsMapper.MapAccountingPostingsResponse(response));
        }

        [HttpGet]
        [Route("{documentBaseId:long}/TaxPostings")]
        public async Task<IHttpActionResult> GetTaxPostingsAsync(long documentBaseId)
        {
            var response = await taxPostingsReader.GetByBaseIdAsync(userContext, documentBaseId).ConfigureAwait(false);
            return Data(MoneyOperationTaxPostingsMapper.MapTaxPostingsResponse(response));
        }
    }
}