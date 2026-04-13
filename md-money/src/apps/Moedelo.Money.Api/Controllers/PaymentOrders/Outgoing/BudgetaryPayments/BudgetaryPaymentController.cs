using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.BudgetaryPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class BudgetaryPaymentController : ControllerBase
    {
        private readonly IBudgetaryPaymentValidator validator;
        private readonly IBudgetaryPaymentReader reader;
        private readonly IBudgetaryPaymentCreator creator;
        private readonly IBudgetaryPaymentUpdater updater;
        private readonly IBudgetaryPaymentRemover remover;
        private readonly IBudgetaryPaymentMetadataReader metadataReader;
        private readonly IBudgetaryPaymentKbkDefaultsReader kbkDefaultsReader;
        private readonly IBudgetaryPaymentKbkService kbkAutocompleteService;

        public BudgetaryPaymentController(
            IBudgetaryPaymentValidator validator,
            IBudgetaryPaymentReader reader,
            IBudgetaryPaymentCreator creator,
            IBudgetaryPaymentUpdater updater,
            IBudgetaryPaymentRemover remover,
            IBudgetaryPaymentMetadataReader metadataReader,
            IBudgetaryPaymentKbkDefaultsReader kbkDefaultsReader,
            IBudgetaryPaymentKbkService kbkAutocompleteService)
        {
            this.validator = validator;
            this.reader = reader;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
            this.metadataReader = metadataReader;
            this.kbkDefaultsReader = kbkDefaultsReader;
            this.kbkAutocompleteService = kbkAutocompleteService;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryPaymentResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = BudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Получение операции для копирования
        /// </summary>
        [HttpGet("{documentBaseId:long}/copy")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryPaymentResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> GetCopyAsync(long documentBaseId)
        {
            var model = await reader.GetCopyByBaseIdAsync(documentBaseId);
            var dto = BudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> CreateAsync(BudgetaryPaymentSaveDto saveDto)
        {
            var saveRequest = BudgetaryPaymentMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, BudgetaryPaymentSaveDto saveDto)
        {
            var saveRequest = BudgetaryPaymentMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }

        /// <summary>
        /// Метаданные для предзаполнения операции
        /// </summary>
        [HttpGet("Metadata")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryPaymentMetadataDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> GetMetadataAsync(DateTime? paymentDate = null)
        {
            var model = await metadataReader.GetAsync(paymentDate ?? DateTime.Today);
            var dto = BudgetaryPaymentMapper.MapToDto(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Получение данных для заполнения полей по КБК
        /// </summary>
        [HttpPost("DefaultFieldsByKbk")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryKbkDefaultsDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> GetDefaultFieldsByKbkAsync(BudgetaryKbkDefaultsRequestDto requestDto)
        {
            var request = BudgetaryPaymentMapper.MapToDomain(requestDto);
            var response = await kbkDefaultsReader.GetAsync(request);
            var responseDto = BudgetaryPaymentMapper.MapToDto(response);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Автокомплит КБК
        /// </summary>
        [HttpPost("KbkAutocomplete")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryKbkAutocompleteResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж" })]
        public async Task<IActionResult> KbkAutocompleteAsync(BudgetaryKbkAutocompleteRequestDto requestDto)
        {
            var request = BudgetaryPaymentMapper.MapToDomain(requestDto);
            var response = await kbkAutocompleteService.KbkAutocompleteAsync(request);
            var responseDto = BudgetaryPaymentMapper.MapToDto(response);
            return new ApiDataResult(responseDto);
        }
    }
}