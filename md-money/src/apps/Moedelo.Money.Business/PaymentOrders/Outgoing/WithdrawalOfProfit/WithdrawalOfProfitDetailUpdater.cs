using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit;

[OperationType(OperationType.PaymentOrderOutgoingWithdrawalOfProfit)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class WithdrawalOfProfitDetailUpdater :
    ConcretePaymentDetailUpdaterBase<WithdrawalOfProfitResponse, WithdrawalOfProfitSaveRequest, PaymentOrderSaveResponse>
{
    public WithdrawalOfProfitDetailUpdater(
        IWithdrawalOfProfitReader reader,
        IWithdrawalOfProfitUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<WithdrawalOfProfitResponse, WithdrawalOfProfitSaveRequest> Mapper =>
        WithdrawalOfProfitMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(WithdrawalOfProfitSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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