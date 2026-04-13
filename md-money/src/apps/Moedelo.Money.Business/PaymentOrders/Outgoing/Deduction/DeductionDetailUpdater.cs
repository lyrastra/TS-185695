using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction;

[OperationType(OperationType.PaymentOrderOutgoingDeduction)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class DeductionDetailUpdater :
    ConcretePaymentDetailUpdaterBase<DeductionResponse, DeductionSaveRequest, PaymentOrderSaveResponse>
{
    public DeductionDetailUpdater(
        IDeductionReader reader,
        IDeductionUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<DeductionResponse, DeductionSaveRequest> Mapper =>
        DeductionMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(DeductionSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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