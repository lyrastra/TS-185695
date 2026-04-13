using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.RentPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class RentPaymentController : ControllerBase
    {
        private readonly IRentPaymentCreator creator;
        private readonly IRentPaymentValidator validator;
        private readonly IRentPaymentReader reader;
        private readonly IRentPaymentUpdater updater;
        private readonly IRentPaymentRemover remover;


        public RentPaymentController(
            IRentPaymentCreator creator,
            IRentPaymentValidator validator,
            IRentPaymentReader reader,
            IRentPaymentUpdater updater,
            IRentPaymentRemover remover)
        {
            this.creator = creator;
            this.validator = validator;
            this.reader = reader;
            this.updater = updater;
            this.remover = remover;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<RentPaymentResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Арендный платеж" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = RentPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Арендный платеж" })]
        public async Task<IActionResult> CreateAsync(RentPaymentSaveDto saveDto)
        {
            var saveRequest = RentPaymentMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await creator.CreateAsync(saveRequest);
            var responseDto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(responseDto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Арендный платеж" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] RentPaymentSaveDto saveDto)
        {
            var saveRequest = RentPaymentMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await updater.UpdateAsync(saveRequest);
            var responseDto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Арендный платеж" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }

        [HttpDelete("DeleteListOperations")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OperationType>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Арендный платеж" })]
        public async Task<IActionResult> DeleteListOperationsAsync([FromBody] IReadOnlyCollection<long> ids)
        {
            await remover.DeleteByBaseIdsAsync(ids);
            return Ok();
        }
    }
}