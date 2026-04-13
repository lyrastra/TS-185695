using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment;

[OperationType(OperationType.PaymentOrderOutgoingLoanRepayment)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class LoanRepaymentDetailUpdater :
    ConcretePaymentDetailUpdaterBase<LoanRepaymentResponse, LoanRepaymentSaveRequest, PaymentOrderSaveResponse>
{
    public LoanRepaymentDetailUpdater(
        ILoanRepaymentReader reader,
        ILoanRepaymentUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<LoanRepaymentResponse, LoanRepaymentSaveRequest> Mapper =>
        LoanRepaymentMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(LoanRepaymentSaveRequest saveRequest, ChangeIsPaidRequestItem item)
    {
        if (item.IsPaid.HasValue)
        {
            saveRequest.IsPaid = item.IsPaid.Value;
        }
            
        if (!string.IsNullOrEmpty(item.PaymentNumber))
        {
            saveRequest.Number = item.PaymentNumber;
        }
            
        if (!string.IsNullOrEmpty(item.PayerSettlementNumber))
        {
            var payerSettlementNumberId = await GetPayerSettlementNumberIdAsync(item.PayerSettlementNumber);
                
            if (payerSettlementNumberId.HasValue)
            {
                saveRequest.SettlementAccountId = payerSettlementNumberId.Value;
            }
        }
    }
}