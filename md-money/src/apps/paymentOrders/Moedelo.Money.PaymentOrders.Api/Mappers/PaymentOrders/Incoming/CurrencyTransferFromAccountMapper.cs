using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyTransferFromAccountMapper
    {
        public static CurrencyTransferFromAccountDto Map(PaymentOrderResponse model)
        {
            return new CurrencyTransferFromAccountDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                FromSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                SourceFileId = model.PaymentOrder.SourceFileId,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }

        public static PaymentOrderSaveRequest Map(CurrencyTransferFromAccountDto dto)
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
                    TransferSettlementAccountId = dto.FromSettlementAccountId,
                    Description = dto.Description,
                    TotalSum = dto.TotalSum,

                    Direction = MoneyDirection.Incoming,
                    OperationType = OperationType.PaymentOrderIncomingCurrencyTransferFromAccount,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = PaymentStatus.Payed,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
            };
        }
    }
}