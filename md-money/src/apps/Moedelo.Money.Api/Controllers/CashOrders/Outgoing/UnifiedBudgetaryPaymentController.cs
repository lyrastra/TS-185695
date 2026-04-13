using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.CashOrders;
using Moedelo.Money.Api.Mappers.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.CashOrders;
using Moedelo.Money.Api.Models.CashOrders.Outgoing;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.CashOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/CashOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class UnifiedBudgetaryPaymentController : ControllerBase
    {
        private readonly IUnifiedBudgetaryPaymentValidator validator;
        private readonly IUnifiedBudgetaryPaymentReader reader;
        private readonly IUnifiedBudgetaryPaymentCreator creator;
        private readonly IUnifiedBudgetaryPaymentUpdater updater;
        private readonly IUnifiedBudgetaryPaymentRemover remover;

        public UnifiedBudgetaryPaymentController(
            IUnifiedBudgetaryPaymentValidator validator,
            IUnifiedBudgetaryPaymentReader reader,
            IUnifiedBudgetaryPaymentCreator creator,
            IUnifiedBudgetaryPaymentUpdater updater,
            IUnifiedBudgetaryPaymentRemover remover)
        {
            this.validator = validator;
            this.reader = reader;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<UnifiedBudgetaryPaymentResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса/Списания - Единый налоговый платеж (ЕНП)" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = UnifiedBudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<CashOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса/Списания - Единый налоговый платеж (ЕНП)" })]
        public async Task<IActionResult> CreateAsync(UnifiedBudgetaryPaymentSaveDto saveDto)
        {
            var saveRequest = UnifiedBudgetaryPaymentMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest).ConfigureAwait(false);
            var saveResponse = await creator.CreateAsync(saveRequest).ConfigureAwait(false);
            var dto = CashOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(dto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CashOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса/Списания - Единый налоговый платеж (ЕНП)" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, UnifiedBudgetaryPaymentSaveDto saveDto)
        {
            var saveRequest = UnifiedBudgetaryPaymentMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest).ConfigureAwait(false);
            var saveResponse = await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
            var dto = CashOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса/Списания - Единый налоговый платеж (ЕНП)" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}