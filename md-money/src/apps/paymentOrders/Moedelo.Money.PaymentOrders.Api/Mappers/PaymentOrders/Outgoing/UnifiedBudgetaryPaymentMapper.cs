using System;
using System.Linq;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class UnifiedBudgetaryPaymentMapper
    {
        public static UnifiedBudgetaryPaymentDto Map(PaymentOrderResponse model)
        {
            return new UnifiedBudgetaryPaymentDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                KbkId = model.PaymentOrder.KbkId.GetValueOrDefault(),

                SubPayments = model.UnifiedBudgetarySubPayments.Select(MapSubPayment).ToArray(),

                Recipient = RecipientMapToDto(model.PaymentOrderSnapshot.Recipient),

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed,
                Uin = model.PaymentOrderSnapshot.CodeUin,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,
                PayerStatus = model.PaymentOrderSnapshot.BudgetaryPayerStatus,
            };
        }

        public static PaymentOrderSaveRequest Map(UnifiedBudgetaryPaymentDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    SettlementAccountId = dto.SettlementAccountId,
                    KontragentName = dto.Recipient.Name,
                    Description = dto.Description,
                    OperationType = OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment,
                    OrderType = BankDocType.BudgetaryPayment,
                    Direction = MoneyDirection.Outgoing,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    TaxPostingType = dto.TaxPostingType,
                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
                UnifiedBudgetarySubPayments = MapSubPayments(dto),
                KontragentRequisites = MapRecipient(dto),
                BudgetaryFields =  new BudgetaryFields
                {
                    PayerStatus = dto.PayerStatus,
                    CodeUin = dto.Uin
                },
            };
        }
      
        public static UnifiedBudgetarySubPaymentDto MapSubPayment(UnifiedBudgetarySubPayment subPayment)
        {
            return new UnifiedBudgetarySubPaymentDto
            {
                DocumentBaseId = subPayment.DocumentBaseId,
                ParentDocumentId = subPayment.ParentDocumentId,
                KbkId = subPayment.KbkNumberId,
                Period = new UnifiedBudgetaryPeriodDto
                {
                    Type = subPayment.PeriodType,
                    Year = subPayment.PeriodYear,
                    Number = subPayment.PeriodNumber,
                    Date = subPayment.PeriodDate
                },
                Sum = subPayment.PaymentSum,
                PatentId = subPayment.PatentId == 0 ? null : subPayment.PatentId,
                TradingObjectId = subPayment.TradingObjectId,
                TaxPostingType = subPayment.TaxPostingType
            };
        }

        private static UnifiedBudgetarySubPayment[] MapSubPayments(UnifiedBudgetaryPaymentDto dto)
        {
            if (dto.SubPayments == null || dto.SubPayments.Count == 0)
            {
                return Array.Empty<UnifiedBudgetarySubPayment>();
            }

            return dto.SubPayments.Select(MapSubPayment).ToArray();
        }

        private static UnifiedBudgetarySubPayment MapSubPayment(UnifiedBudgetarySubPaymentDto dto)
        {
            return new UnifiedBudgetarySubPayment
            {
                DocumentBaseId = dto.DocumentBaseId,
                ParentDocumentId = dto.ParentDocumentId,
                KbkNumberId = dto.KbkId,
                PeriodType = dto.Period.Type,
                PeriodYear = dto.Period.Year,
                PeriodNumber = dto.Period.Number,
                PeriodDate = dto.Period.Date,
                PaymentSum = dto.Sum,
                PatentId = dto.PatentId,
                TradingObjectId = dto.TradingObjectId
            };
        }
        
        private static UnifiedBudgetaryRecipientRequisitesDto RecipientMapToDto(OrderDetails recipient)
        {
            return new UnifiedBudgetaryRecipientRequisitesDto
            {
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                Name = recipient.Name,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo,
                BankBik = recipient.BankBik,
                BankName = recipient.BankName,
                SettlementAccount = recipient.SettlementNumber,
                UnifiedSettlementAccount = recipient.BankCorrespondentAccount
            };
        }
        
        private static KontragentRequisites MapRecipient(UnifiedBudgetaryPaymentDto dto)
        {
            return new KontragentRequisites
            {
                Inn = dto.Recipient.Inn,
                Kpp = dto.Recipient.Kpp,
                Name = dto.Recipient.Name,
                Okato = dto.Recipient.Okato,
                Oktmo = dto.Recipient.Oktmo,
                BankBik = dto.Recipient.BankBik,
                BankName = dto.Recipient.BankName,
                SettlementAccount = dto.Recipient.SettlementAccount,
                BankCorrespondentAccount = dto.Recipient.UnifiedSettlementAccount
            };
        }
    }
}