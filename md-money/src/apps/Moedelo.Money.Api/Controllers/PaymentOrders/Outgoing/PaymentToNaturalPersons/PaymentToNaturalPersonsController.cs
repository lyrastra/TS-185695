using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class PaymentToNaturalPersonsController : ControllerBase
    {
        private readonly IPaymentToNaturalPersonsValidator validator;
        private readonly IPaymentToNaturalPersonsReader reader;
        private readonly IPaymentToNaturalPersonsCreator creator;
        private readonly IPaymentToNaturalPersonsUpdater updater;
        private readonly IPaymentToNaturalPersonsRemover remover;

        public PaymentToNaturalPersonsController(
            IPaymentToNaturalPersonsValidator validator,
            IPaymentToNaturalPersonsReader reader,
            IPaymentToNaturalPersonsCreator creator,
            IPaymentToNaturalPersonsUpdater updater,
            IPaymentToNaturalPersonsRemover remover)
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
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentToNaturalPersonsResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплата физ. лицам" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            var dto = PaymentToNaturalPersonsMapper.Map(model);
            return new ApiDataResult(dto);
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(201, Type = typeof(ApiDataResponseDto<PaymentOrderSaveResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплата физ. лицам" })]
        public async Task<IActionResult> CreateAsync(PaymentToNaturalPersonsSaveDto saveDto)
        {
            var saveRequest = PaymentToNaturalPersonsMapper.Map(saveDto);
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await creator.CreateAsync(saveRequest);
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
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплата физ. лицам" })]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, PaymentToNaturalPersonsSaveDto saveDto)
        {
            var saveRequest = PaymentToNaturalPersonsMapper.Map(saveDto);
            saveRequest.DocumentBaseId = documentBaseId;
            await validator.ValidateAsync(saveRequest);
            var saveResponse = await updater.UpdateAsync(saveRequest);
            var responseDto = PaymentOrderMapper.MapToResponse(saveResponse);
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Выплата физ. лицам" })]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }
    }
}
