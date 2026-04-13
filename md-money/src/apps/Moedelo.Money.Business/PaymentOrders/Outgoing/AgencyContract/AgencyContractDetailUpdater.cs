using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract;

[OperationType(OperationType.PaymentOrderOutgoingAgencyContract)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class AgencyContractDetailUpdater :
    ConcretePaymentDetailUpdaterBase<AgencyContractResponse, AgencyContractSaveRequest, PaymentOrderSaveResponse>
{
    public AgencyContractDetailUpdater(
        IAgencyContractReader reader,
        IAgencyContractUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<AgencyContractResponse, AgencyContractSaveRequest> Mapper =>
        AgencyContractMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(AgencyContractSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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