using System;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal class IncomingCurrencySaleMapper
    {
        public static PaymentOrderSaveRequest Map(IncomingCurrencySaleDto dto)
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
                    ProvideInAccounting = dto.ProvideInAccounting,
                    SettlementAccountId = dto.SettlementAccountId,
                    TransferSettlementAccountId = dto.FromSettlementAccountId,
                    
                    OperationType = OperationType.PaymentOrderIncomingCurrencySale,
                    Direction = MoneyDirection.Incoming,
                    OrderType = BankDocType.PaymentOrder,
                    PaymentPriority = PaymentPriority.Fifth,
                    PaidStatus = PaymentStatus.Payed,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,

                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now
                },
            };
        }

        public static IncomingCurrencySaleDto Map(PaymentOrderResponse model)
        {
            return new IncomingCurrencySaleDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Sum = model.PaymentOrder.Sum,
                Date = model.PaymentOrder.Date,
                Number = model.PaymentOrder.Number,
                Description = model.PaymentOrder.Description,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                FromSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }
    }
}