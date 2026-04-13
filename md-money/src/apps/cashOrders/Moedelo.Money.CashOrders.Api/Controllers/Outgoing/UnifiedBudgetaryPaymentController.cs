using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.CashOrders.Api.Mappers.CashOrders.Outgoing;
using Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class UnifiedBudgetaryPaymentController : ControllerBase
    {
        private readonly IUnifiedBudgetaryPaymentReader reader;
        private readonly IUnifiedBudgetaryPaymentCreator creator;
        private readonly IUnifiedBudgetaryPaymentUpdater updater;
        private readonly IUnifiedBudgetaryPaymentRemover remover;

        public UnifiedBudgetaryPaymentController(
            IUnifiedBudgetaryPaymentReader reader,
            IUnifiedBudgetaryPaymentCreator creator,
            IUnifiedBudgetaryPaymentUpdater updater,
            IUnifiedBudgetaryPaymentRemover remover)
        {
            this.reader = reader;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
        }

        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByDocumentBaseIdAsync(int documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var data = UnifiedBudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateAsync([FromBody] UnifiedBudgetaryPaymentDto dto)
        {
            var request = UnifiedBudgetaryPaymentMapper.Map(dto);
            var response = await creator.CreateAsync(request);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] UnifiedBudgetaryPaymentDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = UnifiedBudgetaryPaymentMapper.Map(dto);
            request.DocumentBaseId = documentBaseId;
            var deletedSubPaymentDocumentIds = await updater.UpdateAsync(request);
            var response = new UnifiedBudgetaryPaymentUpdateResponseDto
            {
                DeletedSubPaymentDocumentIds = deletedSubPaymentDocumentIds
            };
            return new ApiDataResult(response);
        }

        [HttpPost("{documentBaseId:long}/Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            var deletedSubPaymentDocumentIds = await remover.DeleteAsync(documentBaseId);
            var response = new UnifiedBudgetaryPaymentDeleteResponseDto
            {
                DeletedSubPaymentDocumentIds = deletedSubPaymentDocumentIds
            };
            return new ApiDataResult(response);
        }
    }
}
