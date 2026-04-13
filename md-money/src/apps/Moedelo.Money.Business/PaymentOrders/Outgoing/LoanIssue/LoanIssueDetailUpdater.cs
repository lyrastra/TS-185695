using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue;

[OperationType(OperationType.PaymentOrderOutgoingLoanIssue)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class LoanIssueDetailUpdater :
    ConcretePaymentDetailUpdaterBase<LoanIssueResponse, LoanIssueSaveRequest, PaymentOrderSaveResponse>
{
    public LoanIssueDetailUpdater(
        ILoanIssueReader reader,
        ILoanIssueUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<LoanIssueResponse, LoanIssueSaveRequest> Mapper => LoanIssueMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(LoanIssueSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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