using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Money.Api.Models.MissingEmployee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.MissingEmployee;
using Moedelo.Money.Domain.MissingEmployee;

namespace Moedelo.Money.Api.Controllers.PaymentOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/MissingEmployee")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentOrdersWithMissingEmployeeController : ControllerBase
    {
        private readonly IPaymentOrdersWithMissingEmployeeReader paymentOrdersWithMissingEmployeeReader;
        private readonly IPaymentOrdersWithMissingEmployeeUpdater paymentOrdersWithMissingEmployeeUpdater;

        public PaymentOrdersWithMissingEmployeeController(
            IPaymentOrdersWithMissingEmployeeReader paymentOrdersWithMissingEmployeeReader,
            IPaymentOrdersWithMissingEmployeeUpdater paymentOrdersWithMissingEmployeeUpdater)
        {
            this.paymentOrdersWithMissingEmployeeReader = paymentOrdersWithMissingEmployeeReader;
            this.paymentOrdersWithMissingEmployeeUpdater = paymentOrdersWithMissingEmployeeUpdater;
        }

        [HttpGet]
        public async Task<List<MissingEmployeePaymentOrdersResponse>> GetByEmployeeAsync(int employeeId)
        {
            var result = await paymentOrdersWithMissingEmployeeReader.GetByEmployeeIdAsync(employeeId);
            return result.Select(Map).ToList();
        }
        
        [HttpPost]
        [Route("ApproveImport")]
        public async Task<IActionResult> ApproveImportWithMissingEmployeeAsync(ApproveImportWithMissingEmployeeRequestDto request)
        {
            await paymentOrdersWithMissingEmployeeUpdater.ApproveImportWithMissingEmployeeAsync(request.EmployeeId, request.PaymentBaseIds).ConfigureAwait(false);
            return Ok();
        }
        
        private static MissingEmployeePaymentOrdersResponse Map(MissingEmployeePayment response)
        {
            return new MissingEmployeePaymentOrdersResponse
            {
                PaymentBaseId = response.DocumentBaseId,
                OperationType = response.OperationType
            };
        }
    }
}