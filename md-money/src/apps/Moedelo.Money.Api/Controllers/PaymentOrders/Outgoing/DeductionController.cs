using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class DeductionController : ControllerBase
    {
        private readonly IDeductionValidator validator;
        private readonly IDeductionReader reader;
        private readonly IDeductionCreator creator;
        private readonly IDeductionUpdater updater;
        private readonly IDeductionRemover remover;

        public DeductionController(
            IDeductionValidator validator,
            IDeductionReader reader,
            IDeductionCreator creator,
            IDeductionUpdater updater,
            IDeductionRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<DeductionResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплаты удержаний" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = DeductionMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплаты удержаний" })]
        public async Task<IActionResult> CreateAsync(DeductionSaveDto saveDto)
        {
            var saveRequest = DeductionMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await creator.CreateAsync(saveRequest);
            var dto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(dto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплаты удержаний" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, DeductionSaveDto saveDto)
        {
            var saveRequest = DeductionMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await updater.UpdateAsync(saveRequest);
            var dto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплаты удержаний" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }
    }
}
