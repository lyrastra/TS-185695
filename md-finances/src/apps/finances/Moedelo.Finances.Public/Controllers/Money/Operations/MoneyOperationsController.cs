using System.Collections.Generic;
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
    public class MoneyOperationsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyOperationReader operationReader;
        private readonly IMoneyOperationRemover operationRemover;

        public MoneyOperationsController(
            IUserContext userContext,
            IMoneyOperationReader operationReader,
            IMoneyOperationRemover operationRemover)
        {
            this.userContext = userContext;
            this.operationReader = operationReader;
            this.operationRemover = operationRemover;
        }

        [HttpGet]
        [Route("{documentBaseId:long}")]
        public async Task<IHttpActionResult> GetByIdAsync(long documentBaseId)
        {
            var operation = await operationReader.GetByBaseIdAsync(userContext.FirmId, documentBaseId).ConfigureAwait(false);
            return Data(operation.MapToBase());
        }

        [HttpDelete]
        [Route("{documentBaseId:long}")]
        public async Task<IHttpActionResult> DeleteByIdAsync(long documentBaseId)
        {
            await operationRemover.DeleteByIdAsync(userContext.FirmId, userContext.UserId, documentBaseId).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> DeleteAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await operationRemover.DeleteAsync(userContext.FirmId, userContext.UserId, documentBaseIds).ConfigureAwait(false);
            return NoContent();
        }
    }
}