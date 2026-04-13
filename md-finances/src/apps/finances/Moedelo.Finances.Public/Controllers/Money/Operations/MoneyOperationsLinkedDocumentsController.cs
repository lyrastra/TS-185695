using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Public.Mappers.Money;

namespace Moedelo.Finances.Public.Controllers.Money.Operations
{
    [RoutePrefix("Money/Operations")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MoneyOperationsLinkedDocumentsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyOperationLinkedDocumentsReader linkedDocumentsReader;

        public MoneyOperationsLinkedDocumentsController(
            IUserContext userContext,
            IMoneyOperationLinkedDocumentsReader linkedDocumentsReader)
        {
            this.userContext = userContext;
            this.linkedDocumentsReader = linkedDocumentsReader;
        }

        [HttpGet]
        [Route("{documentBaseId:long}/LinkedDocuments")]
        public async Task<IHttpActionResult> GetLinkedDocumentsAsync(long documentBaseId)
        {
            var respone = await linkedDocumentsReader.GetByBaseIdAsync(userContext, documentBaseId).ConfigureAwait(false);
            return Data(MoneyOperationLinkedDocumentsMapper.MapLinkedDocuments(respone));
        }
    }
}