using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other;

[OperationType(OperationType.PaymentOrderOutgoingOther)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class OtherOutgoingDetailUpdater :
    ConcretePaymentDetailUpdaterBase<OtherOutgoingResponse, OtherOutgoingSaveRequest, PaymentOrderSaveResponse>
{
    public OtherOutgoingDetailUpdater(
        IOtherOutgoingReader reader,
        IOtherOutgoingUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<OtherOutgoingResponse, OtherOutgoingSaveRequest> Mapper =>
        OtherOutgoingMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(OtherOutgoingSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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