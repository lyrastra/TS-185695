using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class BudgetaryPaymentController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly IPaymentOrderCreator creator;
        private readonly IBudgetaryPaymentUpdater updater;
        private readonly IPaymentOrderRemover remover;
        private readonly IBudgetaryPaymentCatalogReader catalogReader;
        private readonly ICurrencyInvoiceNdsPaymentsReader currencyInvoiceNdsPaymentsReader;
        private readonly IBudgetaryPaymentPayerKppSetter budgetaryPaymentPayerKppSetter;

        public BudgetaryPaymentController(
            IPaymentOrderReader reader,
            IPaymentOrderCreator creator,
            IBudgetaryPaymentUpdater updater,
            IPaymentOrderRemover remover,
            IBudgetaryPaymentCatalogReader catalogReader,
            ICurrencyInvoiceNdsPaymentsReader currencyInvoiceNdsPaymentsReader,
            IBudgetaryPaymentPayerKppSetter budgetaryPaymentPayerKppSetter)
        {
            this.reader = reader;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
            this.catalogReader = catalogReader;
            this.currencyInvoiceNdsPaymentsReader = currencyInvoiceNdsPaymentsReader;
            this.budgetaryPaymentPayerKppSetter = budgetaryPaymentPayerKppSetter;
        }

        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByDocumentBaseIdAsync(int documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.BudgetaryPayment).ConfigureAwait(false);
            if (model == null)
            {
                return NotFound();
            }
            var data = BudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(BudgetaryPaymentDto dto)
        {
            var request = BudgetaryPaymentMapper.Map(dto);
            var response = await creator.CreateAsync(request);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] BudgetaryPaymentDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = BudgetaryPaymentMapper.Map(dto);
            request.DocumentBaseId = documentBaseId;
            await updater.UpdateAsync(request);
            return Ok();
        }

        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return NoContent();
        }

        [HttpGet("PaymentReasons")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPaymentReasonsAsync()
        {
            var model = await catalogReader.GetPaymentReasonsAsync();
            var dto = BudgetaryPaymentMapper.Map(model);
            return new ApiDataResult(dto);
        }

        [HttpPost("GetCurrencyInvoiceNdsPayments")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCurrencyInvoiceNdsPayments(CurrencyInvoiceNdsPaymentsRequestDto criteria)
        {
            var request = BudgetaryPaymentMapper.Map(criteria);

            var result = await currencyInvoiceNdsPaymentsReader.GetByCriteriaAsync(request);
            var data = BudgetaryPaymentMapper.Map(result);

            return new ApiDataResult(data);
        }

        [HttpPut("{documentBaseId:long}/Payer/Kpp")]
        public async Task<IActionResult> SetPayerKppAsync(long documentBaseId, [FromBody] string kpp)
        {
            await budgetaryPaymentPayerKppSetter.SetPayerKppAsync(documentBaseId, kpp);
            return Ok();
        }
    }
}
