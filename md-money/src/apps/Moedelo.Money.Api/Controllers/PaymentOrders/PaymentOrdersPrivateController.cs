using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class PaymentOrdersPrivateController : ControllerBase
    {
        private readonly IPaymentOrderGetter getter;
        private readonly IPaymentOrdersAsyncBatchProvider batchProvider;
        private readonly IPaymentOrderRemover remover;
        private readonly IPaymentOrderImportedApprover importedApprover;

        public PaymentOrdersPrivateController(
            IPaymentOrderGetter getter,
            IPaymentOrdersAsyncBatchProvider batchProvider,
            IPaymentOrderRemover remover,
            IPaymentOrderImportedApprover importedApprover)
        {
            this.getter = getter;
            this.batchProvider = batchProvider;
            this.remover = remover;
            this.importedApprover = importedApprover;
        }

        /// <summary>
        /// Получение типа операции по массиву базовых идентификаторов
        /// </summary>
        [HttpPost("GetTypeByBaseIds")]
        public async Task<IActionResult> GetTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await getter.GetOperationTypeByBaseIdsAsync(documentBaseIds);
            var result = response.Select(x => new OperationTypeDto { DocumentBaseId = x.DocumentBaseId, OperationType = x.OperationType }).ToArray();
            return new ApiDataResult(result);
        }

        /// <summary>
        /// Получение Id операции
        /// </summary>
        [HttpGet("{documentBaseId:long}/Id")]
        public async Task<IActionResult> GetIdAsync(long documentBaseId)
        {
            var id = await getter.GetOperationIdAsync(documentBaseId);
            return new ApiDataResult(id);
        }

        /// <summary>
        /// Получение DocumentBaseId операции
        /// </summary>
        [HttpGet("{Id:long}/DocumentBaseId")]
        public async Task<IActionResult> GetDocumentBaseIdAsync(long id)
        {
            var documentBaseId = await getter.GetOperationBaseIdAsync(id);
            return new ApiDataResult(documentBaseId);
        }

        /// <summary>
        /// Перепроведение операции
        /// </summary>
        [HttpPost("Provide")]
        public async Task<IActionResult> ProvideAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await batchProvider.ProvideAsync(documentBaseIds);
            return Ok();
        }

        /// <summary>
        /// Удаление невалидных операций
        /// </summary>
        [HttpDelete("Invalid")]
        public async Task<IActionResult> DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await remover.DeleteInvalidAsync(documentBaseIds);
            return Ok();
        }

        /// <summary>
        /// Автоапрув из зеленой таблицы в общую из импорта на заданую дату
        /// </summary>
        [HttpPost("Imported/Approve")]
        public async Task<IActionResult> ApproveImportedAsync(ApproveImportedOperationsRequestDto dto)
        {
            await importedApprover.ApproveAsync(null, dto.Skipline);
            return Ok();
        }

        /// <summary>
        /// Получение базовых идентификаторов безналичных денежных операций по типу операции
        /// Используется для перепроведения при отключении / включении склада
        /// Если год не передать то будут получены операции за текущий год
        /// </summary>
        [HttpGet("BaseIdsByOperationType")]
        public async Task<IActionResult> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year)
        {
            var result = await getter.GetBaseIdsByOperationTypeAsync(operationType, year); ;
            return new ApiDataResult(result);
        }

        /// <summary>
        /// Для вычисления номера новой исходящей денежной операции
        /// </summary>
        /// <param name="settlementAccountId">Идентификатор расчетного счета</param>
        /// <param name="year">Расчетный год(если не указан то берётся текущий)</param>
        /// <param name="cut">Значение после которого делается выборка(если не указан то с нуля)</param>
        /// <returns>Список номеров платёжек созданных в указанном году</returns>
        [HttpGet("GetOutgoingNumbers")]
        public async Task<IActionResult> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut)
        {
            var result = await getter.GetOutgoingNumbersAsync(settlementAccountId, year, cut); ;
            return new ApiDataResult(result);
        }

        /// <summary>
        /// Получение snapshot'а платежа
        /// </summary>
        [HttpGet("GetPaymentOrderSnapshot")]
        public async Task<IActionResult> GetPaymentOrderSnapshotAsync(long documentBaseId)
        {
            var model = await getter.GetPaymentOrderSnapshotAsync(documentBaseId);
            var result = model?.Map();
            return new ApiDataResult(result);
        }
    }
}
