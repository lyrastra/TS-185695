using System;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal class IncomingCurrencyPurchaseMapper
    {
        public static PaymentOrderSaveRequest Map(IncomingCurrencyPurchaseDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Sum = dto.Sum,
                    Date = dto.Date,
                    Number = dto.Number,
                    Description = dto.Description,
                    SettlementAccountId = dto.SettlementAccountId,
                    TransferSettlementAccountId = dto.FromSettlementAccountId,
                    TotalSum = dto.TotalSum,
                    
                    OperationType = OperationType.PaymentOrderIncomingCurrencyPurchase,
                    Direction = MoneyDirection.Incoming,
                    OrderType = BankDocType.PaymentOrder,
                    PaymentPriority = PaymentPriority.Fifth,
                    PaidStatus = PaymentStatus.Payed,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,

                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,

                    ProvideInAccounting = dto.ProvideInAccounting
                },
            };
        }

        public static IncomingCurrencyPurchaseDto Map(PaymentOrderResponse model)
        {
            return new IncomingCurrencyPurchaseDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Sum = model.PaymentOrder.Sum,
                Date = model.PaymentOrder.Date,
                Number = model.PaymentOrder.Number,
                Description = model.PaymentOrder.Description,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                FromSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId.GetValueOrDefault(),
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }
    }
}