using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/CurrencyPurchase")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class OutgoingCurrencyPurchaseController : ControllerBase
    {
        private readonly IOutgoingCurrencyPurchaseValidator validator;
        private readonly IOutgoingCurrencyPurchaseCreator creator;
        private readonly IOutgoingCurrencyPurchaseReader reader;
        private readonly IOutgoingCurrencyPurchaseUpdater updater;
        private readonly IOutgoingCurrencyPurchaseRemover remover;

        public OutgoingCurrencyPurchaseController(
            IOutgoingCurrencyPurchaseValidator validator,
            IOutgoingCurrencyPurchaseCreator creator,
            IOutgoingCurrencyPurchaseReader reader,
            IOutgoingCurrencyPurchaseUpdater updater,
            IOutgoingCurrencyPurchaseRemover remover)
        {
            this.validator = validator;
            this.creator = creator;
            this.reader = reader;
            this.updater = updater;
            this.remover = remover;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OutgoingCurrencyPurchaseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Покупка валюты" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = OutgoingCurrencyPurchaseMapper.Map(model);
            return new ApiDataResult(dto);
        }
        
        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Покупка валюты" })]
        public async Task<IActionResult> CreateAsync(OutgoingCurrencyPurchaseSaveDto saveDto)
        {
            var saveRequest = OutgoingCurrencyPurchaseMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Покупка валюты" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, OutgoingCurrencyPurchaseSaveDto saveDto)
        {
            var saveRequest = OutgoingCurrencyPurchaseMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Покупка валюты" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}