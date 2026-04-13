using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyTransferToAccountMapper
    {
        public static CurrencyTransferToAccountDto Map(PaymentOrderResponse model)
        {
            return new CurrencyTransferToAccountDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                ToSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId,
                Description = model.PaymentOrder.Description,
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                SourceFileId = model.PaymentOrder.SourceFileId,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };
        }

        public static PaymentOrderSaveRequest Map(CurrencyTransferToAccountDto dto)
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
                    TransferSettlementAccountId = dto.ToSettlementAccountId,
                    Description = dto.Description,
                    PaymentPriority = PaymentPriority.Fifth,
                    TotalSum = dto.TotalSum,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingCurrencyTransferToAccount,
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