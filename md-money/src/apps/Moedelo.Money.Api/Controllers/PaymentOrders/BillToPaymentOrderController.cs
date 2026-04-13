using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Handler.Mappers;
using Moedelo.Money.Handler.Services;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BillToPaymentOrderController : ControllerBase
    {
        private readonly IBillToPaymentOrderService billToPaymentOrderService;

        public BillToPaymentOrderController(IBillToPaymentOrderService billToPaymentOrderService)
        {
            this.billToPaymentOrderService = billToPaymentOrderService;
        }

        [HttpGet("GetBill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBillAsync(string billNumber)
        {
            var model = await billToPaymentOrderService.GetBillAsync(billNumber);

            if (model == null)
            {
                return NotFound();
            }

            var dto = BillToPaymentOrderMapper.Map(model);

            return new ApiDataResult(dto);
        }
    }
}
