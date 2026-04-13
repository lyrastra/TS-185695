using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders;

namespace Moedelo.Money.PaymentOrders.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentOrdersController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly IPaymentOrderRemover remover;
        private readonly IPaymentOrderIgnoreNumberSaver ignoreNumberSaver;
        private readonly IPaymentOrdersWithMissingEmployeeReader withMissingEmployeeReader;
        private readonly IPaymentOrderImportedApprover importedApprover;
        private readonly IPaymentOrderReportBuilder reportBuilder;

        public PaymentOrdersController(
            IPaymentOrderReader reader,
            IPaymentOrderRemover remover,
            IPaymentOrderIgnoreNumberSaver ignoreNumberSaver,
            IPaymentOrdersWithMissingEmployeeReader withMissingEmployeeReader,
            IPaymentOrderImportedApprover importedApprover,
            IPaymentOrderReportBuilder reportBuilder)
        {
            this.reader = reader;
            this.remover = remover;
            this.ignoreNumberSaver = ignoreNumberSaver;
            this.withMissingEmployeeReader = withMissingEmployeeReader;
            this.importedApprover = importedApprover;
            this.reportBuilder = reportBuilder;
        }

        [HttpGet("{documentBaseId:long}/OperationType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOperationTypeAsync(long documentBaseId)
        {
            var operationType = await reader.GetOperationTypeAsync(documentBaseId);
            return new ApiDataResult(new OperationTypeDto { OperationType = operationType });
        }

        [HttpGet("{documentBaseId:long}/IsFromImport")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetIsFromImportAsync(long documentBaseId)
        {
            var isFromImport = await reader.GetIsFromImportAsync(documentBaseId);
            return new ApiDataResult(new OperationIsFromImportDto { IsFromImport = isFromImport });
        }

        [HttpPost("OperationType")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOperationTypesAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var operationTypes = await reader.GetOperationTypeByIdsAsync(documentBaseIds);
            return new ApiDataResult(operationTypes.Select(x => new OperationTypeDto { DocumentBaseId = x.DocumentBaseId, OperationType = x.OperationType }));
        }

        [HttpGet("{documentBaseId:long}/Id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOperationIdAsync(long documentBaseId)
        {
            var operationId = await reader.GetOperationIdAsync(documentBaseId);
            return new ApiDataResult(new OperationIdDto { OperationId = operationId });
        }

        [HttpGet("{documentBaseId:long}/DuplicateData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDuplicateIdAsync(long documentBaseId)
        {
            var response = await reader.GetDuplicateDataAsync(documentBaseId);
            var dto = new DuplicateDataDto
            {
                Date = response.Date,
                OperationType = response.OperationType,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState
            };
            return new ApiDataResult(dto);
        }

        [HttpGet("GetBaseIdById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBaseIdByIdAsync(long id)
        {
            var baseId = await reader.GetBaseIdByIdAsync(id);
            return new ApiDataResult(new OperationIdDto { OperationId = baseId });
        }

        [HttpDelete("Invalid")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await remover.DeleteInvalidAsync(documentBaseIds);
            return Ok();
        }

        [HttpPost("Invalid/Delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteInvalidPostAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var deletedBaseIds = await remover.DeleteInvalidAsync(documentBaseIds);
            return new ApiDataResult(deletedBaseIds);
        }

        [HttpPost("ApplyIgnoreNumber")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ApplyIgnoreNumberAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await ignoreNumberSaver.ApplyIgnoreNumberAsync(documentBaseIds);
            return Ok();
        }

        [HttpGet("WithMissingEmployee")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> WithMissingEmployeeAsync()
        {
            var result = await withMissingEmployeeReader.GetAsync();
            return new ApiDataResult(result);
        }

        [HttpPost("Imported/Approve")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ApproveImportedAsync(ApproveImportedRequestDto requestDto)
        {
            await importedApprover.ApproveAsync(requestDto.SettlementAccountId, requestDto.Skipline);
            return Ok();
        }

        [HttpGet("BaseIdsByOperationType")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year)
        {
            var result = await reader.GetBaseIdsByOperationTypeAsync(operationType, year);
            return new ApiDataResult(result);
        }

        [HttpGet("{documentBaseId:long}/Report")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReportAsync(long documentBaseId, [FromQuery] ReportFormat format)
        {
            var report = await reportBuilder.BuildAsync(documentBaseId, format);
            return File(report.Content, report.ContentType, report.FileName);
        }

        //Для вычисления номера новой исходящей денежной операции
        [HttpGet("GetOutgoingNumbers")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut)
        {
            var result = await reader.GetOutgoingNumbersAsync(settlementAccountId, year, cut);
            return new ApiDataResult(result);
        }
        
        [HttpGet("GetIsPaid")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetIsPaidAsync(long documentBaseId)
        {
            var result = await reader.GetIsPaidAsync(documentBaseId);
            return new ApiDataResult(result);
        }
        
        /// <summary>
        /// Проверка операций по DocumentBaseIds:
        /// - статус оплачем
        /// - операция не является плохой
        /// - состояние обработки платежа сотрудником Аута в статусе = null
        /// </summary>
        [HttpPost("GetDocumentsStatus")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetDocumentsStatusAsync(DocumentsStatusRequest request)
        {
            var docsStatus = await reader.GetDocumentsStatusAsync(request);
            return new ApiDataResult(docsStatus);
        }

        /// <summary>
        /// Получение snapshot'а платежа
        /// </summary>
        /// <param name="documentBaseId"></param>
        /// <returns></returns>
        [HttpGet("GetPaymentOrderSnapshot")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPaymentOrderSnapshotAsync(long documentBaseId)
        {
            var snapshot = await reader.GetPaymentSnapshotAsync(documentBaseId);
            var response = snapshot?.Map();
            return new ApiDataResult(response);
        }
    }
}
