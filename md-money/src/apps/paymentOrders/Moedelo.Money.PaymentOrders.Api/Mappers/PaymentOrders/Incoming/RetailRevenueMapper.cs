using System;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal static class RetailRevenueMapper
    {
        public static RetailRevenueDto Map(PaymentOrderResponse model)
        {
            return new RetailRevenueDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                TaxationSystemType = model.PaymentOrder.TaxationSystemType,
                AcquiringCommissionSum = model.PaymentOrder.AcquiringCommission,
                AcquiringCommissionDate = model.PaymentOrder.AcquiringCommissionDate,
                PatentId = model.PaymentOrder.PatentId,
                IsMediation = model.PaymentOrder.IsMediation ?? false,

                IncludeNds = model.PaymentOrder.IncludeNds,
                NdsType = model.PaymentOrder.NdsType,
                NdsSum = model.PaymentOrder.NdsSum,
                
                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                SaleDate = model.PaymentOrder?.SaleDate?.Date == DateTime.MinValue
                    ? model.PaymentOrder.Date.Date
                    : model.PaymentOrder?.SaleDate?.Date ?? model.PaymentOrder.Date.Date
            };
        }

        public static PaymentOrderSaveRequest Map(RetailRevenueDto dto)
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
                    AcquiringCommission = dto.AcquiringCommissionSum,
                    AcquiringCommissionDate = dto.AcquiringCommissionDate,
                    TaxationSystemType = dto.TaxationSystemType,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    TaxPostingType = dto.TaxPostingType,
                    IsMediation = dto.IsMediation,
                    
                    IncludeNds = dto.IncludeNds,
                    NdsType = dto.NdsType,
                    NdsSum = dto.NdsSum,

                    Direction = MoneyDirection.Incoming,
                    OperationType = OperationType.MemorialWarrantRetailRevenue,
                    OrderType = BankDocType.MemorialWarrant,
                    PaidStatus = PaymentStatus.Payed,
                    KontragentAccountCode = 620200,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                    PatentId = dto.PatentId,
                    SaleDate = dto.SaleDate.Date
                },
            };
        }
    }
}
