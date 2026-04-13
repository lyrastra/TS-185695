using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class BudgetaryPaymentMapper
    {
        public static BudgetaryPaymentDto Map(PaymentOrderResponse model)
        {
            return new BudgetaryPaymentDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                AccountCode = model.PaymentOrder.BudgetaryTaxesAndFees ?? 0,
                KbkPaymentType = model.PaymentOrder.KbkPaymentType ?? KbkPaymentType.Payment,
                KbkId = model.PaymentOrder.KbkId,
                KbkNumber = model.PaymentOrderSnapshot.Kbk,
                TaxationSystemType = model.PaymentOrder.TaxationSystemType,
                PatentId = model.PaymentOrder.PatentId,

                Period = new BudgetaryPeriodDto
                {
                    Type = model.PaymentOrder.BudgetaryPeriodType == null || model.PaymentOrder.BudgetaryPeriodType.Value == BudgetaryPeriodType.None 
                        ? BudgetaryPeriodType.NoPeriod 
                        : model.PaymentOrder.BudgetaryPeriodType.Value,
                    Year = model.PaymentOrder.BudgetaryPeriodYear ?? model.PaymentOrder.Date.Year,
                    Number = model.PaymentOrder.BudgetaryPeriodNumber ?? 0,
                    Date = model.PaymentOrder.BudgetaryPeriodDate
                },

                PaymentType = model.PaymentOrderSnapshot.BudgetaryPaymentType,
                PayerStatus = model.PaymentOrderSnapshot.BudgetaryPayerStatus,
                PaymentBase = model.PaymentOrderSnapshot.BudgetaryPaymentBase,
                DocumentNumber = model.PaymentOrderSnapshot.BudgetaryDocNumber,
                DocumentDate = model.PaymentOrderSnapshot.BudgetaryDocDate?.ToString("yyyy-MM-dd"),
                Uin = model.PaymentOrderSnapshot.CodeUin,

                Recipient = KontragentRequisitesMapper.MapToBudgetaryRecipient(model.PaymentOrderSnapshot.Recipient),

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed,
                TradingObjectId = model.PaymentOrder.TradingObjectId
            };
        }

        public static PaymentOrderSaveRequest Map(BudgetaryPaymentDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    KontragentName = dto.Recipient.Name,
                    SettlementAccountId = dto.SettlementAccountId,
                    Description = dto.Description,
                    ProvideInAccounting = dto.ProvideInAccounting,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.BudgetaryPayment,
                    OrderType = BankDocType.BudgetaryPayment,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,

                    BudgetaryTaxesAndFees = dto.AccountCode,

                    PatentId = dto.PatentId,
                    KbkPaymentType = dto.KbkPaymentType,
                    KbkId = dto.AccountCode != BudgetaryAccountCodes.OtherTaxes
                        ? dto.KbkId
                        : null,

                    BudgetaryPeriodType = dto.Period.Type,
                    BudgetaryPeriodYear = dto.Period.Year,
                    BudgetaryPeriodNumber = dto.Period.Number,
                    BudgetaryPeriodDate = dto.Period.Date,

                    TradingObjectId = dto.TradingObjectId,
                    TaxationSystemType = dto.TaxationSystemType,
                    TaxPostingType = dto.TaxPostingType,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
                BudgetaryFields = new BudgetaryFields
                {
                    Kbk = dto.KbkNumber,
                    Period = new BudgetaryPeriod
                    {
                        Type = dto.Period.Type,
                        Year = dto.Period.Year,
                        Number = dto.Period.Number,
                        Date = dto.Period.Date
                    },
                    PaymentType = dto.PaymentType,
                    PayerStatus = dto.PayerStatus,
                    PaymentBase = dto.PaymentBase,
                    DocNumber = dto.DocumentNumber,
                    DocDate = dto.DocumentDate,
                    CodeUin = dto.Uin
                },
                KontragentRequisites = KontragentRequisitesMapper.Map(dto.Recipient),
            };
        }

        public static BudgetaryPaymentReasonDto[] Map(IReadOnlyCollection<BudgetaryPaymentReason> reasons)
        {
            return reasons.Select(x =>
                new BudgetaryPaymentReasonDto
                {
                    Id = x.Id,
                    Designation = x.Designation,
                    Description = x.Description,
                    Code = x.Code
                }).ToArray();
        }

        public static CurrencyInvoiceNdsPaymentsRequest Map(CurrencyInvoiceNdsPaymentsRequestDto criteria)
        {
            return new CurrencyInvoiceNdsPaymentsRequest
            {
                Offset = criteria.Offset,
                Limit = criteria.Limit,
                StartDate = criteria.StartDate,
                EndDate = criteria.EndDate,
                QueryByNumber = criteria.QueryByNumber,
                KbkIds = criteria.KbkIds
            };
        }

        public static CurrencyInvoiceNdsPaymentResponseDto[] Map(IReadOnlyList<CurrencyInvoiceNdsPayment> payments)
        {
            return payments.Select(p => new CurrencyInvoiceNdsPaymentResponseDto
            {
                DocumentBaseId = p.DocumentBaseId,
                Date = p.Date,
                Number = p.Number,
                Sum = p.Sum
            }).ToArray();
        }
    }
}
