using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentDetailController : ControllerBase
    {
        private readonly ILogger<PaymentDetailController> logger;
        private readonly IPaymentOrderGetter getter;
        private readonly IRefundToCustomerReader refundToCustomerReader;
        private readonly IAgencyContractReader agencyContractReader;
        private readonly IWithdrawalOfProfitReader withdrawalOfProfitReader;
        private readonly IPaymentToSupplierReader paymentToSupplierReader;
        private readonly IPaymentToAccountablePersonReader paymentToAccountablePersonReader;
        private readonly ITransferToAccountReader transferToAccountReader;
        private readonly ILoanRepaymentReader loanRepaymentReader;
        private readonly ILoanIssueReader loanIssueReader;
        private readonly IOtherOutgoingReader otherOutgoingReader;
        private readonly IBudgetaryPaymentReader budgetaryPaymentReader;
        private readonly IDeductionReader deductionReader;
        private readonly IUnifiedBudgetaryPaymentReader unifiedBudgetaryPaymentReader;

        public PaymentDetailController(
            ILogger<PaymentDetailController> logger,
            IPaymentOrderGetter getter,
            IRefundToCustomerReader refundToCustomerReader,
            IAgencyContractReader agencyContractReader,
            IWithdrawalOfProfitReader withdrawalOfProfitReader,
            IPaymentToSupplierReader paymentToSupplierReader,
            IPaymentToAccountablePersonReader paymentToAccountablePersonReader,
            ITransferToAccountReader transferToAccountReader,
            ILoanRepaymentReader loanRepaymentReader,
            IOtherOutgoingReader otherOutgoingReader,
            IBudgetaryPaymentReader budgetaryPaymentReader,
            ILoanIssueReader loanIssueReader,
            IDeductionReader deductionReader, 
            IUnifiedBudgetaryPaymentReader unifiedBudgetaryPaymentReader)
        {
            this.getter = getter;
            this.refundToCustomerReader = refundToCustomerReader;
            this.agencyContractReader = agencyContractReader;
            this.withdrawalOfProfitReader = withdrawalOfProfitReader;
            this.paymentToSupplierReader = paymentToSupplierReader;
            this.paymentToAccountablePersonReader = paymentToAccountablePersonReader;
            this.transferToAccountReader = transferToAccountReader;
            this.loanRepaymentReader = loanRepaymentReader;
            this.otherOutgoingReader = otherOutgoingReader;
            this.budgetaryPaymentReader = budgetaryPaymentReader;
            this.loanIssueReader = loanIssueReader;
            this.deductionReader = deductionReader;
            this.unifiedBudgetaryPaymentReader = unifiedBudgetaryPaymentReader;
            this.logger = logger;
        }
        
        /// <summary>
        /// Получение деталей операции списания
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<PaymentDetailDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Детали платежа" })]
        public async Task<IActionResult> Get(long documentBaseId)
        {
            var operationType = await getter.GetOperationTypeAsync(documentBaseId);
            PaymentDetailDto responseDto;

            switch (operationType)
            {
                // Списания

                //Возврат покупателю
                case OperationType.PaymentOrderOutgoingRefundToCustomer:
                    var returnToBuyerOperation = await refundToCustomerReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = RefundToCustomerMapper.MapToPaymentDetail(returnToBuyerOperation);
                    break;
                //Выплата по агентскому договору
                case OperationType.PaymentOrderOutgoingAgencyContract:
                    var agencyContractOperation = await agencyContractReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = AgencyContractMapper.MapToPaymentDetail(agencyContractOperation);
                    break;
                //Снятие прибыли
                case OperationType.PaymentOrderOutgoingWithdrawalOfProfit:
                    var withdrawalOfProfitOperation = await withdrawalOfProfitReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = WithdrawalOfProfitMapper.MapToPaymentDetail(withdrawalOfProfitOperation);
                    break;
                //Оплата поставщику
                case OperationType.PaymentOrderOutgoingPaymentToSupplier:
                    var paymentToSupplierOperation = await paymentToSupplierReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = PaymentToSupplierMapper.MapToPaymentDetail(paymentToSupplierOperation);
                    break;
                //Выдача подотчетному лицу
                case OperationType.PaymentOrderOutgoingPaymentToAccountablePerson:
                    var paymentToAccountablePersonOperation = await paymentToAccountablePersonReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = PaymentToAccountablePersonMapper.MapToPaymentDetail(paymentToAccountablePersonOperation);
                    break;
                //Перевод на другой счет
                case OperationType.PaymentOrderOutgoingTransferToAccount:
                    var transferToAccountOperation = await transferToAccountReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = TransferToAccountMapper.MapToPaymentDetail(transferToAccountOperation);
                    break;
                //Погашение займа или процентов
                case OperationType.PaymentOrderOutgoingLoanRepayment:
                    var loanRepaymentOperation = await loanRepaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = LoanRepaymentMapper.MapToPaymentDetail(loanRepaymentOperation);
                    break;
                //Прочее списание
                case OperationType.PaymentOrderOutgoingOther:
                    var otherOutgoingOperation = await otherOutgoingReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = OtherOutgoingMapper.MapToPaymentDetail(otherOutgoingOperation);
                    break;
                //Бюджетный платёж
                case OperationType.BudgetaryPayment:
                    var budgetaryPayment = await budgetaryPaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = BudgetaryPaymentMapper.MapToPaymentDetail(budgetaryPayment);
                    break;
                //Выдача займа
                case OperationType.PaymentOrderOutgoingLoanIssue:
                    var loanIssueOperation = await loanIssueReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = LoanIssueMapper.MapToPaymentDetail(loanIssueOperation);
                    break;
                //Выплаты удержаний
                case OperationType.PaymentOrderOutgoingDeduction:
                    var deductionOperation = await deductionReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = DeductionMapper.MapToPaymentDetail(deductionOperation);
                    break;
                //ЕНП
                case OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment:
                    var unifiedBudgetaryPaymentOperation = await unifiedBudgetaryPaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = UnifiedBudgetaryPaymentMapper.MapToPaymentDetail(unifiedBudgetaryPaymentOperation);
                    break;

                default:
                    logger.LogErrorExtraData(new { documentBaseId, operationType },"Unhandled operation type");
                    throw new NotImplementedException($"Not found case for type: {operationType}");
            }

            return new ApiDataResult(responseDto);
        }
    }
}