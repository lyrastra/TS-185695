using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class TransferFromPurseController : ControllerBase
    {
        private readonly ITransferFromPurseValidator validator;
        private readonly ITransferFromPurseReader reader;
        private readonly ITransferFromPurseCreator creator;
        private readonly ITransferFromPurseUpdater updater;
        private readonly ITransferFromPurseRemover remover;

        public TransferFromPurseController(
            ITransferFromPurseValidator validator,
            ITransferFromPurseReader reader,
            ITransferFromPurseCreator creator,
            ITransferFromPurseUpdater updater,
            ITransferFromPurseRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<TransferFromPurseResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление с электронного кошелька" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = TransferFromPurseMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление с электронного кошелька" })]
        public async Task<IActionResult> CreateAsync(TransferFromPurseSaveDto saveDto)
        {
            var request = TransferFromPurseMapper.Map(saveDto);
            await validator.ValidateAsync(request).ConfigureAwait(false);
            var response = await creator.CreateAsync(request).ConfigureAwait(false);
            var dto = PaymentOrderMapper.MapToResponse(response);
            return new ApiDataResult(dto) { StatusCode = 201 };
        }

        /// <summary>
        /// Редактирование операции
        /// </summary>
        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление с электронного кошелька" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, TransferFromPurseSaveDto saveDto)
        {
            var request = TransferFromPurseMapper.Map(saveDto);
            request.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(request).ConfigureAwait(false);
            var response = await updater.UpdateAsync(request).ConfigureAwait(false);
            var dto = PaymentOrderMapper.MapToResponse(response);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление с электронного кошелька" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}