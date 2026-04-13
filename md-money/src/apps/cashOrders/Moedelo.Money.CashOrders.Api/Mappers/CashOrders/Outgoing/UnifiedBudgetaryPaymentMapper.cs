using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using System;
using System.Linq;

namespace Moedelo.Money.CashOrders.Api.Mappers.CashOrders.Outgoing
{
    internal class UnifiedBudgetaryPaymentMapper
    {
        public static UnifiedBudgetaryPaymentDto Map(CashOrderResponse model)
        {
            return new UnifiedBudgetaryPaymentDto
            {
                DocumentBaseId = model.CashOrder.DocumentBaseId,
                Date = model.CashOrder.Date,
                CashId = model.CashOrder.CashId,
                Number = model.CashOrder.Number,
                Sum = model.CashOrder.Sum,

                Recipient = model.CashOrder.DestinationName,
                Destination = model.CashOrder.Destination,

                ProvideInAccounting = model.CashOrder.ProvideInAccounting,
                PostingsAndTaxMode = model.CashOrder.PostingsAndTaxMode,
                SubPayments = model.UnifiedBudgetarySubPayments.Select(MapSubPayment).ToArray()
            };
        }

        public static CashOrderSaveRequest Map(UnifiedBudgetaryPaymentDto dto)
        {
            return new CashOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                CashOrder = new CashOrder
                {
                    Date = dto.Date,
                    CashId = dto.CashId,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    DestinationName = dto.Recipient,
                    Destination = dto.Destination,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PostingsAndTaxMode = dto.PostingsAndTaxMode,
                    //TaxPostingType = dto.TaxPostingType,
                    OperationType = OperationType.CashOrderOutgoingUnifiedBudgetaryPayment,
                    Direction = MoneyDirection.Outgoing
                },
                UnifiedBudgetarySubPayments = MapSubPayments(dto)
            };
        }

        private static UnifiedBudgetarySubPaymentDto MapSubPayment(UnifiedBudgetarySubPayment subPayment)
        {
            return new UnifiedBudgetarySubPaymentDto
            {
                DocumentBaseId = subPayment.DocumentBaseId,
                KbkId = subPayment.KbkNumberId,
                Period = new UnifiedBudgetaryPeriodDto
                {
                    Type = subPayment.PeriodType,
                    Year = subPayment.PeriodYear,
                    Number = subPayment.PeriodNumber,
                    Date = subPayment.PeriodDate
                },
                Sum = subPayment.PaymentSum,
                PatentId = subPayment.PatentId,
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
                KbkNumberId = dto.KbkId,
                PeriodType = dto.Period.Type,
                PeriodYear = dto.Period.Year,
                PeriodNumber = dto.Period.Number,
                PeriodDate = dto.Period.Date,
                PaymentSum = dto.Sum,
                PatentId = dto.PatentId,
                TaxPostingType = dto.TaxPostingType
            };
        }
    }
}
