using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Public.ClientData;
using Moedelo.Finances.Public.ClientData.Money.Table;
using Moedelo.Finances.Public.ClientData.Money.Table.Main;
using Moedelo.Finances.Public.ClientData.Money.Table.OutsourceProcessing;
using Moedelo.Finances.Public.Mappers.Money;

namespace Moedelo.Finances.Public.Controllers.Money
{
    [RoutePrefix("Money/Table")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MoneyTableController(IUserContext userContext,
        IMoneyTableReader moneyTableReader,
        IPaymentOrderOperationUpdater paymentOrderOperationUpdater,
        IPaymentOrderOperationRemover paymentOrderOperationRemover) : BaseApiController
    {
        [HttpGet]
        [Route("Warning/Count")]
        public async Task<IHttpActionResult> GetUnrecognizedOperationsCountAsync(MoneySourceType? sourceType = null, long? sourceId = null, CancellationToken cancellationToken = default)
        {
            var count = await moneyTableReader
                .GetUnrecognizedOperationsCountAsync(userContext, sourceType ?? MoneySourceType.All, sourceId, cancellationToken)
                .ConfigureAwait(false);
            return Data(count);
        }

        [HttpGet, Route("Warning")]
        public async Task<IHttpActionResult> GetUnrecognizedTableAsync([FromUri] MoneyTableRequestClientData clientData,
            CancellationToken cancellationToken)
        {
            var request = clientData.MapTableRequestToDomain();
            var response = await moneyTableReader.GetUnrecognizedAsync(userContext, request, cancellationToken).ConfigureAwait(false);
            return Data(response.MapUnrecognizedTableResponseToClient());
        }

        [HttpDelete, Route("Warning")]
        public async Task<IHttpActionResult> DeleteAsync(SourceClientData clientData)
        {
            if (clientData.SourceType != MoneySourceType.All &&
                clientData.SourceType != MoneySourceType.SettlementAccount)
            {
                return NoContent();
            }
            await paymentOrderOperationRemover.DeleteUncategorizedAsync(userContext.FirmId, userContext.UserId, clientData.SourceId).ConfigureAwait(false);
            return NoContent();
        }

        [HttpGet]
        [Route("Success/Count")]
        public async Task<IHttpActionResult> GetImportedOperationsCountAsync(MoneySourceType? sourceType = null, long? sourceId = null, CancellationToken cancellationToken = default)
        {
            var count = await moneyTableReader
                .GetImportedCountAsync(userContext, sourceType ?? MoneySourceType.All, sourceId, cancellationToken)
                .ConfigureAwait(false);
            return Data(count);
        }

        [HttpGet]
        [Route("Success")]
        public async Task<IHttpActionResult> GetImportedTableAsync([FromUri] MoneyTableRequestClientData clientData, CancellationToken cancellationToken)
        {
            var request = clientData.MapTableRequestToDomain();
            var response = await moneyTableReader
                .GetImportedAsync(userContext, request, cancellationToken)
                .ConfigureAwait(false);

            return Data(response.MapToClient());
        }

        [HttpPost]
        [Route("Success/Approve")]
        public async Task<IHttpActionResult> ApproveAsync(SourceClientData clientData)
        {
            await paymentOrderOperationUpdater.ApproveImportedAsync(userContext.FirmId, userContext.UserId, clientData.SourceType, clientData.SourceId).ConfigureAwait(false);
            return NoContent();
        }
        
        [HttpGet, Route("OutsourceProcessing/Count")]
        public async Task<IHttpActionResult> GetOutsourceProcessingCountAsync([FromUri] OutsourceProcessingFilterClientData clientData,
            CancellationToken cancellationToken)
        {
            var request = clientData.MapOutsourceProcessingFilterToDomain();
            var count = await moneyTableReader
                .GetOutsourceProcessingCountAsync(userContext, request, cancellationToken)
                .ConfigureAwait(false);

            return Data(count);
        }

        [HttpGet, Route("OutsourceProcessing")]
        public async Task<IHttpActionResult> GetOutsourceProcessingTableAsync([FromUri] OutsourceProcessingPageFilterClientData clientData,
            CancellationToken cancellationToken)
        {
            var request = clientData.MapOutsourceProcessingPageFilterToDomain();
            var response = await moneyTableReader
                .GetOutsourceProcessingAsync(userContext, request, cancellationToken)
                .ConfigureAwait(false);

            return Data(response.MapToClient());
        }

        [HttpGet]
        [Route("MultiCurrency")]
        public async Task<IHttpActionResult> GetMultiCurrencyTableAsync(
            [FromUri] MainMoneyTableRequestClientData clientData,
            CancellationToken cancellationToken)
        {
            var request = clientData.MapMainTableRequestToDomain();

            var response = await moneyTableReader
                .GetMultiCurrencyTableAsync(userContext, request, cancellationToken)
                .ConfigureAwait(false);

            return Data(response.Map());
        }
    }
}