using Moedelo.Money.CashOrders.Dto;
using Moedelo.Money.CashOrders.Dto.CashOrders.Incoming;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events;

namespace Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue
{
    internal static class RetailRevenueMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(RetailRevenueSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static RetailRevenueDto MapToDto(RetailRevenueSaveRequest operation)
        {
            return new RetailRevenueDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                CashId = operation.CashId,
                Number = operation.Number,
                Worker = new WorkerDto
                {
                    Id = operation.Employee.Id,
                    Name = operation.Employee.Name
                },
                SalaryWorkerId = operation.Employee.Id,
                Comments = operation.Comments,
                Destination = operation.Destination,
                DestinationName = operation.Employee.Name,
                Sum = operation.Sum,
                PaidCardSum = operation.PaidCardSum,
                TaxationSystemType = operation.TaxationSystemType,
                SyntheticAccountTypeId = operation.SyntheticAccountTypeId,
                ZReportNumber = operation.ZReportNumber,
                NdsType = operation.NdsType,
                NdsSum = operation.NdsSum,
                IncludeNds = operation.IncludeNds,
                ProvideInAccounting = operation.ProvideInAccounting,
                PostingsAndTaxMode = operation.PostingsAndTaxMode,
                TaxPostingType = operation.TaxPostingType,
                PatentId = operation.PatentId
            };
        }

        internal static RetailRevenueSaveRequest MapToSaveRequest(RetailRevenueResponse response)
        {
            return new RetailRevenueSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                CashId = response.CashId,
                Number = response.Number,
                Employee = response.Employee,
                Destination = response.Destination,
                Comments = response.Comments,
                Sum = response.Sum,
                PaidCardSum = response.PaidCardSum,
                TaxationSystemType = response.TaxationSystemType,
                ZReportNumber = response.ZReportNumber,
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                IncludeNds = response.IncludeNds,
                ProvideInAccounting = response.ProvideInAccounting,
                PostingsAndTaxMode = response.PostingsAndTaxMode,
                TaxPostingType = response.TaxPostingType,
                PatentId = response.PatentId
            };
        }

        internal static RetailRevenueResponse MapToResponse(RetailRevenueDto dto)
        {
            return new RetailRevenueResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                CashId = dto.CashId,
                Number = dto.Number,
                Employee = new Employee
                {
                    Id = dto.Worker?.Id ?? dto.SalaryWorkerId ?? 0,
                    Name = dto.Worker.Name ?? dto.DestinationName
                },
                Destination = dto.Destination,
                Comments = dto.Comments,
                Sum = dto.Sum,
                PaidCardSum = dto.PaidCardSum,
                TaxationSystemType = dto.TaxationSystemType,
                SyntheticAccountTypeId = dto.SyntheticAccountTypeId,
                ZReportNumber = dto.ZReportNumber,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                IncludeNds = dto.IncludeNds,
                ProvideInAccounting = dto.ProvideInAccounting,
                PostingsAndTaxMode = dto.PostingsAndTaxMode,
                TaxPostingType = dto.TaxPostingType,
                PatentId = dto.PatentId
            };
        }

        internal static RetailRevenueUpdated MapToUpdatedMessage(RetailRevenueSaveRequest request)
        {
            return new RetailRevenueUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                CashId = request.CashId,
                Number = request.Number,
                ZReportNumber = request.ZReportNumber,
                Sum = request.Sum,
                PaidCardSum = request.PaidCardSum,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                Contractor = new Kafka.Abstractions.Models.ContractorBase
                {
                    Id = request.Employee.Id,
                    Name = request.Employee.Name,
                    Type = ContractorType.Worker
                },
                Destination = request.Comments,
                Comment = request.Comments,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostingType == ProvidePostingType.ByHand,
                OldOperationType = OperationType.CashOrderIncomingFromRetailRevenue
            };
        }
    }
}