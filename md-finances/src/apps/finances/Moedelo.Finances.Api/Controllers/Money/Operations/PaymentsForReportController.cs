using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations.Requests;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Finances.Api.Mappers.Money.Operations.BudgetaryPayments;
using Moedelo.Finances.Business.Services.Money.Operations;
using Moedelo.Finances.Domain.Interfaces.Business.Money;

namespace Moedelo.Finances.Api.Controllers.Money.Operations
{
    [RoutePrefix("Money/Operations/PaymentsForReport")]
    public class PaymentsForReportController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IPaymentsForReport paymentsForReport;
        private readonly IPaymentsForReportGetter paymentsGetter;

        public PaymentsForReportController(
            IUserContext userContext, 
            IPaymentsForReportGetter paymentsGetter, 
            IPaymentsForReport paymentsForReport)
        {
            this.userContext = userContext;
            this.paymentsGetter = paymentsGetter;
            this.paymentsForReport = paymentsForReport;
        }

        [HttpPost]
        [Route("GetBudgetaryPayments")]
        public async Task<List<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsAsync(GetBudgetaryAccPaymentsRequestDto requestDto)
        {
            var payments = BudgetaryPaymentsMapper.Map(requestDto);
            var request = BudgetaryPaymentsMapper.MapDtoToDomain<BudgetaryPaymentOrderOperationQueryParams>(payments);
            var budgetaryPayments = await paymentsGetter.GetBudgetaryPaymentsAsync(userContext, request).ConfigureAwait(false);
            var result = budgetaryPayments.Select(BudgetaryPaymentsMapper.MapDomainToDto).ToList();

            if (requestDto.NeedCashOperations)
            {
                var requestCashOrder = BudgetaryPaymentsMapper.MapDtoToDomain<BudgetaryCashOrderOperationQueryParams>(payments);
                var budgetaryCashPayments = await paymentsGetter
                    .GetBudgetaryCashPaymentsAsync(userContext, requestCashOrder).ConfigureAwait(false);
                result.AddRange(budgetaryCashPayments.Select(BudgetaryPaymentsMapper.MapDomainToDto));
            }

            return result;
        }

        [HttpPost]
        [Route("V2/GetBudgetaryPayments")]
        public async Task<List<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsAsync(BudgetaryAccPaymentsRequestDto requestDto)
        {
            var request = BudgetaryPaymentsMapper.MapDtoToDomain<BudgetaryPaymentOrderOperationQueryParams>(requestDto);
            var budgetaryPayments = await paymentsGetter.GetBudgetaryPaymentsWithSubPaymentsAsync(userContext, request).ConfigureAwait(false);
            var result = budgetaryPayments.Select(BudgetaryPaymentsMapper.MapDomainToDto).ToList();

            if (requestDto.NeedCashOperations)
            {
                var requestCashOrder = BudgetaryPaymentsMapper.MapDtoToDomain<BudgetaryCashOrderOperationQueryParams>(requestDto);
                var budgetaryCashPayments = await paymentsGetter
                    .GetBudgetaryCashPaymentsWithSubPaymentsAsync(userContext, requestCashOrder).ConfigureAwait(false);
                result.AddRange(budgetaryCashPayments.Select(BudgetaryPaymentsMapper.MapDomainToDto));
            }

            return result;
        }

        [HttpGet]
        [Route("GetUsnBudgetaryPrepayments")]
        public async Task<List<UsnBudgetaryPrepaymentDto>> GetUsnBudgetaryPrepaymentsAsync(int year, bool needCashOperations)
        {
            return await paymentsForReport.GetUsnBudgetaryPrepaymentsAsync(userContext, year, needCashOperations).ConfigureAwait(false);
        }
    }
}
