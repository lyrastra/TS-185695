using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CurrencyTransferToAccountController : ControllerBase
    {
        private readonly ICurrencyTransferToAccountValidator validator;
        private readonly ICurrencyTransferToAccountReader reader;
        private readonly ICurrencyTransferToAccountCreator creator;
        private readonly ICurrencyTransferToAccountUpdater updater;
        private readonly ICurrencyTransferToAccountRemover remover;

        public CurrencyTransferToAccountController(
            ICurrencyTransferToAccountValidator validator,
            ICurrencyTransferToAccountReader reader,
            ICurrencyTransferToAccountCreator creator,
            ICurrencyTransferToAccountUpdater updater,
            ICurrencyTransferToAccountRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<CurrencyTransferToAccountResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой валютный счет" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = CurrencyTransferToAccountMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой валютный счет" })]
        public async Task<IActionResult> CreateAsync(CurrencyTransferToAccountSaveDto saveDto)
        {
            var saveRequest = Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой валютный счет" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, CurrencyTransferToAccountSaveDto saveDto)
        {
            var saveRequest = Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой валютный счет" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }

        private static CurrencyTransferToAccountSaveRequest Map(CurrencyTransferToAccountSaveDto dto)
        {
            return new CurrencyTransferToAccountSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.TransferToAccount
                    : dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }
    }
}
