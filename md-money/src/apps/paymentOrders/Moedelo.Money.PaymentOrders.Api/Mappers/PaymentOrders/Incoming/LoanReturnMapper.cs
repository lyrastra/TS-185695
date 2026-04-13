using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal static class LoanReturnMapper
    {
        public static LoanReturnDto Map(PaymentOrderResponse model)
        {
            return new LoanReturnDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                LoanInterestSum = model.PaymentOrder.LoanInterestSum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                Kontragent = KontragentRequisitesMapper.MapToKontragent(
                    model.PaymentOrder.KontragentId,
                    model.PaymentOrderSnapshot.Payer),
                IsLongTermLoan = model.PaymentOrder.IsLongTermLoan ?? false,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting
            };
        }

        public static PaymentOrderSaveRequest Map(LoanReturnDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    LoanInterestSum = dto.LoanInterestSum,
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

                    Direction = MoneyDirection.Incoming,
                    OperationType = OperationType.PaymentOrderIncomingLoanReturn,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = PaymentStatus.Payed,

                    SourceFileId = dto.SourceFileId,
                    OperationState = dto.OperationState,
                    DuplicateId = dto.DuplicateId,
                    OutsourceState = dto.OutsourceState,
                },
                KontragentRequisites = dto.OperationState != OperationState.MissingKontragent
                    ? KontragentRequisitesMapper.Map(dto.Kontragent)
                    : null,
            };
        }
    }
}
