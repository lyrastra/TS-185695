using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/CurrencyOther")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CurrencyOtherIncomingController : ControllerBase
    {
        private readonly ICurrencyOtherIncomingValidator validator;
        private readonly ICurrencyOtherIncomingReader reader;
        private readonly ICurrencyOtherIncomingCreator creator;
        private readonly ICurrencyOtherIncomingUpdater updater;
        private readonly ICurrencyOtherIncomingRemover remover;

        public CurrencyOtherIncomingController(
            ICurrencyOtherIncomingValidator validator, 
            ICurrencyOtherIncomingCreator creator, 
            ICurrencyOtherIncomingUpdater updater, 
            ICurrencyOtherIncomingRemover remover, 
            ICurrencyOtherIncomingReader reader)
        {
            this.validator = validator;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
            this.reader = reader;
        }
        
        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CurrencyOtherIncomingResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Прочее валютное поступление" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            return new ApiDataResult(CurrencyOtherIncomingMapper.Map(model));
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Прочее валютное поступление" })]
        public async Task<IActionResult> CreateAsync(CurrencyOtherIncomingSaveDto saveDto)
        {
            var saveRequest = CurrencyOtherIncomingMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest).ConfigureAwait(false);
            var saveResponse = await creator.CreateAsync(saveRequest).ConfigureAwait(false);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Прочее валютное поступление" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, CurrencyOtherIncomingSaveDto saveDto)
        {
            var saveRequest = CurrencyOtherIncomingMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest).ConfigureAwait(false);
            var saveResponse = await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
            var dto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Прочее валютное поступление" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}