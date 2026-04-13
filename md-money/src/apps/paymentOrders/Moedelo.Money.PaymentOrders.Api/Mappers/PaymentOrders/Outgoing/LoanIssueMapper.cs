using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class LoanIssueMapper
    {
        public static LoanIssueDto Map(PaymentOrderResponse model)
        {
            return new LoanIssueDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                IsLongTermLoan = model.PaymentOrder.IsLongTermLoan ?? false,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                Kontragent = KontragentRequisitesMapper.MapToKontragent(
                    model.PaymentOrder.KontragentId,
                    model.PaymentOrderSnapshot.Recipient),

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

        public static PaymentOrderSaveRequest Map(LoanIssueDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    IsLongTermLoan = dto.IsLongTermLoan,
                    SettlementAccountId = dto.SettlementAccountId,
                    KontragentId = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Kontragent.Id
                        : (int?)null,
                    KontragentName = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Kontragent.Name
                        : null,
                    Description = dto.Description,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    TaxPostingType = dto.TaxPostingType,
                    KontragentAccountCode = 600200,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingLoanIssue,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = dto.IsPaid
                        ? PaymentStatus.Payed
                        : PaymentStatus.NotPayed,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
                KontragentRequisites = dto.OperationState != OperationState.MissingKontragent
                    ? KontragentRequisitesMapper.Map(dto.Kontragent)
                    : null,
            };
        }
    }
}
