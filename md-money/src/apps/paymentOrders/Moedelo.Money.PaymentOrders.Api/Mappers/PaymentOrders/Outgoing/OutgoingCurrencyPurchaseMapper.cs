using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class OutgoingCurrencyPurchaseMapper
    {
        public static PaymentOrderSaveRequest Map(OutgoingCurrencyPurchaseDto dto)
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
                    TransferSettlementAccountId = dto.ToSettlementAccountId,
                    ExchangeRate = dto.ExchangeRate,
                    ExchangeRateDiff = dto.ExchangeRateDiff,
                    TotalSum = dto.TotalSum,

                    OperationType = OperationType.PaymentOrderOutgoingCurrencyPurchase,
                    Direction = MoneyDirection.Outgoing,
                    OrderType = BankDocType.PaymentOrder,
                    PaymentPriority = PaymentPriority.Fifth,
                    PaidStatus = PaymentStatus.Payed,
                    TaxPostingType = dto.TaxPostingType,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,

                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now
                },
            };
        }

        public static OutgoingCurrencyPurchaseDto Map(PaymentOrderResponse model)
        {
            return new OutgoingCurrencyPurchaseDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Sum = model.PaymentOrder.Sum,
                Date = model.PaymentOrder.Date,
                Number = model.PaymentOrder.Number,
                Description = model.PaymentOrder.Description,
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                ExchangeRate = model.PaymentOrder.ExchangeRate.GetValueOrDefault(),
                ExchangeRateDiff = model.PaymentOrder.ExchangeRateDiff.GetValueOrDefault(),
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                ToSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }
    }
}