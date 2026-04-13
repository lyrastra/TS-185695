using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class WithdrawalFromAccountController : ControllerBase
    {
        private readonly IWithdrawalFromAccountValidator validator;
        private readonly IWithdrawalFromAccountReader reader;
        private readonly IWithdrawalFromAccountCreator creator;
        private readonly IWithdrawalFromAccountUpdater updater;
        private readonly IWithdrawalFromAccountRemover remover;

        public WithdrawalFromAccountController(
            IWithdrawalFromAccountValidator validator,
            IWithdrawalFromAccountReader reader,
            IWithdrawalFromAccountCreator creator,
            IWithdrawalFromAccountUpdater updater,
            IWithdrawalFromAccountRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<WithdrawalFromAccountResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Снятие с р/сч" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = WithdrawalFromAccountMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Снятие с р/сч" })]
        public async Task<IActionResult> CreateAsync(WithdrawalFromAccountSaveDto saveDto)
        {
            var request = WithdrawalFromAccountMapper.Map(saveDto);
            await validator.ValidateAsync(request).ConfigureAwait(false);
            var response = await creator.CreateAsync(request).ConfigureAwait(false);
            var responseDto = PaymentOrderMapper.MapToResponse(response);
            return new ApiDataResult(responseDto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Снятие с р/сч" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, WithdrawalFromAccountSaveDto saveDto)
        {
            var request = WithdrawalFromAccountMapper.Map(saveDto);
            request.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(request).ConfigureAwait(false);
            var response = await updater.UpdateAsync(request).ConfigureAwait(false);
            var responseDto = PaymentOrderMapper.MapToResponse(response);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Снятие с р/сч" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}