using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Duplicates;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Duplicates")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class PaymentOrdersDuplicatesController : ControllerBase
    {
        private readonly IPaymentOrderDuplicateMerger duplicateMerger;
        private readonly IPaymentOrderDuplicateImporter duplicateImporter;

        public PaymentOrdersDuplicatesController(
            IPaymentOrderDuplicateMerger duplicateMerger,
            IPaymentOrderDuplicateImporter duplicateImporter)
        {
            this.duplicateMerger = duplicateMerger;
            this.duplicateImporter = duplicateImporter;
        }

        /// <summary>
        /// Обновить дату операции в сервисе (не импортировать дубликат)
        /// </summary>
        [HttpPost("{documentBaseId:long}/Merge")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<object>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        public async Task<IActionResult> MergeAsync(long documentBaseId)
        {
            await duplicateMerger.MergeAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Импортировать дубликат в сервис (не дубликат)
        /// </summary>
        [HttpPost("{documentBaseId:long}/Import")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<object>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        public async Task<IActionResult> ImportAsync(long documentBaseId)
        {
            await duplicateImporter.ImportAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}
