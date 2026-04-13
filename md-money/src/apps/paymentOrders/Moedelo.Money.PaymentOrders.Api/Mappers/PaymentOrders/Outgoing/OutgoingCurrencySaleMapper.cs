using System;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class OutgoingCurrencySaleMapper
    {
        public static PaymentOrderSaveRequest Map(OutgoingCurrencySaleDto dto)
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
                    ExchangeRate = dto.ExchangeRate,
                    ExchangeRateDiff = dto.ExchangeRateDiff,
                    TotalSum = dto.TotalSum,
                    TransferSettlementAccountId = dto.ToSettlementAccountId,
                    
                    OperationType = OperationType.PaymentOrderOutgoingCurrencySale,
                    Direction = MoneyDirection.Outgoing,
                    OrderType = BankDocType.PaymentOrder,
                    PaymentPriority = PaymentPriority.Fifth,
                    PaidStatus = PaymentStatus.Payed,
                    TaxPostingType = dto.TaxPostingsInManualMode ? ProvidePostingType.ByHand : ProvidePostingType.Auto,
                    ProvideInAccounting = dto.ProvideInAccounting,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,

                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now
                },
            };
        }

        public static OutgoingCurrencySaleDto Map(PaymentOrderResponse model)
        {
            return new OutgoingCurrencySaleDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Sum = model.PaymentOrder.Sum,
                Date = model.PaymentOrder.Date,
                Number = model.PaymentOrder.Number,
                Description = model.PaymentOrder.Description,
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                ExchangeRate = model.PaymentOrder.ExchangeRate.GetValueOrDefault(),
                ExchangeRateDiff = model.PaymentOrder.ExchangeRateDiff.GetValueOrDefault(),
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                ToSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId.GetValueOrDefault(),
                TaxPostingsInManualMode = model.PaymentOrder.TaxPostingType == ProvidePostingType.ByHand,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }
    }
}