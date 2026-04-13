using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CurrencyPaymentToSupplierController : ControllerBase
    {
        private readonly ICurrencyPaymentToSupplierValidator validator;
        private readonly ICurrencyPaymentToSupplierReader reader;
        private readonly ICurrencyPaymentToSupplierCreator creator;
        private readonly ICurrencyPaymentToSupplierUpdater updater;
        private readonly ICurrencyPaymentToSupplierRemover remover;

        public CurrencyPaymentToSupplierController(
            ICurrencyPaymentToSupplierValidator validator,
            ICurrencyPaymentToSupplierReader reader,
            ICurrencyPaymentToSupplierCreator creator,
            ICurrencyPaymentToSupplierUpdater updater,
            ICurrencyPaymentToSupplierRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CurrencyPaymentToSupplierResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику в валюте" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = CurrencyPaymentToSupplierMapper.Map(model);
            return new ApiDataResult(dto);
        }
        
        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику в валюте" })]
        public async Task<IActionResult> CreateAsync(CurrencyPaymentToSupplierSaveDto saveDto)
        {
            var saveRequest = CurrencyPaymentToSupplierMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику в валюте" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, CurrencyPaymentToSupplierSaveDto saveDto)
        {
            var saveRequest = CurrencyPaymentToSupplierMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Оплата поставщику в валюте" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}