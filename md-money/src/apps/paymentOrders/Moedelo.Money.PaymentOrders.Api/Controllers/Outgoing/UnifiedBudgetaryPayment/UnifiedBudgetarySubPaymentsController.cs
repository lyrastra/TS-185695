using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Outgoing.UnifiedBudgetaryPayment
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/UnifiedBudgetaryPayment/SubPayments")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UnifiedBudgetarySubPaymentsController(
        IUnifiedTaxPaymentReader unifiedTaxPaymentReader,
        IUnifiedTaxPaymentUpdater unifiedTaxPaymentUpdater) : ControllerBase
    {

        /// <summary>
        /// Возвращает дочерние ЕНП платежи по списку идентификаторов
        /// </summary>
        [HttpPost("GetByBaseIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds, CancellationToken ct)
        {
            var payments = await unifiedTaxPaymentReader.GetByBaseIdsAsync(baseIds, ct);
            return new ApiDataResult(payments.Select(UnifiedBudgetaryPaymentMapper.MapSubPayment));
        }

        [HttpPost("GetByParentIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByParentsBaseIdAsync(IReadOnlyCollection<long> parentsBaseIds)
        {
            var payments = await unifiedTaxPaymentReader.GetByParentBaseIdsAsync(parentsBaseIds);
            return new ApiDataResult(payments.Select(UnifiedBudgetaryPaymentMapper.MapSubPayment));
        }

        [HttpGet("{documentBaseId:long}/ParentId")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetParentIdByBaseIdAsync(long documentBaseId)
        {
            var subPayment = await unifiedTaxPaymentReader.GetByBaseIdAsync(documentBaseId);
            return new ApiDataResult(new ParentIdDto { ParentId = subPayment.ParentDocumentId });
        }

        [HttpPut("{documentBaseId:long}/TaxPostingType")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTaxPostingTypeAsync(
            long documentBaseId,
            [FromBody] UpdateTaxPostingTypeRequestDto request)
        {
            await unifiedTaxPaymentUpdater.SetTaxPostingTypeAsync(documentBaseId, request.TaxPostingType);
            return Ok();
        }
    }
}