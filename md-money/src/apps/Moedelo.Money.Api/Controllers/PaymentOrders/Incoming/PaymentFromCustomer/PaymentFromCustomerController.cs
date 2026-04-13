using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming.PaymentFromCustomer
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class PaymentFromCustomerController : ControllerBase
    {
        private readonly IPaymentFromCustomerValidator validator;
        private readonly IPaymentFromCustomerReader reader;
        private readonly IPaymentFromCustomerCreator creator;
        private readonly IPaymentFromCustomerUpdater updater;
        private readonly IPaymentFromCustomerRemover remover;

        public PaymentFromCustomerController(
            IPaymentFromCustomerValidator validator,
            IPaymentFromCustomerReader reader,
            IPaymentFromCustomerCreator creator,
            IPaymentFromCustomerUpdater updater,
            IPaymentFromCustomerRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentFromCustomerResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Оплата от покупателя" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = PaymentFromCustomerMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Оплата от покупателя" })]
        public async Task<IActionResult> CreateAsync(PaymentFromCustomerSaveDto saveDto)
        {
            var saveRequest = PaymentFromCustomerMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest).ConfigureAwait(false);
            var saveResponse = await creator.CreateAsync(saveRequest).ConfigureAwait(false);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Оплата от покупателя" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, PaymentFromCustomerSaveDto saveDto)
        {
            var saveRequest = PaymentFromCustomerMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest).ConfigureAwait(false);
            var saveResponse = await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
            var responseDto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Оплата от покупателя" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Устанавка значения "Резерва"
        /// </summary>
        [HttpPost("{documentBaseId:long}/SetReserve")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetReserveAsync(long documentBaseId, SetReserveDto dto)
        {
            var request = new SetReserveRequest { DocumentBaseId = documentBaseId, ReserveSum = dto.ReserveSum };
            await validator.ValidateAsync(request);
            await updater.SetReserveAsync(request);
            return Ok();
        }
    }
}
