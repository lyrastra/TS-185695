using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Money.Api.Models;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationsRemover operationsRemover;
        private readonly IChangeTaxationSystemSender сhangeTaxationSystemSender;
        private readonly IPaymentOrderImportedApprover importedApprover;

        public OperationsController(
            IOperationsRemover operationsRemover,
            IChangeTaxationSystemSender сhangeTaxationSystemSender,
            IPaymentOrderImportedApprover importedApprover)
        {
            this.operationsRemover = operationsRemover;
            this.сhangeTaxationSystemSender = сhangeTaxationSystemSender;
            this.importedApprover = importedApprover;
        }

        /// <summary>
        /// Удаление операций по списку базовых идентификаторов
        /// </summary>
        [HttpDelete("")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Tags = new[] { "Деньги" })]
        public async Task<IActionResult> DeleteAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await operationsRemover.DeleteAsync(documentBaseIds).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Изменение СНО в операциях
        /// </summary>
        [HttpPost("ChangeTaxationSystem")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Tags = new[] { "Деньги" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ChangeTaxationSystemAsync(ChangeTaxationSystemDto dto)
        {
            await сhangeTaxationSystemSender.SendCommandAsync(dto.DocumentBaseIds, dto.TaxationSystemType).ConfigureAwait(false);
            return Accepted();
        }

        /// <summary>
        /// Подтверждение успешно импортированных операций
        /// Сделан специально для переноса метода из md-finances
        /// </summary>
        [HttpPost("Imported/Approve")]
        [ProducesResponseType(200)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ApproveImportedAsync(ApproveImportedOperationsRequestDto dto)
        {
            if (dto.SourceType != null &&
                dto.SourceType != MoneySourceType.SettlementAccount)
            {
                return NoContent();
            }
            await importedApprover.ApproveAsync((int?)dto.SourceId, dto.Skipline);
            return Ok();
        }
    }
}
