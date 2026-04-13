using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CurrencyTransferFromAccountController : ControllerBase
    {
        private readonly ICurrencyTransferFromAccountValidator validator;
        private readonly ICurrencyTransferFromAccountReader reader;
        private readonly ICurrencyTransferFromAccountCreator creator;
        private readonly ICurrencyTransferFromAccountUpdater updater;
        private readonly ICurrencyTransferFromAccountRemover remover;

        public CurrencyTransferFromAccountController(
            ICurrencyTransferFromAccountValidator validator,
            ICurrencyTransferFromAccountReader reader,
            ICurrencyTransferFromAccountCreator creator,
            ICurrencyTransferFromAccountUpdater updater,
            ICurrencyTransferFromAccountRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CurrencyTransferFromAccountResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Перевод с валютного счета" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = CurrencyTransferFromAccountMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Перевод с валютного счета" })]
        public async Task<IActionResult> CreateAsync(CurrencyTransferFromAccountSaveDto saveDto)
        {
            var saveRequest = Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Перевод с валютного счета" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, CurrencyTransferFromAccountSaveDto saveDto)
        {
            var saveRequest = Map(saveDto);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Перевод с валютного счета" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }

        private static CurrencyTransferFromAccountSaveRequest Map(CurrencyTransferFromAccountSaveDto dto)
        {
            return new CurrencyTransferFromAccountSaveRequest
            {
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                ProvideInAccounting = dto.ProvideInAccounting ?? true
            };
        }
    }
}