using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/CurrencySale")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class IncomingCurrencySaleController : ControllerBase
    {
        private readonly IIncomingCurrencySaleValidator validator;
        private readonly IIncomingCurrencySaleCreator creator;
        private readonly IIncomingCurrencySaleReader reader;
        private readonly IIncomingCurrencySaleUpdater updater;
        private readonly IIncomingCurrencySaleRemover remover;

        public IncomingCurrencySaleController(
            IIncomingCurrencySaleValidator validator,
            IIncomingCurrencySaleCreator creator,
            IIncomingCurrencySaleReader reader,
            IIncomingCurrencySaleUpdater updater,
            IIncomingCurrencySaleRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<IncomingCurrencySaleDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление от продажи валюты" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = IncomingCurrencySaleMapper.Map(model);
            return new ApiDataResult(dto);
        }
        
        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление от продажи валюты" })]
        public async Task<IActionResult> CreateAsync(IncomingCurrencySaleSaveDto saveDto)
        {
            var saveRequest = IncomingCurrencySaleMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление от продажи валюты" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, IncomingCurrencySaleSaveDto saveDto)
        {
            var saveRequest = IncomingCurrencySaleMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление от продажи валюты" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}