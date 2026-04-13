using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.CashOrders;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.CashOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class CashOrdersController : ControllerBase
    {
        private readonly ICashOrderGetter getter;
        private readonly ICashOrderRemover remover;
        private readonly IUnifiedBudgetaryPaymentReader unifiedBudgetaryPaymentReader;

        public CashOrdersController(
            ICashOrderGetter getter,
            ICashOrderRemover remover,
            IUnifiedBudgetaryPaymentReader unifiedBudgetaryPaymentReader)
        {
            this.getter = getter;
            this.remover = remover;
            this.unifiedBudgetaryPaymentReader = unifiedBudgetaryPaymentReader;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<object>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var operationType = await getter.GetOperationTypeAsync(documentBaseId);
            object responseDto;

            switch (operationType)
            {
                // Поступления

                // Списания

                //Бюджетный платёж
                case OperationType.CashOrderOutgoingUnifiedBudgetaryPayment:
                    var budgetaryPayment = await unifiedBudgetaryPaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingCashOrderResponseDto(operationType, UnifiedBudgetaryPaymentMapper.Map(budgetaryPayment));
                    break;
                
                default:
                    throw new NotImplementedException($"Not found case for type: {operationType}");
            }

            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Получение типа операции
        /// </summary>
        [HttpGet("{documentBaseId:long}/Type")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OperationType>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTypeAsync(long documentBaseId)
        {
            var type = await getter.GetOperationTypeAsync(documentBaseId);
            return new ApiDataResult(type);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }
    }
}
