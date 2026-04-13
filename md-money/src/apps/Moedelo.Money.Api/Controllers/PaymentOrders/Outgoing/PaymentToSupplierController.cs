using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class PaymentToSupplierController : ControllerBase
    {
        private readonly IPaymentToSupplierValidator validator;
        private readonly IPaymentToSupplierReader reader;
        private readonly IPaymentToSupplierCreator creator;
        private readonly IPaymentToSupplierUpdater updater;
        private readonly IPaymentToSupplierRemover remover;

        public PaymentToSupplierController(
            IPaymentToSupplierValidator validator,
            IPaymentToSupplierReader reader,
            IPaymentToSupplierCreator creator,
            IPaymentToSupplierUpdater updater,
            IPaymentToSupplierRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentToSupplierResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = PaymentToSupplierMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику" })]
        public async Task<IActionResult> CreateAsync(PaymentToSupplierSaveDto saveDto)
        {
            var saveRequest = PaymentToSupplierMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, PaymentToSupplierSaveDto saveDto)
        {
            var saveRequest = PaymentToSupplierMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
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
