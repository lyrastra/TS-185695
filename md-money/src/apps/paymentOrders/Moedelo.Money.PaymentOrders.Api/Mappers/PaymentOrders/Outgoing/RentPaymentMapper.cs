using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal class RentPaymentMapper
    {
        public static RentPaymentDto Map(PaymentOrderResponse model)
        {
            return new RentPaymentDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                IncludeNds = model.PaymentOrder.IncludeNds,
                NdsType = model.PaymentOrder.NdsType,
                NdsSum = model.PaymentOrder.NdsSum,

                Kontragent = KontragentRequisitesMapper.MapToKontragent(
                    model.PaymentOrder.KontragentId,
                    model.PaymentOrderSnapshot.Recipient),

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed,    
                RentPeriods = model.RentPeriods.Select(MapRentPeriod).ToArray()
            };        
        }

        public static PaymentOrderSaveRequest Map(RentPaymentDto dto)
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
                    KontragentId = dto.Kontragent.Id,
                    KontragentName = dto.Kontragent.Name,
                    Description = dto.Description,
                    IncludeNds = dto.IncludeNds,
                    NdsType = dto.NdsType,
                    NdsSum = dto.NdsSum,
                    KontragentAccountCode = 760800,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,          

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingRentPayment,
                    OrderType = BankDocType.PaymentOrder,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
                KontragentRequisites = KontragentRequisitesMapper.Map(dto.Kontragent),
                RentPeriods = dto.RentPeriods?.Select(MapRentPeriod).ToArray() ?? Array.Empty<RentPeriod>(),
            };
        }

        public static RentPaymentPeriodDto MapRentPaymentPeriod(RentPaymentPeriod period)
        {
            return new RentPaymentPeriodDto
            {              
                PaymentSum = period.PaymentSum,
                RentalPaymentItemId = period.RentalPaymentItemId,
                PaymentBaseId = period.PaymentBaseId,
                PaymentDate = period.PaymentDate
            };            
        }

        private static RentPeriod MapRentPeriod(RentPeriodDto dto)
        {
            return new RentPeriod
            {
                PaymentSum = dto.PaymentSum,
                RentalPaymentItemId = dto.RentalPaymentItemId             
            };
        }

        private static RentPeriodDto MapRentPeriod(RentPeriod period)
        {
            return new RentPeriodDto
            {
                PaymentSum = period.PaymentSum,
                RentalPaymentItemId = period.RentalPaymentItemId,
                PaymentRequiredSum = period.PaymentRequiredSum
            };
        }
    }
}
