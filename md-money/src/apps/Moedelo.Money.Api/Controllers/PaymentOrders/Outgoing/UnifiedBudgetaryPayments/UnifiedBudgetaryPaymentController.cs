using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.UnifiedBudgetaryPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    public class UnifiedBudgetaryPaymentController : ControllerBase
    {
        private readonly IUnifiedBudgetaryPaymentValidator validator;
        private readonly IUnifiedBudgetaryPaymentReader reader;
        private readonly IUnifiedBudgetaryPaymentCreator creator;
        private readonly IUnifiedBudgetaryPaymentUpdater updater;
        private readonly IUnifiedBudgetaryPaymentRemover remover;
        private readonly IUnifiedBudgetaryPaymentAccountsReader accountsCodeReader;
        private readonly IUnifiedBudgetaryPaymentKbkService kbkAutocompleteService;
        private readonly IUnifiedBudgetaryPaymentMetadataReader metadataReader;
        private readonly IUnifiedBudgetaryPaymentKbkDefaultsReader kbkDefaultsReader;
        private readonly IUnifiedBudgetaryPaymentDescriptor descriptor;

        public UnifiedBudgetaryPaymentController(
            IUnifiedBudgetaryPaymentValidator validator,
            IUnifiedBudgetaryPaymentReader reader,
            IUnifiedBudgetaryPaymentCreator creator,
            IUnifiedBudgetaryPaymentUpdater updater,
            IUnifiedBudgetaryPaymentRemover remover,
            IUnifiedBudgetaryPaymentAccountsReader accountsCodeReader,
            IUnifiedBudgetaryPaymentKbkService kbkAutocompleteService,
            IUnifiedBudgetaryPaymentMetadataReader metadataReader,
            IUnifiedBudgetaryPaymentKbkDefaultsReader kbkDefaultsReader,
            IUnifiedBudgetaryPaymentDescriptor descriptor)
        {
            this.validator = validator;
            this.reader = reader;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
            this.remover = remover;
            this.accountsCodeReader = accountsCodeReader;
            this.kbkAutocompleteService = kbkAutocompleteService;
            this.metadataReader = metadataReader;
            this.kbkDefaultsReader = kbkDefaultsReader;
            this.descriptor = descriptor;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<UnifiedBudgetaryPaymentResponseDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = UnifiedBudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Получение операции для копирования
        /// </summary>
        [HttpGet("{documentBaseId:long}/copy")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryPaymentResponseDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> GetCopyAsync(long documentBaseId)
        {
            var model = await reader.GetCopyByBaseIdAsync(documentBaseId);
            var dto = UnifiedBudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> CreateAsync(UnifiedBudgetaryPaymentSaveDto saveDto)
        {
            var saveRequest = UnifiedBudgetaryPaymentMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest);
            saveRequest.Description = await descriptor.GetDescription(saveRequest.SubPayments.Select(s=> new UnifiedBudgetarySubPaymentForDescription
            { 
                KbkId = s.KbkId,
                Period = s.Period,
                Sum = s.Sum,
                PatentId = s.PatentId,
                TradingObjectId = s.TradingObjectId
            }).ToList(), saveRequest.Date);
            var saveResponse = await creator.CreateAsync(saveRequest);
            var dto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(dto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, UnifiedBudgetaryPaymentSaveDto saveDto)
        {
            var saveRequest = UnifiedBudgetaryPaymentMapper.Map(saveDto);
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
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }

        /// <summary>
        /// Бюджетные счета
        /// </summary>
        [HttpGet("GetAccountCodes")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<List<BudgetaryAccountDto>>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> GetAccountCodesAsync()
        {
            var result = await accountsCodeReader.GetAsync();
            return new ApiDataResult(result);
        }

        /// <summary>
        /// Автокомплит КБК
        /// </summary>
        [HttpPost("KbkAutocomplete")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryKbkAutocompleteResponseDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Единый налоговый платеж"])]
        public async Task<IActionResult> KbkAutocompleteAsync(BudgetaryKbkAutocompleteRequestDto requestDto)
        {
            var request = BudgetaryPaymentMapper.MapToDomain(requestDto);
            var response = await kbkAutocompleteService.KbkAutocompleteAsync(request);
            var responseDto = BudgetaryPaymentMapper.MapToDto(response);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Метаданные для предзаполнения операции
        /// </summary>
        [HttpGet("Metadata")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryPaymentMetadataDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Бюджетный платеж"])]
        public async Task<IActionResult> GetMetadataAsync()
        {
            var model = await metadataReader.GetAsync();
            var dto = BudgetaryPaymentMapper.MapToDto(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Получение данных для заполнения полей по КБК
        /// </summary>
        [HttpPost("DefaultFieldsByKbk")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryKbkDefaultsDto>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Бюджетный платеж"])]
        public async Task<IActionResult> GetDefaultFieldsByKbkAsync(UnifiedBudgetaryPaymentKbkDefaultsRequestDto requestDto)
        {
            var request = UnifiedBudgetaryPaymentMapper.MapToDomain(requestDto);
            var response = await kbkDefaultsReader.GetAsync(request);
            var responseDto = BudgetaryPaymentMapper.MapToDto(response);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Получить Назначение платежа по указанным подплатежам
        /// </summary>
        [HttpPost("GetDescription")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<string>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Бюджетный платеж"])]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetDescription(
            [FromBody] IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> subPaymentsDto,
            [FromQuery] DateTime paymentDate)
        {
            var subPayments = UnifiedBudgetaryPaymentMapper.MapToDomain(subPaymentsDto);
            var response = await descriptor.GetDescription(subPayments, paymentDate);
            return new ApiDataResult(response);
        }

        /// <summary>
        /// Получить подплатежи по Назначениию платежа
        /// </summary>
        [HttpPost("GetSubPayments")]
        [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<List<UnifiedBudgetarySubPaymentDto>>))]
        [SwaggerOperation(Tags = ["Деньги/Банк/Списания - Бюджетный платеж"])]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetSubPayments(string description)
        {
            var response = await descriptor.GetSubPayments(description);
            return new ApiDataResult(response);
        }
    }
}