using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    public static class TransferToAccountMapper
    {
        public static TransferToAccountDto Map(PaymentOrderResponse model)
        {
            return new TransferToAccountDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                ToSettlementAccountId = model.PaymentOrder.TransferSettlementAccountId ?? 0,
                Description = model.PaymentOrder.Description,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed
            };
        }

        public static PaymentOrderSaveRequest Map(TransferToAccountDto dto)
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
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PaymentPriority = PaymentPriority.Fifth,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingTransferToAccount,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = dto.IsPaid
                        ? PaymentStatus.Payed
                        : PaymentStatus.NotPayed,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    IsIgnoreNumber = dto.IsIgnoreNumber,
                    OutsourceState = dto.OutsourceState,
                },
            };
        }
    }
}