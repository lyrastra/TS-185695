using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyPaymentToSupplierMapper
    {
        public static CurrencyPaymentToSupplierDto Map(PaymentOrderResponse model)
        {
            return new CurrencyPaymentToSupplierDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                TotalSum = model.PaymentOrder.TotalSum ?? 0,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                IncludeNds = model.PaymentOrder.IncludeNds,
                NdsType = model.PaymentOrder.NdsType,
                NdsSum = model.PaymentOrder.NdsSum,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                Kontragent = KontragentRequisitesMapper.MapToKontragent(
                    model.PaymentOrder.KontragentId,
                    model.PaymentOrderSnapshot.Recipient),
                
                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting
            };
        }

        public static PaymentOrderSaveRequest Map(CurrencyPaymentToSupplierDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    TotalSum = dto.TotalSum,
                    SettlementAccountId = dto.SettlementAccountId,
                    KontragentId = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Kontragent.Id
                        : (int?)null,
                    KontragentName = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Kontragent.Name
                        : null,
                    Description = dto.Description,
                    IncludeNds = dto.IncludeNds,
                    NdsType = dto.NdsType,
                    NdsSum = dto.NdsSum,
                    TaxPostingType = dto.TaxPostingType,
                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier,
                    OrderType = BankDocType.PaymentOrder,
                    OperationState = dto.OperationState,
                    DuplicateId = dto.DuplicateId,
                    SourceFileId = dto.SourceFileId,
                    OutsourceState = dto.OutsourceState,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PaidStatus = PaymentStatus.Payed
                },
                KontragentRequisites = dto.OperationState != OperationState.MissingKontragent
                    ? KontragentRequisitesMapper.Map(dto.Kontragent)
                    : null,
            };
        }
    }
}
