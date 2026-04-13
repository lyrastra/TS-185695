using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class WithdrawalOfProfitMapper
    {
        internal static WithdrawalOfProfitDto Map(PaymentOrderResponse model)
        {
            return new WithdrawalOfProfitDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                Kontragent = KontragentRequisitesMapper.MapToKontragent(
                    model.PaymentOrder.KontragentId,
                    model.PaymentOrderSnapshot.Recipient),

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed
            };
        }

        internal static PaymentOrderSaveRequest Map(WithdrawalOfProfitDto dto)
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
                    KontragentId = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Kontragent.Id
                        : (int?)null,
                    KontragentName = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Kontragent.Name
                        : null,
                    Description = dto.Description,
                    KontragentAccountCode = 600200,
                    ProvideInAccounting = false,
                    PaymentPriority = PaymentPriority.Fifth,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingWithdrawalOfProfit,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    IsIgnoreNumber = dto.IsIgnoreNumber,
                    OutsourceState = dto.OutsourceState,
                },
                KontragentRequisites = dto.OperationState != OperationState.MissingKontragent
                    ? KontragentRequisitesMapper.Map(dto.Kontragent)
                    : null,
            };
        }
    }
}
