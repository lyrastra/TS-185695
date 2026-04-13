using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.UnifiedBudgetaryPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/SubPayments")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class UnifiedBudgetarySubPaymentPrivateController(
        IUnifiedBudgetarySubPaymentReader unifiedBudgetarySubPaymentReader) : ControllerBase
    {
        [HttpGet("{documentBaseId:long}/ParentId")]
        public async Task<IActionResult> GetParentIdByBaseIdAsync(long documentBaseId)
        {
            var parentId = await unifiedBudgetarySubPaymentReader.GetParentIdByBaseIdAsync(documentBaseId);
            return new ApiDataResult(parentId);
        }

        [HttpPost("GetByParentIds")]
        public async Task<IActionResult> GetByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await unifiedBudgetarySubPaymentReader.GetByByParentIdsAsync(documentBaseIds);
            var result = response.Select(UnifiedBudgetaryPaymentMapper.MapToPrivateDto).ToArray();
            return new ApiDataResult(result);
        }


        /// <summary>
        /// Возвращает дочерние ЕНП платежи по списку идентификаторов
        /// </summary>
        [HttpPost("GetByBaseIds")]
        public async Task<IActionResult> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken ct)
        {
            var response = await unifiedBudgetarySubPaymentReader.GetByBaseIdsAsync(documentBaseIds, ct);
            var result = response.Select(UnifiedBudgetaryPaymentMapper.MapToPrivateDto).ToArray();
            return new ApiDataResult(result);
        }
    }
}