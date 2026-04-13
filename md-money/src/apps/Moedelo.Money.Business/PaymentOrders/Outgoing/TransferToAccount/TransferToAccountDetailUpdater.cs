using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount;

[OperationType(OperationType.PaymentOrderOutgoingTransferToAccount)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class TransferToAccountDetailUpdater :
    ConcretePaymentDetailUpdaterBase<TransferToAccountResponse, TransferToAccountSaveRequest, PaymentOrderSaveResponse>
{
    public TransferToAccountDetailUpdater(
        ITransferToAccountReader reader,
        ITransferToAccountUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<TransferToAccountResponse, TransferToAccountSaveRequest> Mapper =>
        TransferToAccountMapper.MapToSaveRequest;
    
    protected override async Task DetailUpdateAsync(TransferToAccountSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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