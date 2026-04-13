using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CurrencyPaymentFromCustomerController : ControllerBase
    {
        private readonly ICurrencyPaymentFromCustomerValidator validator;
        private readonly ICurrencyPaymentFromCustomerReader reader;
        private readonly ICurrencyPaymentFromCustomerCreator creator;
        private readonly ICurrencyPaymentFromCustomerUpdater updater;
        private readonly ICurrencyPaymentFromCustomerRemover remover;

        public CurrencyPaymentFromCustomerController(
            ICurrencyPaymentFromCustomerValidator validator,
            ICurrencyPaymentFromCustomerReader reader,
            ICurrencyPaymentFromCustomerCreator creator,
            ICurrencyPaymentFromCustomerUpdater updater,
            ICurrencyPaymentFromCustomerRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CurrencyPaymentFromCustomerResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Валютная оплата от покупателя" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = CurrencyPaymentFromCustomerMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Валютная оплата от покупателя" })]
        public async Task<IActionResult> CreateAsync(CurrencyPaymentFromCustomerSaveDto saveDto)
        {
            var saveRequest = CurrencyPaymentFromCustomerMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Валютная оплата от покупателя" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, CurrencyPaymentFromCustomerSaveDto saveDto)
        {
            var saveRequest = CurrencyPaymentFromCustomerMapper.Map(saveDto);
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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Валютная оплата от покупателя" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}