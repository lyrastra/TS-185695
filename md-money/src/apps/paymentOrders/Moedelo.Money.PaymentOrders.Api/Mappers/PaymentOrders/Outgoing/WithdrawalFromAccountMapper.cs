using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class WithdrawalFromAccountMapper
    {
        internal static WithdrawalFromAccountDto Map(PaymentOrderResponse model)
        {
            return new WithdrawalFromAccountDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting
            };
        }

        internal static PaymentOrderSaveRequest Map(WithdrawalFromAccountDto dto)
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
                    PaymentPriority = PaymentPriority.Fifth,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.MemorialWarrantWithdrawalFromAccount,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = PaymentStatus.Payed,

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
