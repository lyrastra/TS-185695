using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class TransferToAccountController : ControllerBase
    {
        private readonly ITransferToAccountValidator validator;
        private readonly ITransferToAccountReader reader;
        private readonly ITransferToAccountCreator creator;
        private readonly ITransferToAccountUpdaterExt updater;
        private readonly ITransferToAccountRemover remover;

        public TransferToAccountController(
            ITransferToAccountValidator validator,
            ITransferToAccountReader reader,
            ITransferToAccountCreator creator,
            ITransferToAccountUpdaterExt updater,
            ITransferToAccountRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<TransferToAccountResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой счет" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = TransferToAccountMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<TransferToAccountSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой счет" })]
        public async Task<IActionResult> CreateAsync(TransferToAccountSaveDto saveDto)
        {
            var saveRequest = TransferToAccountMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await creator.CreateWithIncomingAsync(saveRequest);
            var responseDto = TransferToAccountMapper.Map(saveResponse);
            return new ApiDataResult(responseDto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<TransferToAccountSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой счет" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, TransferToAccountSaveDto saveDto)
        {
            var saveRequest = TransferToAccountMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await updater.UpdateAsync(saveRequest);
            var responseDto = TransferToAccountMapper.Map(saveResponse);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Перевод на другой счет" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }
    }
}
