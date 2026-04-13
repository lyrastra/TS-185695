using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class TransferFromCashCollectionController : ControllerBase
    {
        private readonly ITransferFromCashCollectionValidator validator;
        private readonly ITransferFromCashCollectionReader reader;
        private readonly ITransferFromCashCollectionCreator creator;
        private readonly ITransferFromCashCollectionUpdater updater;
        private readonly ITransferFromCashCollectionRemover remover;

        public TransferFromCashCollectionController(
            ITransferFromCashCollectionValidator validator,
            ITransferFromCashCollectionReader reader,
            ITransferFromCashCollectionCreator creator,
            ITransferFromCashCollectionUpdater updater,
            ITransferFromCashCollectionRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<TransferFromCashCollectionResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Инкассированные денежные средства" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var dto = TransferFromCashCollectionMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Инкассированные денежные средства" })]
        public async Task<IActionResult> CreateAsync(TransferFromCashCollectionSaveDto saveDto)
        {
            var request = TransferFromCashCollectionMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Инкассированные денежные средства" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, TransferFromCashCollectionSaveDto saveDto)
        {
            var request = TransferFromCashCollectionMapper.Map(saveDto);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Инкассированные денежные средства" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return Ok();
        }
    }
}