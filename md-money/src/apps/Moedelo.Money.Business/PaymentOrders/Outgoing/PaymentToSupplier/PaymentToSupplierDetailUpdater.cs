using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier;

[OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
[InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
internal class PaymentToSupplierDetailUpdater :
    ConcretePaymentDetailUpdaterBase<PaymentToSupplierResponse, PaymentToSupplierSaveRequest, PaymentOrderSaveResponse>
{
    public PaymentToSupplierDetailUpdater(
        IPaymentToSupplierReader reader,
        IPaymentToSupplierUpdater updater,
        IExecutionInfoContextAccessor contextAccessor, 
        ISettlementAccountApiClient settlementAccountApiClient)
        : base(reader, updater, contextAccessor, settlementAccountApiClient)
    {
    }
    
    protected override Func<PaymentToSupplierResponse, PaymentToSupplierSaveRequest> Mapper =>
        PaymentToSupplierMapper.MapToSaveRequest;

    protected override async Task DetailUpdateAsync(PaymentToSupplierSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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