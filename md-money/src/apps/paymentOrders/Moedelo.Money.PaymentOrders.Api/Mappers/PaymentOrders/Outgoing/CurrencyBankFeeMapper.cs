using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyBankFeeMapper
    {
        public static CurrencyBankFeeDto Map(PaymentOrderResponse model)
        {
            return new CurrencyBankFeeDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                SourceFileId = model.PaymentOrder.SourceFileId,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }

        public static PaymentOrderSaveRequest Map(CurrencyBankFeeDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    SettlementAccountId = dto.SettlementAccountId,
                    Description = dto.Description,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    TotalSum = dto.TotalSum,

                    OperationType = OperationType.CurrencyBankFee,
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
    }
}