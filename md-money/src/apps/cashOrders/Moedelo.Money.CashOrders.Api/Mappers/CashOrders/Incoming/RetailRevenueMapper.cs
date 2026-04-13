using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.CashOrders.Dto;
using Moedelo.Money.CashOrders.Dto.CashOrders.Incoming;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing;
using Moedelo.Money.Enums;

namespace Moedelo.Money.CashOrders.Api.Mappers.CashOrders.Incoming
{
    internal class RetailRevenueMapper
    {
        public static RetailRevenueDto Map(CashOrderResponse model)
        {
            return new RetailRevenueDto
            {
                DocumentBaseId = model.CashOrder.DocumentBaseId,
                Date = model.CashOrder.Date,
                CashId = model.CashOrder.CashId,
                Number = model.CashOrder.Number,
                Worker = new WorkerDto
                {
                    Id = model.CashOrder.SalaryWorkerId ?? 0,
                    Name = model.CashOrder.DestinationName
                },
                SalaryWorkerId = model.CashOrder.SalaryWorkerId,
                Comments = model.CashOrder.Comments,
                Destination = model.CashOrder.Destination,
                DestinationName = model.CashOrder.DestinationName,
                Sum = model.CashOrder.Sum + (model.CashOrder.PaidCardSum ?? 0m),
                PaidCardSum = model.CashOrder.PaidCardSum,
                TaxationSystemType = model.CashOrder.TaxationSystemType,
                SyntheticAccountTypeId = model.CashOrder.SyntheticAccountTypeId,
                ZReportNumber = model.CashOrder.ZReportNumber,
                PatentId = model.CashOrder.PatentId,

                NdsType = model.CashOrder.NdsType,
                NdsSum = model.CashOrder.NdsSum,
                IncludeNds = model.CashOrder.IncludeNds,

                ProvideInAccounting = model.CashOrder.ProvideInAccounting,
                PostingsAndTaxMode = model.CashOrder.PostingsAndTaxMode,
                TaxPostingType = model.CashOrder.TaxPostingType
            };
        }

        public static CashOrderSaveRequest Map(RetailRevenueDto dto)
        {
            return new CashOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                CashOrder = new CashOrder
                {
                    Date = dto.Date,
                    CashId = dto.CashId,
                    Number = dto.Number,
                    SalaryWorkerId = dto.Worker?.Id ?? dto.SalaryWorkerId,
                    Comments = dto.Comments,
                    Destination = dto.Destination,
                    DestinationName = dto.Worker?.Name ?? dto.DestinationName,
                    Sum = dto.Sum - (dto.PaidCardSum ?? 0),
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PostingsAndTaxMode = dto.PostingsAndTaxMode,
                    TaxPostingType = dto.TaxPostingType,
                    PaidCardSum = dto.PaidCardSum,
                    OperationType = OperationType.CashOrderIncomingFromRetailRevenue,
                    Direction = MoneyDirection.Incoming,
                    TaxationSystemType = dto.TaxationSystemType,
                    SyntheticAccountTypeId = dto.SyntheticAccountTypeId,
                    NdsType = dto.NdsType,
                    NdsSum = dto.NdsSum,
                    IncludeNds = dto.IncludeNds,
                    ZReportNumber = dto.ZReportNumber,
                    PatentId = dto.PatentId
                }
            };
        }
    }
}
