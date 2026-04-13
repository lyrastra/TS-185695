using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/CurrencyOther")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CurrencyOtherOutgoingController : ControllerBase
    {
        private readonly ICurrencyOtherOutgoingValidator validator;
        private readonly ICurrencyOtherOutgoingReader reader;
        private readonly ICurrencyOtherOutgoingCreator creator;
        private readonly ICurrencyOtherOutgoingUpdater updater;
        private readonly ICurrencyOtherOutgoingRemover remover;

        public CurrencyOtherOutgoingController(
            ICurrencyOtherOutgoingValidator validator, 
            ICurrencyOtherOutgoingCreator creator, 
            ICurrencyOtherOutgoingUpdater updater, 
            ICurrencyOtherOutgoingRemover remover, 
            ICurrencyOtherOutgoingReader reader)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CurrencyOtherOutgoingResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Прочее валютное списание" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            return new ApiDataResult(CurrencyOtherOutgoingMapper.Map(model));
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Прочее валютное списание" })]
        public async Task<IActionResult> CreateAsync(CurrencyOtherOutgoingSaveDto saveDto)
        {
            var saveRequest = CurrencyOtherOutgoingMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Прочее валютное списание" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, CurrencyOtherOutgoingSaveDto saveDto)
        {
            var saveRequest = CurrencyOtherOutgoingMapper.Map(saveDto);
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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Прочее валютное списание" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}